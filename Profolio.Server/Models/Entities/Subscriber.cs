namespace Profolio.Server.Models.Entities;

public partial class Subscriber
{
    public int ID { get; set; }

    public string Email { get; set; } = null!;

    public bool? IsSubscribed { get; set; }

    public DateTime? SubscribedAt { get; set; }

    public DateTime? UnsubscribedAt { get; set; }
}