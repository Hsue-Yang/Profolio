namespace Profolio.Server.Models.Entities;

public partial class HackMDNote
{
    public int ID { get; set; }

    public string NoteID { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Content { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Tags { get; set; }

    public string? Author { get; set; }

    public DateTime? PublishedAt { get; set; }

    public string? PublishLink { get; set; }

    public int Views { get; set; }

    public string? BlobUrl { get; set; }
}