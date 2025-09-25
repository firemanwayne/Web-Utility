namespace Web.Utility.Authentication.Models;

using System.Security.Claims;

using Web.Utility.Options;

public sealed class Auth0ClaimsPrincipal(ClaimsPrincipal p) : ClaimsPrincipal(p)
{
    public IEnumerable<Auth0Claim>? Auth0Claims { get; private set; }

    internal Auth0ClaimsPrincipal AddAuthClaims(IEnumerable<Auth0Claim> claims)
    {
        IEnumerable<Claim> existingClaims = Claims;

        List<Auth0Claim> allClaims = [.. existingClaims, .. claims];

        Auth0Claims = allClaims?.ToList();

        return this;
    }
}

static class TransformerExtensions
{
    public static Auth0ClaimsPrincipal GetAuthUser(this ClaimsPrincipal p, Auth0ClientTransformerOptions opts)
    {
        IEnumerable<Auth0Claim> authClaims = UseClaimKeysToFindAndAdd(p, opts);

        return new Auth0ClaimsPrincipal(p)
            .AddAuthClaims(authClaims);
    }

    static readonly Func<ClaimsPrincipal, Auth0ClientTransformerOptions, IEnumerable<Auth0Claim>> UseClaimKeysToFindAndAdd = (p, opts) =>
    {
        IEnumerable<Claim> claims = p.Claims;
        List<string> customClaimValues;
        List<Auth0Claim> authClaims = [];

        if (opts.ClaimKeys != null)
        {
            foreach (string key in opts.ClaimKeys)
            {
                customClaimValues = [.. claims
                .Where(a => a.Type.Equals(key))
                .Select(a => a.Value)];

                if (customClaimValues.Count > 1)
                {
                    Auth0Claim authClaim = new()
                    {
                        Key = key,
                        Value = string.Join(",", customClaimValues)
                    };

                    authClaims.Add(authClaim);
                }
                else
                {
                    string? customClaimValue = customClaimValues
                    .FirstOrDefault();

                    if (!string.IsNullOrEmpty(customClaimValue))
                    {
                        Auth0Claim authClaim = new()
                        {
                            Key = key,
                            Value = customClaimValue
                        };

                        authClaims.Add(authClaim);
                    }
                }
            }
        }
        return authClaims;
    };
}
