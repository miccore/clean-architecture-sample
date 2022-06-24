using Miccore.CleanArchitecture.Sample.Core.Entities;
using Miccore.CleanArchitecture.Sample.Core.Enumerations;
using Miccore.CleanArchitecture.Sample.Core.Exceptions;
using Miccore.CleanArchitecture.Sample.Core.Repositories.Base;
using Miccore.CleanArchitecture.Sample.Infrastructure.Data;
using Miccore.Pagination.Model;
using Microsoft.Extensions.DependencyInjection;

namespace Miccore.CleanArchitecture.Sample.Infrastructure.Repositories.Base
{
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
        /// delete entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<T> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// get all entities paginated
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<PaginationModel<T>> GetAllAsync(PaginationQuery query)
        {
            var entities =  await _context.Set<T>().PaginateAsync(query);
            // remove all deleted
            entities.Items.RemoveAll(x => x.DeletedAt is not 0);
            
            return entities;
        }

        /// <summary>
        /// get unique entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            if(entity is null || entity.DeletedAt is not 0){
                throw new NotFoundException(ExceptionEnum.NOT_FOUND.ToString());
            }
            
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
    }
}
