using System.Linq.Expressions;
using Miccore.CleanArchitecture.Sample.Core.Entities;
using Miccore.Pagination.Model;

namespace Miccore.CleanArchitecture.Sample.Core.Repositories.Base;

/// <summary>
/// core repository interface
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T> where T : BaseEntity
{
    Task<PaginationModel<T>> GetAllAsync(PaginationQuery query, params string[] includes);
    Task<T> GetByIdAsync(int id, params string[] includes);
    Task<PaginationModel<T>> GetAllByParametersPaginatedAsync(PaginationQuery query, Expression<Func<T, bool>> WhereExpression, params string[] includes);
    Task<List<T>> GetAllByParametersAsync(Expression<Func<T, bool>> WhereExpression, params string[] includes);
    Task<T> GetByParametersAsync(Expression<Func<T, bool>> WhereExpression, params string[] includes);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(int id);
    Task DeleteHardAsync(Expression<Func<T, bool>> WhereExpression);
    T SetValueForUpdateAsync(T entity, T context);
}