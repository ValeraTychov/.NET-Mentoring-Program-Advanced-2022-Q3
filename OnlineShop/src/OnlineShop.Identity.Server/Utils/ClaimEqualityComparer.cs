using System.Security.Claims;

namespace OnlineShop.Identity.Server.Utils
{
    public class ClaimEqualityComparer : EqualityComparer<Claim>
    {
        public override bool Equals(Claim x, Claim y)
        {
            return x.Type.Equals(y.Type)
                   && x.Value.Equals(y.Value);
        }

        public override int GetHashCode(Claim obj)
        {
            return obj.Type.GetHashCode() ^ obj.Value.GetHashCode();
        }
    }
}
