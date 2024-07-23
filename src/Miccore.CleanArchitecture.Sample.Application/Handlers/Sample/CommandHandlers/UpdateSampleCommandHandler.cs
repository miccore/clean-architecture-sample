using MediatR;
using Miccore.CleanArchitecture.Sample.Application.Commands.Sample;
using Miccore.CleanArchitecture.Sample.Application.Mappers;
using Miccore.CleanArchitecture.Sample.Application.Responses.Sample;
using Miccore.CleanArchitecture.Sample.Core.Enumerations;
using Miccore.CleanArchitecture.Sample.Core.Repositories;

namespace Miccore.CleanArchitecture.Sample.Application.Handlers.Sample.CommandHandlers
{
    /// <summary>
    /// Update Sample Command Handler 
    /// </summary>
    public class UpdateSampleCommandHandler : IRequestHandler<UpdateSampleCommand, SampleResponse>
    {
        private readonly ISampleRepository _sampleRepository;

        public UpdateSampleCommandHandler(ISampleRepository sampleRepository)
        {
            _sampleRepository = sampleRepository;
        }


        /// <summary>
        /// Sample command Handle for updating element
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<SampleResponse> Handle(UpdateSampleCommand request, CancellationToken cancellationToken)
        {
            // map request with the entity
            var sampleEntity = SampleMapper.Mapper.Map<Core.Entities.Sample>(request);

            // check if it's mapped correctly
            if(sampleEntity is null){
                throw new ApplicationException(ExceptionEnum.MAPPER_ISSUE.ToString());
            }

            // add async with the repository
            var updatedSample = await _sampleRepository.UpdateAsync(sampleEntity);

            //map with the response
            var sampleResponse = SampleMapper.Mapper.Map<SampleResponse>(updatedSample);

            // return response
            return sampleResponse;
        }
    }
}