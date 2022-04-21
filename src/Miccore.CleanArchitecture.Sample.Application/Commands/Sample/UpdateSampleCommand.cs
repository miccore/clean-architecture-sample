using MediatR;
using Miccore.CleanArchitecture.Sample.Application.Responses.Sample;

namespace Miccore.CleanArchitecture.Sample.Application.Commands.Sample
{
    /// <summary>
    ///  Sample Command request
    /// </summary>
    public class UpdateSampleCommand : IRequest<SampleResponse>
    {
        public int Id
        {
            get;
            set;
        }

        public string? Name
        {
            get;
            set;
        }
    }

}