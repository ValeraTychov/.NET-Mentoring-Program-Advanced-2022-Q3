namespace OnlineShop.Identity.Core;

public static class ApplicationRoles
{
    public static string User { get; } = nameof(User);

    public static string Manager { get; } = nameof(Manager);

    public static string Administrator { get; } = nameof(Administrator);
}