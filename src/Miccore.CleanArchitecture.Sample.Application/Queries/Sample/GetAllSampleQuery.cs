using MediatR;
using Miccore.CleanArchitecture.Sample.Application.Responses.Sample;
using Miccore.Pagination.Model;

namespace Miccore.CleanArchitecture.Sample.Application.Queries.Sample
{
    /// <summary>
    /// Sample query model
    /// </summary>
    public class GetAllSampleQuery : IRequest<PaginationModel<SampleResponse>>
    {
        public PaginationQuery query
        {
            get;
            set;
        }

        public GetAllSampleQuery(PaginationQuery query)
        {
            this.query = query;
        }
    }
}