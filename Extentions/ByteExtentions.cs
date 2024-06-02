namespace System;

/// <summary>
/// Provides extension methods for the <see cref="byte[]"/> type.
/// </summary>
internal static class ByteExtentions {

    /// <summary>
    /// Converts a byte array to a Base64 URL encoded string.
    /// </summary>
    /// <param name="input">The byte array to encode.</param>
    /// <returns>The Base64 URL encoded string.</returns>
    public static string ToBase64UrlEncoded(this byte[] input) {
        var output = Convert.ToBase64String(input);
        output = output.Split('=')[0]; // Remove any trailing '='s
        output = output.Replace('+', '-'); // 62nd char of encoding
        output = output.Replace('/', '_'); // 63rd char of encoding
        return output;
    }
}
