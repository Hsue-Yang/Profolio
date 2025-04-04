namespace Profolio.Server.Models.Entities;

public partial class Tag
{
    public int ID { get; set; }

    public string Name { get; set; } = null!;

    public int? ParentID { get; set; }
}