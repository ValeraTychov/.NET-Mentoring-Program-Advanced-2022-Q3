using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Identity.Server.DataAccess.Entities
{
    public class Role : IdentityRole
    {
        public List<User> Users { get; set; }

        public List<RoleClaim> RoleClaims { get; set; }
    }
}
