using MediatR;
using Miccore.CleanArchitecture.Sample.Application.Mappers;
using Miccore.CleanArchitecture.Sample.Application.Queries.Sample;
using Miccore.CleanArchitecture.Sample.Application.Responses.Sample;
using Miccore.CleanArchitecture.Sample.Core.Enumerations;
using Miccore.CleanArchitecture.Sample.Core.Exceptions;
using Miccore.CleanArchitecture.Sample.Core.Repositories;

namespace Miccore.CleanArchitecture.Sample.Application.Handlers.Sample.QueryHandlers
{   
    /// <summary>
    /// get sample by id
    /// </summary>
    public class GetSampleByIdQueryHandler : IRequestHandler<GetSampleByIdQuery, SampleResponse>
    {
        private readonly ISampleRepository _sampleRepository;
        public GetSampleByIdQueryHandler(ISampleRepository sampleRepository)
        {
            _sampleRepository = sampleRepository;
        }

        /// <summary>
        /// getting sample by id query
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<SampleResponse> Handle(GetSampleByIdQuery request, CancellationToken cancellationToken)
        {
            // get entity by id
            var entity = await _sampleRepository.GetByIdAsync(request.Id);
          
            // mapping response
            var response = SampleMapper.Mapper.Map<SampleResponse>(entity);

            // return object
            return response;
        }
    }
}