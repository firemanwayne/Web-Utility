namespace Web.Utility.Authorization.Models;

using System.Text.Json.Serialization;

/// <summary>
/// Condensed version of a Claim
/// </summary>
public record ClaimDto
{
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;
}