namespace Miccore.CleanArchitecture.Sample.Core.Exceptions
{
    /// <summary>
    /// throw exception for validations handling
    /// </summary>
    [Serializable]
    public class ValidatorException : Exception
    {
        public ValidatorException(){}
        public ValidatorException(string message) : base(message) {}
        public ValidatorException(string message, Exception inner) : base(message, inner) {}
    }
}