namespace Web.Utility.Interfaces;

using System.Net;

public interface IResult
{
    /// <summary>
    /// Status code returned by http operation
    /// </summary>
    HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Was the operation successful?
    /// </summary>
    bool Succeeded { get; }

    /// <summary>
    /// Message if there is an error
    /// </summary>
    string? ErrorMessage { get; }

    string? Content => string.Empty;
}
