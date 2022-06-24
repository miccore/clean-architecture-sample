using System.ComponentModel;

namespace Miccore.CleanArchitecture.Sample.Core.Enumerations
{
    public enum ValidatorEnum
    {
        [Description("Not_EMPTY")]
        NOT_EMPTY,
        [Description("Not_NULL")]
        NOT_NULL,
    }
}