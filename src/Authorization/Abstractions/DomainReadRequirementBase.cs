namespace Web.Utility.Authorization.Abstractions;

/// <summary>
/// Access requirements include read permissions for a data model.
/// This class is the preferred class to use for data model read authorizations.
/// This uses a convention of "{ typeof(<see cref="T"/>).Name.ToLower() }.read".
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class DomainReadRequirementBase<T> : DomainRequirementBase
{
    static IEnumerable<string> s_AllowedValues = [$"{nameof(T).ToLower()}.read"];

    protected DomainReadRequirementBase() : base()
    {
        AllowedValues = s_AllowedValues;
    }
}
