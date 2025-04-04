namespace Profolio.Server.Models.Entities;

public partial class Timeline
{
	public int ID { get; set; }

	public DateTime TimePoint { get; set; }

	public string Title { get; set; } = null!;

	public string Description { get; set; } = null!;

	public string? ImageUrl { get; set; }

	public DateTime? CreatedAt { get; set; }
}