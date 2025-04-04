namespace Profolio.Server.Dto.Article
{
	public class SearchArticleDto
	{
		public List<SearchArticles> SearchArticles { get; set; } = new List<SearchArticles>();
		public int TotalCount => SearchArticles.Count;
	}

	public class SearchArticles
	{
		public string Title { get; set; }
		public string NoteID { get; set; }
	}
}