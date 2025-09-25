namespace SampleDomainLibrary.Authorizations.Requirements;

using SampleDomainLibrary.Models;

using Web.Utility.Authorization.Abstractions;

internal class SampleDomainAccessPermission : DomainAccessRequirementBase<SampleDataModel>
{
    public const string PolicyName = "SampleDomainAccessPolicy";

    public override string ToString() => $"{nameof(SampleDomainAccessPermission)}: Requires a user with any permissions for {nameof(SampleDataModel)}s.";
}
