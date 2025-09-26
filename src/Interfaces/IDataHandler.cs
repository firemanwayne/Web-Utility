namespace Web.Utility.Interfaces;

using Web.Utility.Abstractions.ViewModels;

public interface IDataHandler<T> where T : IUpdateable
{
    /// <summary>
    /// Method for persisting a new instance of an entity.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<IResult> CreateAsync(ViewModelBase<T> entity);

    /// <summary>
    /// Method for persisting an existing instance of an entity.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<IResult> UpdateAsync(ViewModelBase<T> entity);

    /// <summary>
    /// Method for deleting an entity.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<IResult> DeleteAsync(ViewModelBase<T> entity);
}