using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Identity.Server.DataAccess.Entities;

namespace OnlineShop.Identity.Server.Areas.Identity.Pages.Admin
{
    public class RolesModel : PageModel
    {
        private readonly RoleManager<Role> _roleManager;

        public RolesModel(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        [BindProperty]
        public string NewRole { get; set; }

        public List<OutputRole> OutputRoles { get; set; }

        public void OnGet()
        {
            OutputRoles = GetRoles();
        }

        public async void OnPostAddNewRole()
        {
            if (!string.IsNullOrEmpty(NewRole) && !await _roleManager.RoleExistsAsync(NewRole))
            {
                await _roleManager.CreateAsync(new DataAccess.Entities.Role
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = NewRole,
                    NormalizedName = NewRole.ToUpper()
                });
            }

            OutputRoles = GetRoles();
        }

        public async void OnPostDeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            await _roleManager.DeleteAsync(role);

            OutputRoles = GetRoles();
        }

        private List<OutputRole> GetRoles()
        {
            return _roleManager.Roles
                .Select(r => new OutputRole
                {
                    Id = r.Id,
                    Name = r.Name,
                })
                .ToList();
        }
    }

    public class OutputRole
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
