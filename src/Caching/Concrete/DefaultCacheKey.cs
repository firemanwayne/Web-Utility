namespace Web.Utility.Caching.Concrete;

using System;
using System.Diagnostics;

using Web.Utility.Interfaces;

public sealed class DefaultCacheKey : ICacheKey
{
    readonly Stopwatch _stopWatch;

    public DefaultCacheKey(string primaryKey, string? secondaryKey = null, string? message = null)
    {
        PrimaryKey = primaryKey ?? throw new ArgumentNullException(nameof(primaryKey));

        SecondaryKey = secondaryKey;

        Message = message;

        CreatedDate = DateTime.UtcNow;

        Key = $"{PrimaryKey}{secondaryKey}Cache";

        _stopWatch = new Stopwatch();
    }

    public string Key { get; }
    public string PrimaryKey { get; }
    public string? SecondaryKey { get; }
    public string? Message { get; private set; }
    public DateTime CreatedDate { get; }
    public TimeSpan OperationDuration { get; private set; }

    public void SetMessage(string message) => Message = message;

    public void StartOperation() => _stopWatch.Start();

    public void StopOperation()
    {
        _stopWatch.Stop();

        OperationDuration = TimeSpan.FromMilliseconds(_stopWatch.ElapsedMilliseconds);
    }
}
