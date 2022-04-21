using MediatR;
using Miccore.CleanArchitecture.Sample.Application.Responses.Sample;

namespace Miccore.CleanArchitecture.Sample.Application.Commands.Sample
{
    /// <summary>
    ///  Sample Command request
    /// </summary>
    public class CreateSampleCommand : IRequest<SampleResponse>
    {
        public string? Name
        {
            get;
            set;
        }
    }

}