using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineShop.Identity.Server.Areas.Identity.Pages.Admin
{
    public class AdminNavPages
    {
        public static string Users => "Users";

        public static string Roles => "Roles";

        public static string UsersNavClass(ViewContext viewContext) => NavPages.PageNavClass(viewContext, Users);

        public static string RolesNavClass(ViewContext viewContext) => NavPages.PageNavClass(viewContext, Roles);
    }
}
