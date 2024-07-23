using MediatR;
using Miccore.CleanArchitecture.Sample.Application.Commands.Sample;
using Miccore.CleanArchitecture.Sample.Application.Mappers;
using Miccore.CleanArchitecture.Sample.Application.Responses.Sample;
using Miccore.CleanArchitecture.Sample.Core.Repositories;

namespace Miccore.CleanArchitecture.Sample.Application.Handlers.Sample.CommandHandlers
{
    /// <summary>
    /// Delete Sample Command Handler 
    /// </summary>
    public class DeleteSampleCommandHandler : IRequestHandler<DeleteSampleCommand, SampleResponse>
    {
        private readonly ISampleRepository _sampleRepository;

        public DeleteSampleCommandHandler(ISampleRepository sampleRepository)
        {
            _sampleRepository = sampleRepository;
        }


        /// <summary>
        /// Sample command Handle for delete element
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<SampleResponse> Handle(DeleteSampleCommand request, CancellationToken cancellationToken)
        {
            // add async with the repository
            var deletedSample = await _sampleRepository.DeleteAsync(request.Id);

            //map with the response
            var sampleResponse = SampleMapper.Mapper.Map<SampleResponse>(deletedSample);

            // return response
            return sampleResponse;
        }
    }
}