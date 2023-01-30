using Microsoft.EntityFrameworkCore;

namespace Miccore.CleanArchitecture.Sample.Infrastructure.Data
{
    /// <summary>
    /// Database context registration
    /// </summary>
    public class SampleApplicationDbContext : DbContext
    {
        public SampleApplicationDbContext(DbContextOptions<SampleApplicationDbContext> options) : base(options) { }

        #region dbset

        public  DbSet<Miccore.CleanArchitecture.Sample.Core.Entities.Sample> Samples
        {
            get;
            set;
        }

        #endregion
    }
}