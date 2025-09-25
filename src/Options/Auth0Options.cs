namespace Web.Utility.Options;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Auth0Options
{
    string _audience = string.Empty;
    string _authority = string.Empty;
    string _baseUri = string.Empty;
    string _clientId = string.Empty;
    string _clientSecret = string.Empty;
    string _connectionName = string.Empty;
    string _domain = string.Empty;

    /// <summary>
    /// Auth0 Audience
    /// </summary>
    [JsonPropertyName("audience")]
    public string Audience
    {
        get => _audience;
        set => _audience = string.IsNullOrEmpty(value) ? throw new ArgumentNullException(nameof(value)) : value;
    }

    /// <summary>
    /// Auth0 Authority
    /// </summary>
    [JsonPropertyName("authority")]
    public string Authority
    {
        get => _authority;
        set => _authority = string.IsNullOrEmpty(value) ? throw new ArgumentNullException(nameof(value)) : value;
    }

    /// <summary>
    /// Base Uri for Auth0
    /// </summary>
    [JsonPropertyName("baseUri")]
    public string BaseUri
    {
        get => _baseUri;
        set => _baseUri = new UrlAttribute().IsValid(value) ? value : throw new UriFormatException(nameof(value));
    }

    /// <summary>
    /// Auth0 Client Id
    /// </summary>
    [JsonPropertyName("clientId")]
    public string ClientId
    {
        get => _clientId;
        set => _clientId = string.IsNullOrEmpty(value) ? throw new ArgumentNullException(nameof(value)) : value;
    }

    /// <summary>
    /// Auth0 Client Secret
    /// </summary>
    [JsonPropertyName("clientSecret")]
    public string ClientSecret
    {
        get => _clientSecret;
        set => _clientSecret = string.IsNullOrEmpty(value) ? throw new ArgumentNullException(nameof(value)) : value;
    }

    /// <summary>
    /// Connection String for Auth0 user database
    /// </summary>
    [JsonPropertyName("connectionName")]
    public string ConnectionName
    {
        get => _connectionName;
        set => _connectionName = string.IsNullOrEmpty(value) ? throw new ArgumentNullException(nameof(value)) : value;
    }

    /// <summary>
    /// Domain for Auth0
    /// </summary>
    [JsonPropertyName("domain")]
    public string Domain
    {
        get => _domain;
        set => _domain = string.IsNullOrEmpty(value) ? throw new ArgumentNullException(nameof(value)) : value;
    }

    public const string Abbreviation = "Auth0";

    public const string HttpClientName = "Auth0Client";
}
