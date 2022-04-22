using MediatR;
using Miccore.CleanArchitecture.Sample.Application.Mappers;
using Miccore.CleanArchitecture.Sample.Application.Queries.Sample;
using Miccore.CleanArchitecture.Sample.Application.Responses.Sample;
using Miccore.CleanArchitecture.Sample.Core.Repositories;
using Miccore.Pagination.Model;

namespace Miccore.CleanArchitecture.Sample.Application.Handlers.Sample.QueryHandlers
{
    /// <summary>
    /// query handler
    /// </summary>
    public class GetAllSampleQueryHandler : IRequestHandler<GetAllSampleQuery, PaginationModel<SampleResponse>>
    {
        private readonly ISampleRepository _sampleRepository;

        public GetAllSampleQueryHandler(ISampleRepository sampleRepository)
        {
            this._sampleRepository = sampleRepository;
        }

        /// <summary>
        /// handle for getting all sample paginated or not
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<PaginationModel<SampleResponse>> Handle(GetAllSampleQuery request, CancellationToken cancellationToken)
        {
            // get items
            var entities =  await _sampleRepository.GetAllAsync(request.query);
            
            // mapping with response
            var responses = SampleMapper.Mapper.Map<PaginationModel<SampleResponse>>(entities);

            // return response
            return responses;
        }
    }
}