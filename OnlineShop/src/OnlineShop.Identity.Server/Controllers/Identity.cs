using System.IdentityModel.Tokens.Jwt;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
using OnlineShop.Identity.Core;
using OnlineShop.Identity.Server.DataAccess;
using OnlineShop.Identity.Server.Mapping;
using OnlineShop.Identity.Server.Models;
using OnlineShop.Identity.Server.Utils;
using DalClaim = OnlineShop.Identity.Server.DataAccess.Entities.RoleClaim;
using DalRole = OnlineShop.Identity.Server.DataAccess.Entities.Role;
using DalUser = OnlineShop.Identity.Server.DataAccess.Entities.User;
using SystemClaim = System.Security.Claims.Claim;

namespace OnlineShop.Identity.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Identity : ControllerBase
    {

        private readonly UserManager<DalUser> _userManager;
        private readonly RoleManager<DalRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly SignInManager<DalUser> _signInManager;
        private readonly IConfiguration _configuration;

        public Identity(
            UserManager<DalUser> userManager,
            RoleManager<DalRole> roleManager,
            ApplicationDbContext dbContext,
            SignInManager<DalUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [Route("Register")]
        [HttpGet]
        public async Task<IdentityResult> Register(string userName, string password)
        {
            if (!ModelState.IsValid)
            {
                return IdentityResult.Failed();
            }

            var user = new DalUser { UserName = userName };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return IdentityResult.Success;
            }

            return result;
        }

        [Route("Login")]
        [HttpGet]
        public async Task<IdentityResult> Login(string userName, string password, bool rememberMe = false, bool lockoutOnFailure = false)
        {
            if (!ModelState.IsValid)
            {
                return IdentityResult.Failed();
            }

            var result = await _signInManager.PasswordSignInAsync(userName, password, rememberMe, lockoutOnFailure);
            if (result.Succeeded)
            {
                return IdentityResult.Success;
            }
            if (result.IsLockedOut)
            {
                return IdentityResult.Failed(new[] { IdentityErrors.UserIsLockedOut });
            }
            else
            {
                return IdentityResult.Failed(new[] { IdentityErrors.InvalidLoginAttempt });
            }
        }

        [Route("Authenticate")]
        [HttpGet]
        public async Task<object> Authenticate(string userName, string password)
        {
            var user = _userManager.Users
                .Include(u => u.Roles)
                .ThenInclude(r => r.RoleClaims)
                .FirstOrDefault(u => u.UserName == userName);

            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            {
                return IdentityResult.Failed(new[] { IdentityErrors.InvalidLoginAttempt });
            }

            var options = new JwtOptions(_configuration);

            var token = new JwtSecurityToken(
                issuer: options.Issuer,
                audience: options.Audience,
                claims: CreateClaimCollection(user),
                expires: options.Expires,
                signingCredentials: new SigningCredentials(options.SecurityKey, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private List<SystemClaim> CreateClaimCollection(DalUser user)
        {
            var claims = new List<SystemClaim>
            {
                new SystemClaim(JwtClaimTypes.Name, user.UserName)
            };

            if (user.Roles == null)
            {
                return claims;
            }

            user.Roles.ForEach(r => claims.Add(new SystemClaim(JwtClaimTypes.Role, r.Name)));

            var rolesClaims = user.Roles
                .SelectMany(r => r.RoleClaims)
                .Distinct()
                .Select(c => c.ToClaim());

            claims.AddRange(rolesClaims);

            return claims;
        }

        [Route("Users")]
        [Authorize(Roles = nameof(ApplicationRoles.Administrator))]
        [HttpGet]
        public IEnumerable<User> Users()
        {
            return _dbContext.Users
                .Include(u => u.Roles)
                .AsEnumerable()
                .Select(u => u.ToUiUser())
                .ToArray();
        }

        [Route("User")]
        [HttpPost]
        public void User(string login, string password)
        {

        }

        [Route("Roles")]
        [HttpGet]
        public IEnumerable<Role> Roles()
        {
            return _roleManager.Roles
                .AsEnumerable()
                .Select(r => r.ToUiRole())
                .ToArray();
        }

        [Route("Role/{roleId}")]
        [HttpGet]
        public Role? GetRole(string roleId)
        {
            var role = _dbContext.Roles
                .Include(r => r.RoleClaims)
                .FirstOrDefault(u => u.Id == roleId);

            return role?.ToUiRole();
        }

        [Route("Role/{roleId}")]
        [HttpPost]
        public async void CreateRole(string role)
        {
            if (!string.IsNullOrEmpty(role) && !await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new DalRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = role,
                    NormalizedName = role.ToUpper()
                });
            }
        }

        [Route("Role/{roleId}")]
        [HttpDelete]
        public async void DeleteRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            await _roleManager.DeleteAsync(role);
        }

        [Route("Role/{roleId}/Claims")]
        [HttpGet]
        public IEnumerable<Claim> RoleClaims(string roleId)
        {
            var role = _roleManager.Roles
                .Include(r => r.RoleClaims)
                .FirstOrDefault(u => u.Id == roleId);

            return role?.RoleClaims
                ?.AsEnumerable()
                ?.Select(c => c.ToUiClaim()) ?? Array.Empty<Claim>();
        }

        [Route("Role/{roleId}/Claim")]
        [HttpPost]
        public async void AddRoleClaim(string roleId, Claim claim)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            await _roleManager.AddClaimAsync(role, claim.ToDalClaim().ToClaim());
        }

        [Route("Role/{roleId}/Claim")]
        [HttpDelete]
        public async void DeleteRoleClaim(string roleId, Claim claim)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            await _roleManager.RemoveClaimAsync(role, claim.ToDalClaim().ToClaim());
        }
    }
}
