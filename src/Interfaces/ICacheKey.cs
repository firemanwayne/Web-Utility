namespace Web.Utility.Interfaces;

using System;

public interface ICacheKey
{
    /// <summary>
    /// DateTime value when cache key was generated
    /// </summary>
    DateTime CreatedDate { get; }

    /// <summary>
    /// Optional place holder for the amount of time the caching operation takes to complete
    /// </summary>
    TimeSpan OperationDuration { get; }

    /// <summary>
    /// message displayed when cache event occurs
    /// </summary>
    string? Message { get; }

    /// <summary>
    /// The Cache key used to cache data
    /// </summary>
    string Key { get; }

    /// <summary>
    /// The value used to create the first section of the key. key Schema => key: [{primaryKey}Cache] 
    /// </summary>
    string PrimaryKey { get; }

    /// <summary>
    /// When used, this will create a secondary section of the key. key Schema => key: [{primaryKey}{secondaryKey}Cache]
    /// </summary>
    string? SecondaryKey { get; }

    /// <summary>
    /// Set the message parameter of the Cache key object
    /// </summary>
    /// <param name="message"></param>
    void SetMessage(string message);

    /// <summary>
    /// Start Operation Timer
    /// </summary>
    void StartOperation();

    /// <summary>
    /// Stop Operation Timer
    /// </summary>
    void StopOperation();
}