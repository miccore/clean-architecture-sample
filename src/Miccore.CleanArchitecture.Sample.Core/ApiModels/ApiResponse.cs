namespace Miccore.CleanArchitecture.Sample.Core.ApiModels
{
    /// <summary>
    /// Api response model
    /// </summary>
    public class ApiResponse
    {
        public object? Data
        {
            get;
            set;
        }

        public IEnumerable<ApiError>? Errors
        {
            get;
            set;
        }
    }
}