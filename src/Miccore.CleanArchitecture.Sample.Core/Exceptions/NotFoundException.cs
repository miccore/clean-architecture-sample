namespace Miccore.CleanArchitecture.Sample.Core.Exceptions
{
    /// <summary>
    /// not found resource exception
    /// </summary>
    [Serializable]
    public class NotFoundException : Exception
    {
        public NotFoundException(){}
        public NotFoundException(string message) : base(message) {}
        public NotFoundException(string message, Exception inner) : base(message, inner) {}
        
    }
}