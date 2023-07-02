using System.Linq.Expressions;
using Miccore.CleanArchitecture.Sample.Core.Entities;
using Miccore.Pagination.Model;

namespace Miccore.CleanArchitecture.Sample.Core.Repositories.Base
{
    /// <summary>
    /// core repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : BaseEntity
    {
        Task<PaginationModel<T>> GetAllAsync(PaginationQuery query);
        Task<T> GetByIdAsync(int id);
        Task<PaginationModel<T>> GetAllByParametersPaginatedAsync(PaginationQuery query, Expression<Func<T, bool>> WhereExpression);
        Task<List<T>> GetAllByParametersAsync(Expression<Func<T, bool>> WhereExpression);
        Task<T> GetByParametersAsync(Expression<Func<T, bool>> WhereExpression);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(int id);
    }
} 