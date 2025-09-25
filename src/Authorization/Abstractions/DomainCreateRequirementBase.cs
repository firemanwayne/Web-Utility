namespace Web.Utility.Authorization.Abstractions;

/// <summary>
/// Access requirements include create permissions for a data model.
/// This class is the preferred class to use for data model create authorizations.
/// This uses a convention of "{ typeof(<see cref="T"/>).Name.ToLower() }.create".
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class DomainCreateRequirementBase<T> : DomainRequirementBase
{
    static IEnumerable<string> s_AllowedValues { get; } = [$"{typeof(T).Name.ToLower()}.create"];

    protected DomainCreateRequirementBase() : base()
    {
        AllowedValues = s_AllowedValues;
    }
}
