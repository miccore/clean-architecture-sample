using Miccore.CleanArchitecture.Sample.Core.Enumerations;
using Miccore.CleanArchitecture.Sample.Core.Exceptions;
using Miccore.CleanArchitecture.Sample.Core.Repositories;
using Miccore.CleanArchitecture.Sample.Core.Utils;
using Miccore.CleanArchitecture.Sample.Infrastructure.Data;
using Miccore.CleanArchitecture.Sample.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Miccore.CleanArchitecture.Sample.Infrastructure.Repositories
{
    public class SampleRepository : Repository<Core.Entities.Sample>, ISampleRepository
    {
        /// <summary>
        /// Sample repository
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public SampleRepository(SampleApplicationDbContext context) : base(context) { }

        /// <summary>
        /// update sample entity
        /// </summary>
        /// <param name="sample"></param>
        /// <returns></returns>
        public new async Task<Core.Entities.Sample> UpdateAsync(Core.Entities.Sample entity)
        {
            var sample = await _context.Set<Core.Entities.Sample>().FirstOrDefaultAsync(x => x.Id == entity.Id && (x.DeletedAt == 0 || x.DeletedAt == null)) ?? throw new NotFoundException(ExceptionEnum.SAMPLE_NOT_FOUND.ToString());

            sample = SetValueForUpdateAsync(entity, sample);
            sample.UpdatedAt = DateUtils.GetCurrentTimeStamp();
            await _context.SaveChangesAsync();

            return sample;
        }

    }
}