using System.Linq.Expressions;
using CareConnect.Domain.Commons;

namespace CareConnect.Data.Repositories;

public interface IRepository<T> where T : Auditable
{
    Task<T> InsertAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
    Task<T> DropAsync(T entity);
    Task<T> SelectAsync(
        Expression<Func<T, bool>> expression,
        string[] includes = null);
    Task<IEnumerable<T>> SelectAsEnumerable(
        Expression<Func<T, bool>> expression = null,
        string[] includes = null,
        bool isTracked = true);
    IQueryable<T> SelectAsQueryable(
         Expression<Func<T, bool>> expression = null,
         string[] includes = null,
         bool isTracked = true);
}
