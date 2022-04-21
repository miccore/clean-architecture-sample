using MediatR;
using Miccore.CleanArchitecture.Sample.Application.Commands.Sample;
using Miccore.CleanArchitecture.Sample.Application.Mappers;
using Miccore.CleanArchitecture.Sample.Application.Responses.Sample;
using Miccore.CleanArchitecture.Sample.Core.Enumerations;
using Miccore.CleanArchitecture.Sample.Core.Repositories;

namespace Miccore.CleanArchitecture.Sample.Application.Handlers.Sample.CommandHandlers
{
    /// <summary>
    /// Create Sample Command Handler 
    /// </summary>
    public class CreateSampleCommandHandler : IRequestHandler<CreateSampleCommand, SampleResponse>
    {
        private readonly ISampleRepository _sampleRepository;

        public CreateSampleCommandHandler(ISampleRepository sampleRepository)
        {
            _sampleRepository = sampleRepository;
        }


        /// <summary>
        /// Sample command Handle for adding new element
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<SampleResponse> Handle(CreateSampleCommand request, CancellationToken cancellationToken)
        {
            // map request with the entity
            var sampleEntity = SampleMapper.Mapper.Map<Miccore.CleanArchitecture.Sample.Core.Entities.Sample>(request);

            // check if it's mapped correctly
            if(sampleEntity is null){
                throw new ApplicationException(ExceptionEnum.MAPPER_ISSUE.ToString());
            }

            // add async with the repository
            var addedSample = await _sampleRepository.AddAsync(sampleEntity);

            //map with the response
            var sampleResponse = SampleMapper.Mapper.Map<SampleResponse>(addedSample);

            // return response
            return sampleResponse;
        }
    }
}