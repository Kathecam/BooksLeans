using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BookLendApi.API.Auth
{

    public class UserAuthorizationRequirement : IAuthorizationRequirement
    {
    }

    public class UserAuthorizationHandler : AuthorizationHandler<UserAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserAuthorizationRequirement requirement)
        {
            // Verificar si el usuario está autenticado
            if (context.User.Identity.IsAuthenticated)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
