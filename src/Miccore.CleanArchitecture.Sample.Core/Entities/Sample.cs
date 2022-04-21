using System.ComponentModel.DataAnnotations.Schema;

namespace Miccore.CleanArchitecture.Sample.Core.Entities
{
    /// <summary>
    /// Sample entity
    /// </summary>
    [Table("Samples")]
    public class Sample : BaseEntity
    {
        public string? Name
        {
            get;
            set;
        }

    }
}