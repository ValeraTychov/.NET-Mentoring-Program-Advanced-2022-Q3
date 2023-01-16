using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Text;

namespace OnlineShop.Identity.Server;

public class JwtOptions
{
    public JwtOptions() { }

    public JwtOptions(IConfiguration configuration)
    {
        var options = configuration.GetRequiredSection(nameof(JwtOptions)).Get<JwtOptions>();
        Issuer= options.Issuer;
        Audience= options.Audience;
        Expires = DateTime.Now.Add(options.Livetime);
        SecurityKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(options.Key));
    }

    public string Key { get; set; }

    public string Issuer { get; set; }

    public string Audience { get; set; }

    public TimeSpan Livetime { get; set; }

    public DateTime Expires { get; set; }

    public SymmetricSecurityKey SecurityKey { get; set; }
}
