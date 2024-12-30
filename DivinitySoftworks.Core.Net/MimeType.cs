namespace DivinitySoftworks.Core.Net;

/// <summary>
/// Represents a collection of MIME types used to specify the format of files and data.
/// </summary>
public enum MimeType {
    /// <summary>Plain text format (text/plain).</summary>
    TextPlain,
    /// <summary>HTML format (text/html).</summary>
    TextHtml,
    /// <summary>PDF document format (application/pdf).</summary>
    ApplicationPdf,
    /// <summary>JSON format (application/json).</summary>
    ApplicationJson,
    /// <summary>XML format (application/xml).</summary>
    ApplicationXml,
    /// <summary>ZIP archive format (application/zip).</summary>
    ApplicationZip,
    /// <summary>Binary stream format (application/octet-stream).</summary>
    ApplicationOctetStream,
    /// <summary>Microsoft Word document (application/msword).</summary>
    ApplicationMsWord,
    /// <summary>Microsoft Word Open XML document (application/vnd.openxmlformats-officedocument.wordprocessingml.document).</summary>
    ApplicationVndOpenXmlWordprocessingmlDocument,
    /// <summary>Microsoft Excel spreadsheet (application/vnd.ms-excel).</summary>
    ApplicationVndMsExcel,
    /// <summary>Microsoft Excel Open XML spreadsheet (application/vnd.openxmlformats-officedocument.spreadsheetml.sheet).</summary>
    ApplicationVndOpenXmlSpreadsheetmlSheet, 
    /// <summary>Microsoft PowerPoint presentation (application/vnd.ms-powerpoint).</summary>
    ApplicationVndMsPowerpoint,
    /// <summary>Microsoft PowerPoint Open XML presentation (application/vnd.openxmlformats-officedocument.presentationml.presentation).</summary>
    ApplicationVndOpenXmlPresentationmlPresentation,
    /// <summary>JPEG image format (image/jpeg).</summary>
    ImageJpeg,
    /// <summary>PNG image format (image/png).</summary>
    ImagePng,
    /// <summary>GIF image format (image/gif).</summary>
    ImageGif,
    /// <summary>BMP image format (image/bmp).</summary>
    ImageBmp,
    /// <summary>TIFF image format (image/tiff).</summary>
    ImageTiff,
    /// <summary>MPEG audio format (audio/mpeg).</summary>
    AudioMpeg,
    /// <summary>MP4 video format (video/mp4).</summary>
    VideoMp4,
    /// <summary>Windows Media Video format (video/x-ms-wmv).</summary>
    VideoXMsWmv
}
