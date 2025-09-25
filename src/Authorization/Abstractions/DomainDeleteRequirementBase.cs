namespace Web.Utility.Authorization.Abstractions;

/// <summary>
/// Access requirements include delete permissions for a data model.
/// This class is the preferred class to use for data model delete authorizations.
/// This uses a convention of "{ typeof(<see cref="T"/>).Name.ToLower() }.delete".
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class DomainDeleteRequirementBase<T> : DomainRequirementBase
{
    static IEnumerable<string> s_AllowedValues { get; } = [$"{typeof(T).Name.ToLower()}.delete"];

    protected DomainDeleteRequirementBase() : base()
    {
        AllowedValues = s_AllowedValues;
    }
}