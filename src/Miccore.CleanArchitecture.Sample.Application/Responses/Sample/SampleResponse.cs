using Miccore.CleanArchitecture.Sample.Core.Entities;

namespace Miccore.CleanArchitecture.Sample.Application.Responses.Sample
{
    /// <summary>
    /// Sample response
    /// </summary>
    public class SampleResponse : BaseEntity
    {
         public string? Name
        {
            get;
            set;
        }
    }
}