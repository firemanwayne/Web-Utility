namespace SampleDomainLibrary.Models;

using System;

using Web.Utility.Interfaces;

internal class SampleDataModel : IUpdateable
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    public DateTime UpdateDate { get; init; } = DateTime.UtcNow;
    public string UpdateBy { get; init; } = string.Empty;
}