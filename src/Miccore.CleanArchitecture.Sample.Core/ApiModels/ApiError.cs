namespace Miccore.CleanArchitecture.Sample.Core.ApiModels
{
    /// <summary>
    /// Api error model
    /// </summary>
    public class ApiError
    {
        public int Code
        {
            get;
            set;
        }

        public string? Message
        {
            get;
            set;
        }
    }
}