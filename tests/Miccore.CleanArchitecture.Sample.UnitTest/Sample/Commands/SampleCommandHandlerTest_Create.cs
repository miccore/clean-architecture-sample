using System;
using System.Threading;
using FluentAssertions;
using Miccore.CleanArchitecture.Sample.Application.Commands.Sample;
using Miccore.CleanArchitecture.Sample.Application.Handlers.Sample.CommandHandlers;
using Miccore.CleanArchitecture.Sample.Core.Enumerations;
using Miccore.CleanArchitecture.Sample.Infrastructure.Repositories;
using Xunit;

namespace Miccore.CleanArchitecture.Sample.UnitTest.Sample.Commands
{
    public class SampleCommandHandlerTest_Create
    {
        /// <summary>
        /// mock class
        /// </summary>
        private readonly SampleMockClass _mock;

        /// <summary>
        /// initialisation of test objects
        /// </summary>
        public SampleCommandHandlerTest_Create(){
            _mock = new SampleMockClass();
        }

        /// <summary>
        /// test sample creation with entity mapping return null
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async void SampleCommandHandlerTest_Create_Invalid_Mapping(){
            // arrange
            var mockDb = _mock.GetDbContext().Object;
            var repository = new SampleRepository(mockDb);
            var handler = new CreateSampleCommandHandler(repository);

            var command = new CreateSampleCommand(){};
            command = null;

            // act
             var ex = await Assert.ThrowsAsync<ApplicationException>(() => handler.Handle(command, CancellationToken.None));

            // assert
            ex.Message.Should().BeEquivalentTo(ExceptionEnum.MAPPER_ISSUE.ToString());
        }

        /// <summary>
        /// test sample creation with successfull creation
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async void SampleCommandHandlerTest_Create_successful(){
            // arrange
            var mockDb = _mock.GetDbContext().Object;
            var repository = new SampleRepository(mockDb);
            var handler = new CreateSampleCommandHandler(repository);

            var command = new CreateSampleCommand(){
                Name = "Sample 1"
            };

            // act
             var result = await  handler.Handle(command, CancellationToken.None);

            // assert
            result.Should().NotBeNull();
            result.Name.Should().Be("Sample 1");
            result.CreatedAt.Should().NotBe(0);
            result.UpdatedAt.Should().BeNull();
            result.DeletedAt.Should().BeNull();
        }


        
    }
}