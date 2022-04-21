using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Miccore.CleanArchitecture.Sample.Core.Utils;

namespace Miccore.CleanArchitecture.Sample.Core.Entities
{
    /// <summary>
    /// Base entity
    /// </summary>
    public abstract class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get;
            set;
        }

        [DefaultValue(null)]
        public int CreatedAt
        {
            get;
            set;
        } = DateUtils.GetCurrentTimeStamp();

        [DefaultValue(null)]
        public int UpdatedAt
        {
            get;
            set;
        }

        [DefaultValue(null)]
        public int DeletedAt
        {
            get;
            set;
        }
    }
}