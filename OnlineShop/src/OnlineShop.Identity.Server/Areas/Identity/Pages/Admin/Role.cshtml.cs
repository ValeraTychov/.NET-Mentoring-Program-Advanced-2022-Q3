using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Identity.Core;
using OnlineShop.Identity.Server.DataAccess.Entities;
using OnlineShop.Identity.Server.Utils;

namespace OnlineShop.Identity.Server.Areas.Identity.Pages.Admin
{
    public class RoleModel : PageModel
    {
        private readonly List<Claim> _claims = new()
        {
            ApplicationClaims.CrudCanCreate,
            ApplicationClaims.CrudCanRead,
            ApplicationClaims.CrudCanUpdate,
            ApplicationClaims.CrudCanDelete,
        };

        private readonly RoleManager<Role> _roleManager;

        public RoleModel(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public List<RoleClaim> RoleClaims { get; set; }

        public Dictionary<int, Claim> AvailableClaims { get; set; }

        [BindProperty]
        public int[] SelectedClaimIndexesToAdd { get; set; }

        [BindProperty]
        public int[] SelectedClaimIdsToRemove { get; set; }

        public async void OnPostAddRoleClaims(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            foreach (var index in SelectedClaimIndexesToAdd)
            {
                await _roleManager.AddClaimAsync(role, _claims[index]);
            }

            LoadRoleModel(roleId);
        }

        public async void OnPostRemoveRoleClaims(string roleId)
        {
            var role = _roleManager.Roles
                .Include(r => r.RoleClaims)
                .FirstOrDefault(r => r.Id == roleId);

            foreach (var id in SelectedClaimIdsToRemove)
            {
                var claim = role?.RoleClaims.FirstOrDefault(c => c.Id == id);
                if (claim == null)
                {
                    continue;
                }

                await _roleManager.RemoveClaimAsync(role, claim.ToClaim());
            }

            LoadRoleModel(roleId);
        }

        public async void OnGet(string roleId)
        {
            LoadRoleModel(roleId);
        }

        private void LoadRoleModel(string roleId)
        {
            var role = _roleManager.Roles
                .Include(r => r.RoleClaims)
                .First(u => u.Id == roleId);

            RoleClaims = role.RoleClaims;

            var claimEqualityComparer = new ClaimEqualityComparer();
            
            AvailableClaims = _claims
                .Select((c, i) => new { Id = i, Claim = c })
                .Where(x => !RoleClaims.Select(rc => rc.ToClaim()).Contains(x.Claim, claimEqualityComparer))
                .ToDictionary(x => x.Id, x => x.Claim);
            
            Id = roleId;
            Name = role.Name;
        }
    }
}
