using System;

public static class DateTimeExtensions {
    /// <summary>
    /// Converts a DateTime to Unix time seconds.
    /// </summary>
    /// <param name="dateTime">The DateTime to convert. It should be in UTC.</param>
    /// <returns>The Unix time seconds.</returns>
    public static long ToUnixTimeSeconds(this DateTime dateTime) {
        DateTimeOffset dateTimeOffset = new (dateTime.ToUniversalTime());
        return dateTimeOffset.ToUnixTimeSeconds();
    }

    /// <summary>
    /// Converts Unix time seconds to DateTime in UTC.
    /// </summary>
    /// <param name="unixTimeSeconds">The Unix time seconds to convert.</param>
    /// <returns>The DateTime in UTC.</returns>
    public static DateTime FromUnixTimeSeconds(this long unixTimeSeconds) {
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixTimeSeconds);
        return dateTimeOffset.UtcDateTime;
    }
}