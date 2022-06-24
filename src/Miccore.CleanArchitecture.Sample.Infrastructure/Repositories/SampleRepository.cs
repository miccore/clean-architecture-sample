using Miccore.CleanArchitecture.Sample.Core.Enumerations;
using Miccore.CleanArchitecture.Sample.Core.Exceptions;
using Miccore.CleanArchitecture.Sample.Core.Repositories;
using Miccore.CleanArchitecture.Sample.Core.Utils;
using Miccore.CleanArchitecture.Sample.Infrastructure.Data;
using Miccore.CleanArchitecture.Sample.Infrastructure.Repositories.Base;

namespace Miccore.CleanArchitecture.Sample.Infrastructure.Repositories
{
    public class SampleRepository : Repository<Miccore.CleanArchitecture.Sample.Core.Entities.Sample>, ISampleRepository
    {
        /// <summary>
        /// Sample repository
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public SampleRepository(SampleApplicationDbContext context) : base(context) { }
        
        /// <summary>
        /// soft delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public new async Task DeleteAsync(int id)
        {
            var entity = await _context.Samples.FindAsync(id);
            if (entity is null)
            {
                throw new NotFoundException(ExceptionEnum.SAMPLE_NOT_FOUND.ToString());
            }
            entity.DeletedAt = DateUtils.GetCurrentTimeStamp();
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// update sample entity
        /// </summary>
        /// <param name="sample"></param>
        /// <returns></returns>
        public new async Task<Miccore.CleanArchitecture.Sample.Core.Entities.Sample> UpdateAsync(Miccore.CleanArchitecture.Sample.Core.Entities.Sample entity)
        {
            var sample = await _context.Samples.FindAsync(entity.Id);
            if (sample is null || entity.DeletedAt is not 0)
            {
                throw new NotFoundException(ExceptionEnum.SAMPLE_NOT_FOUND.ToString());
            }

            sample.UpdatedAt = DateUtils.GetCurrentTimeStamp();
            await _context.SaveChangesAsync();

            return sample;
        }

    }
}