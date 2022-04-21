using MediatR;
using Miccore.CleanArchitecture.Sample.Application.Responses.Sample;

namespace Miccore.CleanArchitecture.Sample.Application.Commands.Sample
{
    public class DeleteSampleCommand : IRequest<SampleResponse>
    {

        public int Id
        {
            get;
            set;
        }

        public DeleteSampleCommand(int Id)
        {
            this.Id = Id;
        }
    }
}