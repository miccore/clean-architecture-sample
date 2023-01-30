using System.Collections.Generic;
using System.Linq;
using Miccore.CleanArchitecture.Sample.Core.Utils;
using Miccore.CleanArchitecture.Sample.Infrastructure.Data;
using Miccore.Pagination.Model;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace Miccore.CleanArchitecture.Sample.UnitTest.Sample
{
    public class SampleMockClass
    {   

        public List<Miccore.CleanArchitecture.Sample.Core.Entities.Sample> _data;

        public SampleMockClass(){
            // iquerable data
            _data = new List<Core.Entities.Sample>(){
               
            };
        }

        /// <summary>
        /// mock database context with empty data
        /// </summary>
        /// <returns></returns>
         public Mock<SampleApplicationDbContext> GetDbContext(){
            
            // context database setup
            var options = new DbContextOptionsBuilder<SampleApplicationDbContext>().Options;
            var mockDbContext = new Mock<SampleApplicationDbContext>(options);
            mockDbContext.SetupSequence(x => x.Set<Miccore.CleanArchitecture.Sample.Core.Entities.Sample>())
                        .ReturnsDbSet(_data);
            
            // return mock
            return mockDbContext;
        }

    }
}