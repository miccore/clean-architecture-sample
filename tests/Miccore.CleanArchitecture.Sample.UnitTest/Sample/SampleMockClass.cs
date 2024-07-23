using System.Collections.Generic;
using Miccore.CleanArchitecture.Sample.Core.Utils;
using Miccore.CleanArchitecture.Sample.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace Miccore.CleanArchitecture.Sample.UnitTest.Sample
{
    public class SampleMockClass
    {   

        public List<Core.Entities.Sample> _data;

        public SampleMockClass(){
            // iquerable data
            _data = new List<Core.Entities.Sample>(){};
        }

        /// <summary>
        /// mock database context with empty data
        /// </summary>
        /// <returns></returns>
         public Mock<SampleApplicationDbContext> GetDbContext(){
            
            // context database setup
            var options = new DbContextOptionsBuilder<SampleApplicationDbContext>().Options;
            var mockDbContext = new Mock<SampleApplicationDbContext>(options);
            mockDbContext.SetupSequence(x => x.Set<Core.Entities.Sample>())
                        .ReturnsDbSet(_data);
            
            // return mock
            return mockDbContext;
        }

        /// <summary>
        /// générate data from samples
        /// </summary>
        /// <param name="size"></param> <summary>
        public void GenerateData(int size){

            for (int i = 1; i <= 9; i++)
            {
                _data.Add(
                        new Core.Entities.Sample(){
                            Id = i,
                            Name = "Sample " + i,
                            CreatedAt = DateUtils.GetCurrentTimeStamp(),
                            DeletedAt = 0,
                            UpdatedAt = 0
                        }
                );
            }
        }

    }
}