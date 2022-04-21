using AutoMapper;

namespace Miccore.CleanArchitecture.Sample.Application.Mappers
{
    /// <summary>
    /// general class for profile adding
    /// </summary>
    public class SampleMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;

                #region profiles

                // sample mapping profile
                cfg.AddProfile<SampleMappingProfile>();

                #endregion
            });
            var mapper = config.CreateMapper();
            return mapper;
        });
        public static IMapper Mapper => Lazy.Value;
    }
}