namespace Profolio.Server.Dto.Article
{
	public class TagDto
	{
		public int ID { get; set; }
		public required string Name { get; set; }
		public int? ParentID { get; set; }
		public List<TagDto> Children { get; set; } = [];
	}
}