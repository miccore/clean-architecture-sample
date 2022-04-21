using System.ComponentModel;

namespace Miccore.CleanArchitecture.Sample.Core.Enumerations
{
    public enum ExceptionEnum
    {
        [Description("Mapper Issue")]
        MAPPER_ISSUE,

        #region enums

        [Description("Sample not found")]
        SAMPLE_NOT_FOUND,

        #endregion
    }
}