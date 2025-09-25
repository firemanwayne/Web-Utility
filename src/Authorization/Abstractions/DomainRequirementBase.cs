namespace Web.Utility.Authorization.Abstractions;

using Microsoft.AspNetCore.Authorization;

/// <summary>
/// This is the base class for domain model authorization requirements.
/// The parameterless constructor uses "permissions" as the default claim type.
/// The parameterized constructor uses the passed in value for claim type.
/// This class is for permissions requirements that fall out side of the normal CRUD operations.
/// </summary>
public abstract class DomainRequirementBase : IAuthorizationRequirement
{
    const string s_ClaimType = "permissions";

    protected DomainRequirementBase()
    {
        ClaimType = s_ClaimType;
    }

    protected DomainRequirementBase(string claimType, IEnumerable<string> allowedValues)
    {
        ClaimType = claimType;
        AllowedValues = allowedValues;
    }

    /// <summary>
    /// The type of claims to search for.
    /// </summary>
    public string ClaimType { get; protected set; }

    /// <summary>
    /// Values used to determine if a user is authorized. These are compared with a users claim values.
    /// </summary>
    public virtual IEnumerable<string> AllowedValues { get; protected set; } = [];
}
