using System.Security.Claims;

namespace OnlineShop.Identity.Core;

public static class ApplicationClaims
{
    public static string CrudType { get; } = "AllowedCRUD";

    public static string CanCreate { get; } = nameof(CanCreate);

    public static string CanRead { get; } = nameof(CanRead);

    public static string CanUpdate { get; } = nameof(CanUpdate);

    public static string CanDelete { get; } = nameof(CanDelete);

    public static Claim CrudCanCreate { get; } = new (CrudType, CanCreate);
        
    public static Claim CrudCanRead { get; } = new(CrudType, CanRead);
        
    public static Claim CrudCanUpdate { get; } = new(CrudType, CanUpdate);
        
    public static Claim CrudCanDelete { get; } = new(CrudType, CanDelete);

}