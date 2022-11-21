namespace OnlineShop.Messaging.Service.Models;

public class Message
{
    public Message(object? data)
    {
        _ = data ?? throw new ArgumentNullException(nameof(data));

        Type = data.GetType().FullName;
        Data = data;
    }

    public string? Type { get; set; }

    public object Data { get; set; }
}