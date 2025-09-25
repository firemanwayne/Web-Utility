namespace Web.Utility.Authentication.Models;

using System.Security.Claims;

public struct Auth0Claim
{
    public string Key { get; set; }
    public string Value { get; set; }

    public static implicit operator Claim(Auth0Claim claim)
    {
        return new(
                type: claim.Key,
                value: claim.Value,
                valueType: claim.Value.GetType().ToString());
    }

    public static implicit operator Auth0Claim(Claim claim)
    {
        return new()
        {
            Key = claim.Type,
            Value = claim.Value
        };
    }
}