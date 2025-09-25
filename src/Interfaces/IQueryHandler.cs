namespace Web.Utility.Interfaces;

using System.Linq.Expressions;

public interface IQueryHandler<T> where T : IUpdateable
{
    /// <summary>
    /// Get TData item by Id.
    /// </summary>
    /// <param name="id">Id of the item to retrieve</param>
    /// <param name="userId">Current User's Id</param>
    /// <returns></returns>
    Task<T?> GetByIdAsync(Guid id, string userId);

    /// <summary>
    /// Get all TData items
    /// </summary>
    /// <param name="userId">Current User's Id</param>
    /// <returns></returns>
    IAsyncEnumerable<T> GetAllAsync(string userId);

    /// <summary>
    /// Retrieves all TData items for a specified customer ID using paging.
    /// </summary>
    /// <param name="customerId">The customer ID to filter by.</param>
    /// <param name="userId">The current user ID for authentication.</param>
    /// <returns>An async enumerable of TData items.</returns>
    IAsyncEnumerable<T> GetAllByCustomerIdAsync(Guid customerId, string userId);

    /// <summary>
    /// Retrieves all TData items for a specified order ID using paging
    /// </summary>
    /// <param name="orderId">The order ID to filter by.</param>
    /// <param name="userId">The current user ID for authentication.</param>
    /// <returns>An async enumerable of TData items.</returns>
    IAsyncEnumerable<T> GetAllByOrderIdAsync(Guid orderId, string userId);

    /// <summary>
    /// Get all TData items filtered by a provided expression.
    /// </summary>
    /// <param name="predicate">Expression to filter items</param>
    /// <param name="userId">Current User's Id</param>
    /// <returns></returns>
    IAsyncEnumerable<T?> SearchByAsync(Expression<Func<T, bool>> predicate, string userId);
}
