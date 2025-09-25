namespace Web.Utility.Authorization.Abstractions;

/// <summary>
/// Access requirements include update permissions for a data model.
/// This class is the preferred class to use for data model update authorizations.
/// This uses a convention of "{ typeof(<see cref="T"/>).Name.ToLower() }.update".
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class DomainUpdateRequirementBase<T> : DomainRequirementBase
{
    static IEnumerable<string> s_AllowedValues { get; } = [$"{typeof(T).Name.ToLower()}.update"];

    protected DomainUpdateRequirementBase() : base()
    {
        AllowedValues = s_AllowedValues;
    }
}
