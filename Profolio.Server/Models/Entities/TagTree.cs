namespace Profolio.Server.Models.Entities;

public partial class TagTree
{
    public int ID { get; set; }

    public int? TagID { get; set; }

    public string Url { get; set; } = null!;
}