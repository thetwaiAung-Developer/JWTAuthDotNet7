using TimeZoneConverter;

namespace JWTAuthDotNet7.Helper
{
    public static class DateTimeHelper
    {
        public static DateTime ToMyanmarDateTime()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TZConvert.GetTimeZoneInfo("Asia/Yangon"));
        }
    }
}
