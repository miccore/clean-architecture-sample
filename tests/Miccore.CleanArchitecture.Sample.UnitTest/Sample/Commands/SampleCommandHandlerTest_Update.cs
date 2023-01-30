using System;
using System.Threading;
using FluentAssertions;
using Miccore.CleanArchitecture.Sample.Application.Commands.Sample;
using Miccore.CleanArchitecture.Sample.Application.Handlers.Sample.CommandHandlers;
using Miccore.CleanArchitecture.Sample.Core.Enumerations;
using Miccore.CleanArchitecture.Sample.Core.Exceptions;
using Miccore.CleanArchitecture.Sample.Core.Utils;
using Miccore.CleanArchitecture.Sample.Infrastructure.Repositories;
using Xunit;

namespace Miccore.CleanArchitecture.Sample.UnitTest.Sample.Commands
{
    public class SampleCommandHandlerTest_Update
    {
        /// <summary>
        /// mock class
        /// </summary>
        private readonly SampleMockClass _mock;

        /// <summary>
        /// initialisation
        /// </summary>
        public SampleCommandHandlerTest_Update(){
            _mock = new SampleMockClass();
        }

        /// <summary>
        /// test sample update with entity mapping return null
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async void SampleCommandHandlerTest_Update_Invalid_Mapping(){
            // arrange
            var mockDb = _mock.GetDbContext().Object;
            var repository = new SampleRepository(mockDb);
            var handler = new UpdateSampleCommandHandler(repository);

            var command = new UpdateSampleCommand(){};
            command = null;

            // act
             var ex = await Assert.ThrowsAsync<ApplicationException>(() => handler.Handle(command, CancellationToken.None));

            // assert
            ex.Message.Should().BeEquivalentTo(ExceptionEnum.MAPPER_ISSUE.ToString());
        }

        /// <summary>
        /// test update method with not found id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(1)]
        public async void SampleCommandHandlerTest_Update_not_found(int id){
            // arrange
            var mockDb = _mock.GetDbContext().Object;
            var repository = new SampleRepository(mockDb);
            var handler = new UpdateSampleCommandHandler(repository);
            var command = new UpdateSampleCommand(){
                Id = id
            };

            // act
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));

            // assert
            ex.Message.Should().BeEquivalentTo(ExceptionEnum.SAMPLE_NOT_FOUND.ToString());
        }

        /// <summary>
        /// test update method with deleted value
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(1)]
        public async void SampleCommandHandlerTest_Update_deleted(int id){
            // arrange
            _mock._data.Add(
                new Miccore.CleanArchitecture.Sample.Core.Entities.Sample(){
                    Id = 1,
                    Name = "Sample 1",
                    CreatedAt = DateUtils.GetCurrentTimeStamp(),
                    DeletedAt = DateUtils.GetCurrentTimeStamp(),
                    UpdatedAt = 0
                }
            );
            var mockDb = _mock.GetDbContext().Object;
            var repository = new SampleRepository(mockDb);
            var handler = new UpdateSampleCommandHandler(repository);
            var command = new UpdateSampleCommand(){
                Id = id
            };

            // act
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));

            // assert
            ex.Message.Should().BeEquivalentTo(ExceptionEnum.SAMPLE_NOT_FOUND.ToString());
        }

        /// <summary>
        /// test update method with successfull result
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(1)]
        public async void SampleCommandHandlerTest_Update_successfull(int id){
            // arrange
            _mock._data.Add(
                new Miccore.CleanArchitecture.Sample.Core.Entities.Sample(){
                    Id = 1,
                    Name = "Sample 1",
                    CreatedAt = DateUtils.GetCurrentTimeStamp(),
                    DeletedAt = 0,
                    UpdatedAt = 0
                }
            );
            var mockDb = _mock.GetDbContext().Object;
            var repository = new SampleRepository(mockDb);
            var handler = new UpdateSampleCommandHandler(repository);
            var command = new UpdateSampleCommand(){
                Id = id,
                Name = "Sample 1 updated"
            };

            // act
            var result = await  handler.Handle(command, CancellationToken.None);

            // assert
           result.Id.Should().Be(id);
           result.Name.Should().Be("Sample 1 updated");
           result.UpdatedAt.Should().NotBe(0);
        }
    }
}