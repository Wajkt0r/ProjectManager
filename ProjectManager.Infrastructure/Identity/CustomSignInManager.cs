using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProjectManager.Domain.Entities;

namespace ProjectManager.Infrastructure.Identity
{
    public class CustomSignInManager : SignInManager<User>
    {
        public CustomSignInManager(Microsoft.AspNetCore.Identity.UserManager<User> userManager, Microsoft.AspNetCore.Http.IHttpContextAccessor contextAccessor, Microsoft.AspNetCore.Identity.IUserClaimsPrincipalFactory<User> claimsFactory, Microsoft.Extensions.Options.IOptions<Microsoft.AspNetCore.Identity.IdentityOptions> optionsAccessor, Microsoft.Extensions.Logging.ILogger<Microsoft.AspNetCore.Identity.SignInManager<User>> logger, Microsoft.AspNetCore.Authentication.IAuthenticationSchemeProvider schemes, IUserConfirmation<User> confirmation)
        : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
        }

        public override async Task<SignInResult> PasswordSignInAsync(string userName, string password,
            bool isPersistent, bool lockoutOnFailure)
        {
            var user = await UserManager.FindByEmailAsync(userName);
            if (user == null)
            {
                return SignInResult.Failed;
            }

            return await PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
        }
    }
}
