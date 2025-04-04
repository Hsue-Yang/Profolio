namespace Profolio.Server.Models.Entities;

public partial class UserIntroCard
{
    public int ID { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateOnly Date { get; set; }

    public string? ImageUrl { get; set; }

    public string Category { get; set; } = null!;

    public string? IconUrl { get; set; }
}