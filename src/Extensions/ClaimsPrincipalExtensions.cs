using System.Security.Claims;

namespace Web.Utility.Extensions;

using Web.Utility.Authentication.Models;
using Web.Utility.Constants;

/// <summary>
/// Extensions for retrieving various common properties of a user
/// </summary>
public static class ClaimsPrincipalExtensions
{
    public static string? FindFirstValue(this ClaimsPrincipal principal, string claimType)
    {
        return principal.FindFirst(claimType)?.Value;
    }

    public static IEnumerable<Auth0Permission> GetPermissions(this ClaimsPrincipal principal)
    {
        IEnumerable<Claim> permissionClaims = principal
            .Claims
            .Where(a => a.Type.Contains("authPermissions"))
            .AsEnumerable();

        HashSet<Auth0Permission> permissions = [];

        foreach (Claim permission in permissionClaims)
        {
            permissions.Add(new(permission.Type, permission.Value));
        }

        return permissions;
    }

    public static string GetUserId(this ClaimsPrincipal p) => p.Claims
            .FirstOrDefault(a => a.Type.Equals(ClaimTypes.NameIdentifier))?.Value ?? string.Empty;

    public static string GetUserName(this ClaimsPrincipal p) => p.Claims
            .FirstOrDefault(a => a.Type.Equals("userEmail"))?.Value ?? string.Empty;

    public static string GetName(this ClaimsPrincipal p) => p.Claims
            .FirstOrDefault(a => a.Type.Equals("name"))?.Value ?? string.Empty;

    public static string GetTenantId(this ClaimsPrincipal p) => p.Claims
            .FirstOrDefault(a => a.Type.Equals("sid"))?.Value ?? string.Empty;

    public static string GetPicture(this ClaimsPrincipal p) => p.Claims
            .FirstOrDefault(a => a.Type.Equals("picture"))?.Value ?? string.Empty;

    public static bool IsFederated(this ClaimsPrincipal p)
        => p.Identity is not null &&
        !string.IsNullOrEmpty(p.Identity.AuthenticationType) &&
        p.Identity.AuthenticationType.Equals(AuthenticationTypeConstants.FederatedAuthenticationType);
}
