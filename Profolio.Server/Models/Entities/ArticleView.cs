namespace Profolio.Server.Models.Entities;

public partial class ArticleView
{
    public int ID { get; set; }

    public int NoteID { get; set; }

    public int? UserID { get; set; }

    public DateTime? ViewedAt { get; set; }
}