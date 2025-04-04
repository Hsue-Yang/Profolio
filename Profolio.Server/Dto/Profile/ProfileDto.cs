namespace Profolio.Server.Dto.Profile
{
    public class ProfileDto
    {
        public required List<Timeline> Timelines { get; set; }
        public required List<TagTree> TagTreeNodes { get; set; }
        public class Timeline
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string ImageUrl { get; set; }
            public DateOnly TimePoint { get; set; }
        }

        public class TagTree
        {
            public string Name { get; set; }
            public string Url { get; set; }
            public List<TagTree> Children { get; set; } = new();
        }
    }
}