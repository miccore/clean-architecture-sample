using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Miccore.CleanArchitecture.Sample.Api.Controllers;
using Miccore.CleanArchitecture.Sample.Application.Handlers.Sample.QueryHandlers;
using Miccore.CleanArchitecture.Sample.Application.Mappers;
using Miccore.CleanArchitecture.Sample.Application.Queries.Sample;
using Miccore.CleanArchitecture.Sample.Application.Responses.Sample;
using Miccore.CleanArchitecture.Sample.Core.Repositories;
using Miccore.Pagination.Model;
using Moq;
using Xunit;
using System;
using Miccore.CleanArchitecture.Sample.Core.Utils;
using Miccore.CleanArchitecture.Sample.Infrastructure.Data;
using Miccore.CleanArchitecture.Sample.Infrastructure.Repositories;

namespace Miccore.CleanArchitecture.Sample.UnitTest.Sample.Queries
{
    
    /// <summary>
    /// sample query handler test class for get all samples
    /// </summary>
    public class SampleQueryTestHandler_GetAll
    {
        /// <summary>
        /// query element
        /// </summary>
        private GetAllSampleQuery _query;
        /// <summary>
        /// mock class
        /// </summary>
        private SampleMockClass _mock;

        /// <summary>
        /// initialisation of test objects
        /// </summary>
        public SampleQueryTestHandler_GetAll(){
            // query initialisation
            _query = new GetAllSampleQuery(new PaginationQuery());  
            _query.query.paginate = false;
            _query.query.page = 1;
            _query.query.limit = 10;

            // databse d=context
            _mock = new SampleMockClass();
            
        }

        /// <summary>
        /// get samples in empty list and assert return null
        /// </summary>
        [Fact]
        public async void SampleQueryTestHandler_GetAll_ReturnEmptyElements(){
            // arrange
            var mockDbContext = _mock.GetDbContext().Object;
            var repository = new SampleRepository(mockDbContext);
            var handler = new GetAllSampleQueryHandler(repository);

            //act
            // get servie data
            var handle = await handler.Handle(_query, CancellationToken.None);
            var result = SampleMapper.Mapper.Map<PaginationModel<SampleResponse>>(handle);

            // assert
            result.Items.Should().BeNullOrEmpty();
            result.Items.Count.Should().Be(0);
            result.CurrentPage.Should().Be(1);
            result.PageSize.Should().Be(10);
            result.TotalItems.Should().Be(0);
            result.TotalPages.Should().Be(0);
        }

        /// <summary>
        /// get samples in empty list and assert return null
        /// </summary>
        [Fact]
        public async void SampleQueryTestHandler_GetAll_ReturnListOfElements_NotPaginated(){
            //arrange
            for (int i = 0; i < 9; i++)
            {
                _mock._data.Add(
                        new Miccore.CleanArchitecture.Sample.Core.Entities.Sample(){
                            Id = i,
                            Name = "Sample " + i,
                            CreatedAt = DateUtils.GetCurrentTimeStamp(),
                            DeletedAt = 0,
                            UpdatedAt = 0
                        }
                );
            }
            _mock._data.Add(
                new Miccore.CleanArchitecture.Sample.Core.Entities.Sample(){
                    Id = 10,
                    Name = "Sample 10",
                    CreatedAt = DateUtils.GetCurrentTimeStamp(),
                    DeletedAt = DateUtils.GetCurrentTimeStamp(),
                    UpdatedAt = 0
                }
            );

            var mockDbContext = _mock.GetDbContext().Object;
            var repository = new SampleRepository(mockDbContext);
            var handler = new GetAllSampleQueryHandler(repository);
            _query.query.limit = 5;
           
            //act
            var handle = await handler.Handle(_query, CancellationToken.None);
            var result = SampleMapper.Mapper.Map<PaginationModel<SampleResponse>>(handle);
            
            
            // assert
            result.Items.Should().NotBeNullOrEmpty();
            result.Items.Count.Should().Be(9);
            result.CurrentPage.Should().Be(1);
            result.PageSize.Should().Be(5);
            result.TotalItems.Should().Be(9);
            result.TotalPages.Should().Be(2);
        }

        /// <summary>
        /// get samples in empty list and assert return null
        /// </summary>
        [Fact]
        public async void SampleQueryTestHandler_GetAll_ReturnListOfElements_Paginated(){
            //arrange
            for (int i = 0; i < 9; i++)
            {
                _mock._data.Add(
                        new Miccore.CleanArchitecture.Sample.Core.Entities.Sample(){
                            Id = i,
                            Name = "Sample " + i,
                            CreatedAt = DateUtils.GetCurrentTimeStamp(),
                            DeletedAt = 0,
                            UpdatedAt = 0
                        }
                );
            }
            _mock._data.Add(
                new Miccore.CleanArchitecture.Sample.Core.Entities.Sample(){
                    Id = 10,
                    Name = "Sample 10",
                    CreatedAt = DateUtils.GetCurrentTimeStamp(),
                    DeletedAt = DateUtils.GetCurrentTimeStamp(),
                    UpdatedAt = 0
                }
            );

            var mockDbContext = _mock.GetDbContext().Object;
            var repository = new SampleRepository(mockDbContext);
            var handler = new GetAllSampleQueryHandler(repository);
            _query.query.paginate = true;
            _query.query.limit = 5;
           
            //act
            var handle = await handler.Handle(_query, CancellationToken.None);
            var result = SampleMapper.Mapper.Map<PaginationModel<SampleResponse>>(handle);
            
            
            // assert
            result.Items.Should().NotBeNullOrEmpty();
            result.Items.Count.Should().Be(5);
            result.CurrentPage.Should().Be(1);
            result.PageSize.Should().Be(5);
            result.TotalItems.Should().Be(9);
            result.TotalPages.Should().Be(2);
        }

        
    }
}