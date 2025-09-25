namespace Web.Utility.Options;

public sealed class Auth0ClientTransformerOptions
{
    string _authenticationType = string.Empty;

    /// <summary>
    /// Authentication Type
    /// </summary>
    public string AuthenticationType
    {
        get => _authenticationType;
        set => _authenticationType = string.IsNullOrEmpty(value) ? throw new ArgumentNullException(nameof(value)) : value;
    }

    /// <summary>
    /// 
    /// </summary>
    public string[]? ClaimKeys { get; set; }
}
