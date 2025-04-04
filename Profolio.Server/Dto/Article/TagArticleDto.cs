namespace Profolio.Server.Dto.Article
{
    public class TagArticleDto
    {
        public int TagID { get; set; }
        public string Name { get; set; }
        public int? ParentID { get; set; }
        public string NoteID { get; set; }
        public string Title { get; set; }
    }

    public class TagInfo
    {
        public int TagID { get; set; }
        public string Name { get; set; }
        public int? ParentID { get; set; }
        public int Depth { get; set; }
    }
}