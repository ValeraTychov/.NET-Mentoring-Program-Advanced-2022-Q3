namespace OnlineShop.Messaging.Service.Models;

public class Settings
{
    public string Host { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public List<string> Queues { get; set; }
}