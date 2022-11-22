namespace OnlineShop.Messaging.Abstraction.Entities;

public class EventParameters
{
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}