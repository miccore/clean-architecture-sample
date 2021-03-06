using AutoMapper;
using Miccore.CleanArchitecture.Sample.Application.Commands.Sample;
using Miccore.CleanArchitecture.Sample.Application.Responses.Sample;
using Miccore.Pagination.Model;

namespace Miccore.CleanArchitecture.Sample.Application.Mappers
{
    /// <summary>
    /// Sample Mapping Profile map creation
    /// </summary>
    public class SampleMappingProfile : Profile
    {
        public SampleMappingProfile()
        {
            #region createmap

            // sample response
            CreateMap<Miccore.CleanArchitecture.Sample.Core.Entities.Sample, SampleResponse>().ReverseMap();
            // sample create
            CreateMap<Miccore.CleanArchitecture.Sample.Core.Entities.Sample, CreateSampleCommand>().ReverseMap();
            // sample update
            CreateMap<Miccore.CleanArchitecture.Sample.Core.Entities.Sample, UpdateSampleCommand>().ReverseMap();
            // sample response pagination
            CreateMap<PaginationModel<Miccore.CleanArchitecture.Sample.Core.Entities.Sample>, PaginationModel<SampleResponse>>().ReverseMap();

            #endregion
        }
    }
}