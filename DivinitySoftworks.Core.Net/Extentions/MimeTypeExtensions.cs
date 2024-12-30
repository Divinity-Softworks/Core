namespace DivinitySoftworks.Core.Net;

/// <summary>
/// Provides extension methods for working with the <see cref="MimeType"/> enumeration.
/// </summary>
internal static class MimeTypeExtensions {

    /// <summary>
    /// Converts a <see cref="MimeType"/> value to its corresponding MIME type string representation.
    /// </summary>
    /// <param name="mimeType">The <see cref="MimeType"/> value to convert.</param>
    /// <returns>The string representation of the MIME type.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the provided <paramref name="mimeType"/> is not a valid <see cref="MimeType"/> value.</exception>
    public static string ToMimeString(this MimeType mimeType) {
        return mimeType switch {
            MimeType.TextPlain => "text/plain",
            MimeType.TextHtml => "text/html",
            MimeType.ApplicationPdf => "application/pdf",
            MimeType.ApplicationJson => "application/json",
            MimeType.ApplicationXml => "application/xml",
            MimeType.ApplicationZip => "application/zip",
            MimeType.ApplicationOctetStream => "application/octet-stream",
            MimeType.ApplicationMsWord => "application/msword",
            MimeType.ApplicationVndOpenXmlWordprocessingmlDocument => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            MimeType.ApplicationVndMsExcel => "application/vnd.ms-excel",
            MimeType.ApplicationVndOpenXmlSpreadsheetmlSheet => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            MimeType.ApplicationVndMsPowerpoint => "application/vnd.ms-powerpoint",
            MimeType.ApplicationVndOpenXmlPresentationmlPresentation => "application/vnd.openxmlformats-officedocument.presentationml.presentation",
            MimeType.ImageJpeg => "image/jpeg",
            MimeType.ImagePng => "image/png",
            MimeType.ImageGif => "image/gif",
            MimeType.ImageBmp => "image/bmp",
            MimeType.ImageTiff => "image/tiff",
            MimeType.AudioMpeg => "audio/mpeg",
            MimeType.VideoMp4 => "video/mp4",
            MimeType.VideoXMsWmv => "video/x-ms-wmv",
            _ => throw new ArgumentOutOfRangeException(nameof(mimeType), mimeType, null)
        };
    }
}
