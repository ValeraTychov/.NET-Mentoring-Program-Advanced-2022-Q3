using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Identity.Server.DataAccess;
using OnlineShop.Identity.Server.DataAccess.Entities;

namespace OnlineShop.Identity.Server.Areas.Identity.Pages.Admin;

public class UsersModel : PageModel
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<DataAccess.Entities.Role> _roleManager;
    private readonly ApplicationDbContext _dbContext;

    public UsersModel(UserManager<User> userManager, RoleManager<DataAccess.Entities.Role> roleManager, ApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _dbContext = dbContext;
    }

    [BindProperty]
    public IEnumerable<OutputUser> OutputUsers { get; set; }

    public void OnGet()
    {
        OutputUsers = GetUsers();
    }

    public async void OnPostDeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        await _userManager.DeleteAsync(user);

        OutputUsers = GetUsers();
    }

    public IEnumerable<OutputUser> GetUsers()
    {
        return _dbContext.Users
            .Include(u => u.Roles)
            .Select(user => new OutputUser
            {
                Id = user.Id,
                Name = user.UserName,
                Roles = user.Roles.Select(r => r.Name),
            })
            .ToArray();
    }

    public class OutputUser
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}