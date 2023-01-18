using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Identity.Server.DataAccess;
using OnlineShop.Identity.Server.DataAccess.Entities;

namespace OnlineShop.Identity.Server.Areas.Identity.Pages.Admin
{
    public class UserModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public UserModel(ApplicationDbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public List<Role> UserRoles { get; set; }

        public List<Role> AvailableRoles { get; set; }

        [BindProperty]
        public string[] SelectedRolesToAdd { get; set; }

        [BindProperty]
        public string[] SelectedRolesToRemove { get; set; }

        public void OnGet(string userId)
        {
            LoadUserModel(userId);
        }

        public async void OnPostAddUserRoles(string userId)
        {
            var user = _userManager.Users.Include(u => u.Roles).First(u => u.Id == userId);
            if (user == null)
            {
                return;
            }

            foreach (var role in SelectedRolesToAdd)
            {

                await _userManager.AddToRoleAsync(user, role);
            }

            LoadUserModel(userId);
        }

        public async void OnPostRemoveUserRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return;
            }

            await _userManager.RemoveFromRolesAsync(user, SelectedRolesToRemove);

            LoadUserModel(userId);
        }

        private void LoadUserModel(string userId)
        {
            var user = _dbContext.Users
                .Include(u => u.Roles)
                .First(u => u.Id == userId);

            AvailableRoles = _dbContext.Roles
                .AsEnumerable()
                .Except(user.Roles.Select(ur => ur))
                .ToList();

            Id = user.Id;
            Name = user.UserName;
            UserRoles = user.Roles
                .ToList();
        }
    }
}
