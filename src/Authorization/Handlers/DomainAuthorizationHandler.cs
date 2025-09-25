namespace Web.Utility.Authorization.Handlers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

using Web.Utility.Authorization.Abstractions;
using Web.Utility.Authorization.Models;
using Web.Utility.Caching.Concrete;
using Web.Utility.Extensions;

/// <summary>
/// Handler used to search a users claims to determine if they are authorized
/// </summary>
/// <typeparam name="T">The requirement used to determine if a user is authorized</typeparam>
public class DomainAuthorizationHandler<T>(IDistributedCache cache) : AuthorizationHandler<T> where T : IAuthorizationRequirement
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, T requirement)
    {
        List<ClaimDto> claims = [];

        ClaimsPrincipal user = context.User;

        string userId = context.User.GetUserId();

        DefaultCacheKey claimsKey = new(userId, typeof(ClaimDto).Name);

        byte[]? bytes = cache.Get(claimsKey.Key);

        if (bytes is not null)
        {
            claims = JsonSerializer.Deserialize<List<ClaimDto>>(bytes) ?? [];
        }

        bool userIsAnonymous =
            user.Identity == null ||
            !user.Identities.Any(i => i.IsAuthenticated);

        if (!userIsAnonymous && requirement is DomainRequirementBase req)
        {
            List<ClaimDto> authClaims = [.. claims.Where(a => a.Type.Equals(req.ClaimType))];

            if (authClaims.Count != 0)
            {
                IEnumerable<string> values = authClaims
                    .Select(a => a.Value)
                    .Intersect(req.AllowedValues);

                if (values.Any())
                {
                    context.Succeed(requirement);

                    return Task.CompletedTask;
                }
            }
        }

        context.Fail();

        return Task.CompletedTask;
    }
}
