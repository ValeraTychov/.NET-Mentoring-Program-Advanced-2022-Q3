namespace OnlineShop.Messaging.Abstraction;

public class Settings
{
    public string Host { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public List<Type> QueuesToListen { get; set; }
}