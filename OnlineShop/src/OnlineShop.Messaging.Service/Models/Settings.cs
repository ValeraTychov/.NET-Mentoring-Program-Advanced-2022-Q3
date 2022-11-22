using System.Reflection.Metadata.Ecma335;
using OnlineShop.Messaging.Abstraction.Entities;

namespace OnlineShop.Messaging.Service.Models;

public class Settings
{
    public string Host { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public List<Type> Queues { get; set; }
}