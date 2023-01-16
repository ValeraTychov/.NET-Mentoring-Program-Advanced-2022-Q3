namespace OnlineShop.Identity.Server.Models
{
    public class Role
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public List<Claim> Claims { get; set; }
    }
}
