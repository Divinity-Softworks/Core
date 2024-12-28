using System.Web;

namespace System;

/// <summary>
/// Extension methods for <see cref="string"/> to handle URL encoding.
/// </summary>
public static class StringExtensions {
    /// <summary>
    /// Checks if the string is URL encoded (%xx format) and decodes it if encoded.
    /// </summary>
    /// <param name="value">The string to check and decode if URL encoded.</param>
    /// <returns>
    /// The decoded string if URL encoded; otherwise, returns the original string.
    /// </returns>
    public static string DecodeIfUrlEncoded(this string value) {
        if (value.Contains("%")) {
            try {
                return HttpUtility.UrlDecode(value);
            }
            catch (Exception) {
                // If decoding fails, return the original string
                return value;
            }
        }
        else {
            return value;
        }
    }
}