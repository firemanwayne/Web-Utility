namespace Web.Utility.Interfaces;

using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Interface for pushing changes
/// </summary>
public interface IChangeProducer<T>
{
    /// <summary>
    /// Push change to reader
    /// </summary>
    /// <param name="entity">Changed entity</param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task PushAsync(T entity, CancellationToken token = default);
}

/// <summary>
/// Interface for pushing changes
/// </summary>
public interface IChangeProducer : IChangeProducer<IUpdateable>
{ }