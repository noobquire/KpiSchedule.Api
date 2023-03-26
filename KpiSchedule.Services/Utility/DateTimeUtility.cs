using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KpiSchedule.Services.Utility
{
    public static class DateTimeUtility
    {
        public static DateTime UnixTimestampToDateTime(long unixSecondsTimeStamp)
        {
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return dateTime.AddSeconds(unixSecondsTimeStamp);
        }

        public static long DateTimeToUnixTimestamp(DateTime dateTime)
        {
            var offset = (DateTimeOffset)dateTime;
            long unixTime = offset.ToUnixTimeSeconds();
            return unixTime;
        }
    }
}
