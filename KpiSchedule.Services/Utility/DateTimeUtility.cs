namespace KpiSchedule.Services.Utility
{
    /// <summary>
    /// Utility class for timestamp conversion.
    /// </summary>
    public static class DateTimeUtility
    {
        /// <summary>
        /// Convert UNIX seconds timestamp to <see cref="DateTime"/>
        /// </summary>
        /// <param name="unixSecondsTimeStamp">Unix timestamp in seconds.</param>
        /// <returns>DateTime.</returns>
        public static DateTime UnixTimestampToDateTime(long unixSecondsTimeStamp)
        {
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return dateTime.AddSeconds(unixSecondsTimeStamp);
        }

        /// <summary>
        /// Convert <see cref="DateTime"/> to UNIX seconds timestamp.
        /// </summary>
        /// <param name="dateTime">DateTime value.</param>
        /// <returns>Unix seconds timestamp.</returns>
        public static long DateTimeToUnixTimestamp(DateTime dateTime)
        {
            var offset = (DateTimeOffset)dateTime;
            long unixTime = offset.ToUnixTimeSeconds();
            return unixTime;
        }
    }
}
