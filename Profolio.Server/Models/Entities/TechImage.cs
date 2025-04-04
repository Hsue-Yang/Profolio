namespace Profolio.Server.Models.Entities;

public partial class TechImage
{
    public int ID { get; set; }

    public string TagId { get; set; } = null!;

    public string PhotoUrl { get; set; } = null!;
}