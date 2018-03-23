using System.Threading.Tasks;
using medic.Data.Model;
using medic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Medic.Authorization
{
    public class UserIsOwnerAuthorizationHandler
                : AuthorizationHandler<OperationAuthorizationRequirement, Consulta>
    {
        UserManager<ApplicationUser> _userManager;

        public UserIsOwnerAuthorizationHandler(UserManager<ApplicationUser>
            userManager)
        {
            _userManager = userManager;
        }

        protected override Task
            HandleRequirementAsync(AuthorizationHandlerContext context,
                                   OperationAuthorizationRequirement requirement,
                                   Consulta resource)
        {
            if (context.User == null || resource == null)
            {
                // Return Task.FromResult(0) if targeting a version of
                // .NET Framework older than 4.6:
                return Task.CompletedTask;
            }


            if (resource.OwnerID == _userManager.GetUserId(context.User))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}

