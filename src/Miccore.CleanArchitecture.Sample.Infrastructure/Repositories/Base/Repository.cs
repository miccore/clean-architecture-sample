using Miccore.CleanArchitecture.Sample.Core.Entities;
using Miccore.CleanArchitecture.Sample.Core.Enumerations;
using Miccore.CleanArchitecture.Sample.Core.Exceptions;
using Miccore.CleanArchitecture.Sample.Core.Repositories.Base;
using Miccore.CleanArchitecture.Sample.Core.Utils;
using Miccore.CleanArchitecture.Sample.Infrastructure.Data;
using Miccore.Pagination.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace Miccore.CleanArchitecture.Sample.Infrastructure.Repositories.Base;

/// <summary>
/// implementation class of  core repository interface
/// </summary>
/// <typeparam name="T"></typeparam>
public class Repository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly SampleApplicationDbContext _context;

    public Repository(SampleApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// add entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// soft delete entity
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<T> DeleteAsync(int id)
    {
        var entity = await _context.Set<T>()
                                    .FirstOrDefaultAsync(x => x.Id == id && (x.DeletedAt == 0 || x.DeletedAt == null)) ?? throw new NotFoundException(ExceptionEnum.NOT_FOUND.ToString());
        
        entity.DeletedAt = DateUtils.GetCurrentTimeStamp();
        
        await _context.SaveChangesAsync();

        return entity;
    }

    /// <summary>
    /// soft delete entity
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteHardAsync(Expression<Func<T, bool>> WhereExpression)
    {
        var entity = await _context.Set<T>()
                                .Where(WhereExpression)
                                .Where(x => x.DeletedAt == 0 || x.DeletedAt == null)
                                .ToListAsync();

        _context.RemoveRange(entity);

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// get all entities paginated
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public async Task<PaginationModel<T>> GetAllAsync(PaginationQuery query, params string[] includes)
    {
        var entities =  await _context.Set<T>()
                                    .IncludeMultiple(includes)
                                    .Where(x => x.DeletedAt == 0 || x.DeletedAt == null)
                                    .PaginateAsync(query);
    
        return entities;
    }

    /// <summary>
    /// get unique entity
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<T> GetByIdAsync(int id, params string[] includes)
    {
        var entity = await _context.Set<T>()
                                    .IncludeMultiple(includes)
                                    .FirstOrDefaultAsync(x => x.Id == id && (x.DeletedAt == 0 || x.DeletedAt == null)) ?? throw new NotFoundException(ExceptionEnum.NOT_FOUND.ToString());
        return entity;
    }

    /// <summary>
    /// get all entities paginated by passing parameters paginated values
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public async Task<PaginationModel<T>> GetAllByParametersPaginatedAsync(PaginationQuery query, Expression<Func<T, bool>> WhereExpression, params string[] includes)
    {
        var entities =  await _context.Set<T>()
                                    .IncludeMultiple(includes)
                                    .Where(WhereExpression)
                                    .Where(x => x.DeletedAt == 0 || x.DeletedAt == null)
                                    .PaginateAsync(query);
        return entities;
    }

    /// <summary>
    /// get all entities paginated by passing parameters
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public async Task<List<T>> GetAllByParametersAsync(Expression<Func<T, bool>> WhereExpression, params string[] includes)
    {
        var entities =  await _context.Set<T>()
                                    .IncludeMultiple(includes)
                                    .Where(WhereExpression).Where(x => x.DeletedAt == 0 || x.DeletedAt == null)
                                    .ToListAsync();
        return entities;
    }

    /// <summary>
    /// get unique entity by passing parameters
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<T> GetByParametersAsync(Expression<Func<T, bool>> WhereExpression, params string[] includes)
    {
        var entity = await _context.Set<T>()
                                    .IncludeMultiple(includes)
                                    .Where(WhereExpression)
                                    .FirstOrDefaultAsync(x => x.DeletedAt == 0 || x.DeletedAt == null);
        
        return entity;
    }


    /// <summary>
    /// update entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public Task<T> UpdateAsync(T entity)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// set value for update
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public T SetValueForUpdateAsync(T entity, T context)        
    {
        foreach (var property in entity.GetType().GetProperties())
        {
            var prop = property.GetValue(entity);
            var con = context.GetType().GetProperty(property.Name);

            if(prop is null) continue;

            if(prop != con.GetValue(context))
                con.SetValue(context, prop);
        }

        return context;
    }
}
