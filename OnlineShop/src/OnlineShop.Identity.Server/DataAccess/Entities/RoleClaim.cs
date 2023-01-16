using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Identity.Server.DataAccess.Entities
{
    public class RoleClaim : IdentityRoleClaim<string>
    {
        public Role Role { get; set; }
    }
}
