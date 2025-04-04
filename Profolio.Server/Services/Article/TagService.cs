using MapsterMapper;
using Profolio.Server.Dto.Article;
using Profolio.Server.Models.Entities;
using Profolio.Server.Repository.Interfaces.Article;
using Profolio.Server.Services.Interfaces;

namespace Profolio.Server.Services.Article
{
	public class TagService : ITagService
	{
		private readonly ITagRepository _repo;
		private readonly IMapper _mapper;
		public TagService(ITagRepository repo, IMapper mapper)
		{
			_repo = repo;
			_mapper = mapper;
		}

		public async Task<TagDto> GetTag(string tagName)
		{
			var result = await _repo.FirstOrDefaultAsync(x => x.Name == tagName);
			return _mapper.Map<TagDto>(result);
		}

		public async Task<TagDto> AddTag(string tagName, int? parentID)
		{
			var result = await _repo.AddAsync(new Tag
			{
				Name = tagName,
				ParentID = parentID
			});
			var addTag = await _repo.FirstOrDefaultAsync(t => t.Name == tagName && t.ParentID == parentID);

			return _mapper.Map<TagDto>(addTag);
		}

		public async Task<List<TagDto>> GetSubTitle()
		{
			var parentTags = await _repo.ListAsync(tag => tag.ParentID == null);
			var childTags = await _repo.ListAsync(tag => tag.ParentID != null);
			var tagTree = parentTags.Select(tag => new TagDto
			{
				ID = tag.ID,
				Name = tag.Name,
				ParentID = tag.ParentID,
				Children = childTags.Where(childs => childs.ParentID == tag.ID)
									.Select(child => _mapper.Map<TagDto>(child)).ToList()
			}).ToList();

			return tagTree;
		}

		public async Task<List<TreeViewDto>> GetTagTreeArticles()
		{
			// 重整
			var data = await _repo.GetTagTreeArticle();
			// 1. 整理唯一標籤資料（含 ParentID），並計算階層深度
			var tagInfos = data
				.Select(r => new { r.TagID, r.Name, r.ParentID })
				.Distinct()
				.Select(x => new TagInfo
				{
					TagID = x.TagID,
					Name = x.Name,
					ParentID = x.ParentID,
					Depth = 0
				})
			.ToDictionary(x => x.TagID);

			int ComputeDepth(TagInfo tag, Dictionary<int, TagInfo> allTags)
			{
				int depth = 0;
				int? current = tag.ParentID;
				while (current.HasValue && allTags.ContainsKey(current.Value))
				{
					depth++;
					current = allTags[current.Value].ParentID;
				}
				return depth;
			}
			foreach (var tag in tagInfos.Values)
			{
				tag.Depth = ComputeDepth(tag, tagInfos);
			}

			// 2. 針對每篇文章（依 NoteID 分組），選出深度最大的標籤作為該文章的歸屬
			var articles = data
				.GroupBy(r => r.NoteID)
				.Select(g => new
				{
					NoteID = g.Key,
					Title = g.First().Title,
					CandidateTagIDs = g.Select(x => x.TagID).Distinct().ToList()
				})
				.ToList();

			var articleAssignments = new Dictionary<string, int>(); // noteID -> chosen TagID
			foreach (var article in articles)
			{
				// 如果同篇文章出現在多個標籤，選出深度最大的
				int chosenTag = article.CandidateTagIDs
					.OrderByDescending(tid => tagInfos.ContainsKey(tid) ? tagInfos[tid].Depth : 0)
					.First();
				articleAssignments[article.NoteID] = chosenTag;
			}

			// 3. 決定哪些標籤會出現在最終樹中
			// 除了直接有文章的標籤，也要保留作為子節點祖先的標籤
			var usedTagIds = new HashSet<int>(articleAssignments.Values);
			foreach (var tagId in articleAssignments.Values.ToList())
			{
				int? parent = tagInfos[tagId].ParentID;
				while (parent.HasValue)
				{
					usedTagIds.Add(parent.Value);
					if (tagInfos.ContainsKey(parent.Value))
						parent = tagInfos[parent.Value].ParentID;
					else
						break;
				}
			}

			// 4. 建立用到的標籤節點，轉換成 TreeViewDto（只有 usedTagIds 的標籤會出現）
			var tagNodes = new Dictionary<int, TreeViewDto>();
			foreach (var tagId in usedTagIds)
			{
				if (tagInfos.ContainsKey(tagId))
				{
					var tag = tagInfos[tagId];
					tagNodes[tagId] = new TreeViewDto
					{
						Id = tag.TagID.ToString(),
						Label = tag.Name,
						Children = new List<TreeViewDto>()
					};
				}
			}

			// 5. 根據 ParentID 建立標籤層級（注意：若一個標籤的 Parent 不在 usedTagIds 中，就視為根）
			List<TreeViewDto> roots = new List<TreeViewDto>();
			foreach (var tagId in tagNodes.Keys)
			{
				var tag = tagInfos[tagId];
				if (tag.ParentID.HasValue && tagNodes.ContainsKey(tag.ParentID.Value))
				{
					tagNodes[tag.ParentID.Value].Children.Add(tagNodes[tagId]);
				}
				else
				{
					roots.Add(tagNodes[tagId]);
				}
			}

			// 6. 將文章依照選定的標籤加入
			foreach (var article in articles)
			{
				int chosenTag = articleAssignments[article.NoteID];
				if (tagNodes.ContainsKey(chosenTag))
				{
					tagNodes[chosenTag].Children.Add(new TreeViewDto
					{
						Id = article.NoteID,
						Label = article.Title,
						Children = new List<TreeViewDto>()
					});
				}
			}

			// 7. 處理沒有歸類到上述主要標籤的文章
			// 例如資料中可能存在沒有標籤（或非主要標籤）的文章，統一放到 "More" 節點 (id = 99)
			// 找出所有文章 NoteID，如果還有未被指派的，則加入 More 節點
			var allArticleIds = articles.Select(a => a.NoteID).ToHashSet();
			var assignedArticleIds = new HashSet<string>(articleAssignments.Keys);
			var unassignedArticleRows = data
				.Where(r => !assignedArticleIds.Contains(r.NoteID))
				.GroupBy(r => r.NoteID)
				.Select(g => g.First())
				.ToList();

			if (unassignedArticleRows.Any())
			{
				var moreNode = new TreeViewDto
				{
					Id = "99",
					Label = "More",
					Children = new List<TreeViewDto>()
				};

				foreach (var row in unassignedArticleRows)
				{
					moreNode.Children.Add(new TreeViewDto
					{
						Id = row.NoteID,
						Label = row.Title,
						Children = new List<TreeViewDto>()
					});
				}
				roots.Add(moreNode);
			}

			return roots;
		}
	}
}