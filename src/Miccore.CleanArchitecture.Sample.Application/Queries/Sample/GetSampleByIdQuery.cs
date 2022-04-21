using MediatR;
using Miccore.CleanArchitecture.Sample.Application.Responses.Sample;

namespace Miccore.CleanArchitecture.Sample.Application.Queries.Sample
{
    public class GetSampleByIdQuery : IRequest<SampleResponse>
    {
        public int Id
        {
            get;
            set;
        }

        public GetSampleByIdQuery(int Id)
        {
            this.Id = Id;
        }
    }
}