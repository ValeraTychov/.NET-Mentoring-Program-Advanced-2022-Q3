using OnlineShop.Identity.Server.Models;
using DalClaim = OnlineShop.Identity.Server.DataAccess.Entities.RoleClaim;
using DalRole = OnlineShop.Identity.Server.DataAccess.Entities.Role;
using DalUser = OnlineShop.Identity.Server.DataAccess.Entities.User;

namespace OnlineShop.Identity.Server.Mapping;

public static class MapHelper
{
    public static User ToUiUser(this DalUser user)
    {
        return new User
        {
            Name = user.UserName
        };
    }

    public static Role ToUiRole(this DalRole role)
    {
        return new Role
        {
            Id = role.Id,
            Name = role.Name,
            Claims = role.RoleClaims
                .Select(claim => new Claim
                {
                    Type = claim.ClaimType,
                    Value = claim.ClaimValue,
                })
                .ToList(),
        };
    }

    public static Claim ToUiClaim(this DalClaim claim)
    {
        return new Claim
        {
            Type = claim.ClaimType,
            Value = claim.ClaimValue
        };
    }

    public static DalClaim ToDalClaim(this Claim claim)
    {
        return new DalClaim
        {
            ClaimType = claim.Type,
            ClaimValue = claim.Value
        };
    }
}
