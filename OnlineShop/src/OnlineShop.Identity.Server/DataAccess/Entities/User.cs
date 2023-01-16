using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Identity.Server.DataAccess.Entities
{
    public class User : IdentityUser
    {
        public List<Role> Roles { get; set; }
    }
}
