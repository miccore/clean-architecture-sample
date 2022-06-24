using System.ComponentModel;

namespace Miccore.CleanArchitecture.Sample.Core.Enumerations
{
    public enum ExceptionEnum
    {
        [Description("not found")]
        NOT_FOUND,

        [Description("Mapper Issue")]
        MAPPER_ISSUE,
        
        [Description("Sample not found")]
        SAMPLE_NOT_FOUND,
    }
}