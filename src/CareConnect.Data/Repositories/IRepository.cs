using System.Linq.Expressions;
using CareConnect.Domain.Commons;

namespace CareConnect.Data.Repositories;

public interface IRepository<T> where T : Auditable
{
    ValueTask<T> InsertAsync(T entity);
    ValueTask<T> UpdateAsync(T entity);
    ValueTask<T> DeleteAsync(T entity);
    ValueTask<T> DropAsync(T entity);
    ValueTask<T> SelectAsync(
        Expression<Func<T, bool>> expression,
        string[] includes = null);
    ValueTask<IEnumerable<T>> SelectAsEnumerable(
        Expression<Func<T, bool>> expression = null,
        string[] includes = null,
        bool isTracked = true);
    IQueryable<T> SelectAsQueryable(
         Expression<Func<T, bool>> expression = null,
         string[] includes = null,
         bool isTracked = true);
}
