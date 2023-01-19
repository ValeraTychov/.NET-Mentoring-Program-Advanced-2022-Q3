// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using OnlineShop.Identity.Server.DataAccess.Entities;

namespace OnlineShop.Identity.Server.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;

        public LogoutModel(SignInManager<User> signInManager, IIdentityServerInteractionService interaction)
        {
            _signInManager = signInManager;
            _interaction = interaction;
        }

        [BindProperty]
        public string LogoutId { get; set; }

        public void OnGet(string logoutId)
        {
            LogoutId = logoutId;
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();

            string postLogoutRedirectUri = null;
            if (LogoutId != null)
            {
                var context = await _interaction.GetLogoutContextAsync(LogoutId);
                postLogoutRedirectUri = context.PostLogoutRedirectUri;
            }

            if (!string.IsNullOrEmpty(postLogoutRedirectUri))
            {
                return Redirect(postLogoutRedirectUri);
            }

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }

            return RedirectToPage();
        }
    }
}
