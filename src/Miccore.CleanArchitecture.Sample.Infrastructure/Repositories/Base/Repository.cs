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
    public class Repository<T> : IRepository<T> where T : class
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// delete entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> DeleteAsync(int id)
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
            return await _context.Set<T>().PaginateAsync(query);
        }

        /// <summary>
        /// get unique entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if(entity is null){
                throw new NotFoundException(ExceptionEnum.SAMPLE_NOT_FOUND.ToString());
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
