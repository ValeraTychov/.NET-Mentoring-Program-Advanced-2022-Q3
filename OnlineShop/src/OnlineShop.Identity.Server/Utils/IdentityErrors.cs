using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Identity.Server.Utils
{
    public static class IdentityErrors
    {
        public static IdentityError UserIsLockedOut => new IdentityError()
        {
            Code = "🖕",
            Description = "User is locked out."
        };

        public static IdentityError InvalidLoginAttempt => new IdentityError()
        {
            Code = "🖕",
            Description = "Invalid login attempt."
        };

        public static IdentityError InvalidUsernameOrPassword => new IdentityError()
        {
            Code = "🖕",
            Description = "Invalid username or password."
        };
    }
}
