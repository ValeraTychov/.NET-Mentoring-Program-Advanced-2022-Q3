using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Identity.Core;
using OnlineShop.Identity.Server.DataAccess;
using OnlineShop.Identity.Server.Mapping;
using OnlineShop.Identity.Server.Models;
using DalUser = OnlineShop.Identity.Server.DataAccess.Entities.User;
using DalRole = OnlineShop.Identity.Server.DataAccess.Entities.Role;
using DalClaim = OnlineShop.Identity.Server.DataAccess.Entities.RoleClaim;

namespace OnlineShop.Identity.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Identity : ControllerBase
    {
        
        private readonly UserManager<DalUser> _userManager;
        private readonly RoleManager<DalRole> _roleManager;
        private readonly ApplicationDbContext _dbContext;

        public Identity(UserManager<DalUser> userManager, RoleManager<DalRole> roleManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
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
