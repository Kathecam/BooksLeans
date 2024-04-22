using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace BookLendApi.API.Auth
{
    public class UserAuthorizationPolicyProvider: DefaultAuthorizationPolicyProvider
{
    public UserAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
    {
    }

    public override Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
    {
        if (policyName.StartsWith("User"))
        {
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddRequirements(new UserAuthorizationRequirement())
                .Build();

            return Task.FromResult(policy);
        }

        return base.GetPolicyAsync(policyName);
    }
}
}