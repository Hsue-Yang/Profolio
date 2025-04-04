namespace Profolio.Server.Dto.Article
{
    public class TreeViewDto
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public List<TreeViewDto> Children { get; set; } = new();
    }
}