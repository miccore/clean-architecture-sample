using System.Threading;
using FluentAssertions;
using Miccore.CleanArchitecture.Sample.Application.Handlers.Sample.QueryHandlers;
using Miccore.CleanArchitecture.Sample.Application.Queries.Sample;
using Miccore.CleanArchitecture.Sample.Core.Enumerations;
using Miccore.CleanArchitecture.Sample.Core.Exceptions;
using Miccore.CleanArchitecture.Sample.Core.Utils;
using Miccore.CleanArchitecture.Sample.Infrastructure.Repositories;
using Xunit;

namespace Miccore.CleanArchitecture.Sample.UnitTest.Sample.Queries
{
    public class SampleQueryTestHandler_GetById
    {
        
        /// <summary>
        /// mock class
        /// </summary>
        private SampleMockClass _mock;
        private readonly SampleRepository _repository;
        private readonly GetSampleByIdQueryHandler _handler;

        /// <summary>
        /// initialisation of test objects
        /// </summary>
        public SampleQueryTestHandler_GetById(){
            // databse d=context
            _mock = new SampleMockClass();
            
            var mockDb = _mock.GetDbContext().Object;
            
            _repository = new SampleRepository(mockDb);
            
            _handler = new GetSampleByIdQueryHandler(_repository);
        }


        /// <summary>
        /// get sample by id and throw not found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(1)]
        public async void SampleQueryTestHandler_GetById_throw_not_found(int id){
            // arrange
            var request = new GetSampleByIdQuery(id);

            // act
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(request, CancellationToken.None));

            // assert
            ex.Message.Should().BeEquivalentTo(ExceptionEnum.NOT_FOUND.ToString());
        }


        /// <summary>
        /// get sample by id and throw not found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(1)]
        public async void SampleQueryTestHandler_GetById_throw_not_found_with_Data_Deleted(int id){
            // arrange
            _mock._data.Add(
                new Core.Entities.Sample(){
                    Id = 1,
                    Name = "Sample 1",
                    CreatedAt = DateUtils.GetCurrentTimeStamp(),
                    DeletedAt = DateUtils.GetCurrentTimeStamp(),
                    UpdatedAt = 0
                }
            );
            var request = new GetSampleByIdQuery(id);

            // act
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(request, CancellationToken.None));

            // assert
            ex.Message.Should().BeEquivalentTo(ExceptionEnum.NOT_FOUND.ToString());
        }


        /// <summary>
        /// get sample by id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(12)]
        public async void SampleQueryTestHandler_GetById_found(int id){
            // arrange
            _mock._data.Add(
                new Core.Entities.Sample(){
                    Id = 12,
                    Name = "Sample 2",
                    CreatedAt = DateUtils.GetCurrentTimeStamp(),
                    DeletedAt = 0,
                    UpdatedAt = 0
                }
            );
            var request = new GetSampleByIdQuery(id);

            // act
            var result = await _handler.Handle(request, CancellationToken.None);

            // assert
            result.Should().NotBeNull();
            result.Name.Should().BeEquivalentTo("Sample 2");
        }
    }
}