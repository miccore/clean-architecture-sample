namespace Miccore.CleanArchitecture.Sample.Core.Utils
{
    /// <summary>
    /// date utils class for all value of dates
    /// </summary>
    public static class DateUtils
    {
        /// <summary>
        /// get current timestamps int
        /// </summary>
        /// <returns></returns>
        public static int GetCurrentTimeStamp(){
            return (int)DateTime.UtcNow.Subtract(new DateTime(1970, 01, 01, 0, 0, 0)).TotalSeconds;
        }

    }
}