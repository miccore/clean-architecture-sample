using Miccore.CleanArchitecture.Sample.Core.Entities;
using Miccore.CleanArchitecture.Sample.Core.Enumerations;
using Miccore.CleanArchitecture.Sample.Core.Exceptions;
using Miccore.CleanArchitecture.Sample.Core.Repositories.Base;
using Miccore.CleanArchitecture.Sample.Core.Utils;
using Miccore.CleanArchitecture.Sample.Infrastructure.Data;
using Miccore.Pagination.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

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
        /// soft delete entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id && x.DeletedAt == 0);
            if (entity is null)
            {
                throw new NotFoundException(ExceptionEnum.NOT_FOUND.ToString());
            }
            entity.DeletedAt = DateUtils.GetCurrentTimeStamp();
            await _context.SaveChangesAsync();

            return entity;
        }

        /// <summary>
        /// get all entities paginated
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<PaginationModel<T>> GetAllAsync(PaginationQuery query)
        {
            var entities =  await _context.Set<T>()
                                            .Where(x => x.DeletedAt == 0)
                                            .PaginateAsync(query);
            
            return entities;
        }

        /// <summary>
        /// get unique entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id && x.DeletedAt == 0);

            if(entity is null){
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
