namespace Web.Utility.Authentication.Models;

public readonly struct Auth0Permission(string type, string value)
{
    public string Type { get; } = type;
    public string Value { get; } = value;
}
