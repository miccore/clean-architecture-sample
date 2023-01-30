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
    public class SampleCommandHandlerTest_Delete
    {
        /// <summary>
        /// mock class
        /// </summary>
        private readonly SampleMockClass _mock;

        /// <summary>
        /// initialisation
        /// </summary>
        public SampleCommandHandlerTest_Delete(){
            _mock = new SampleMockClass();
        }

        /// <summary>
        /// test delete method with not found id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(1)]
        public async void SampleCommandHandlerTest_Delete_not_found(int id){
            // arrange
            var mockDb = _mock.GetDbContext().Object;
            var repository = new SampleRepository(mockDb);
            var handler = new DeleteSampleCommandHandler(repository);
            var command = new DeleteSampleCommand(id);

            // act
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));

            // assert
            ex.Message.Should().BeEquivalentTo(ExceptionEnum.NOT_FOUND.ToString());
        }

        /// <summary>
        /// test delete method with already deleted value
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(1)]
        public async void SampleCommandHandlerTest_Delete_already_deleted(int id){
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
            var handler = new DeleteSampleCommandHandler(repository);
            var command = new DeleteSampleCommand(id);

            // act
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));

            // assert
            ex.Message.Should().BeEquivalentTo(ExceptionEnum.NOT_FOUND.ToString());
        }

        /// <summary>
        /// test delete method with successfull result
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(1)]
        public async void SampleCommandHandlerTest_Delete_successful(int id){
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
            var handler = new DeleteSampleCommandHandler(repository);
            var command = new DeleteSampleCommand(id);

            // act
            var result =  await handler.Handle(command, CancellationToken.None);

            // assert
            result.DeletedAt.Should().NotBe(0);
        }
    }
}