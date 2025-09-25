namespace Web.Utility.Authorization.Abstractions;

/// <summary>
/// Access requirements include any/all permissions for a data model.
/// This class is the preferred class to use for data model access authorizations.
/// This uses a convention of "{ typeof(<see cref="T"/>).Name.ToLower() }.read" or
/// This uses a convention of "{ typeof(<see cref="T"/>).Name.ToLower() }.create" or
/// This uses a convention of "{ typeof(<see cref="T"/>).Name.ToLower() }.update" or
/// This uses a convention of "{ typeof(<see cref="T"/>).Name.ToLower() }.delete"
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class DomainAccessRequirementBase<T> : DomainRequirementBase
{
    static IEnumerable<string> s_AllowedValues { get; } = [
        $"{typeof(T).Name.ToLower()}.read",
        $"{typeof(T).Name.ToLower()}.create",
        $"{typeof(T).Name.ToLower()}.update",
        $"{typeof(T).Name.ToLower()}.delete"
        ];

    protected DomainAccessRequirementBase() : base()
    {
        AllowedValues = s_AllowedValues;
    }
}
