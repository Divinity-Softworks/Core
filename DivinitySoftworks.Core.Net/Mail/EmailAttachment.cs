namespace DivinitySoftworks.Core.Net.Mail;

/// <summary>
/// Represents an email attachment, containing a file name, content, and MIME type.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="EmailAttachment"/> class.
/// </remarks>
/// <param name="fileName">The name of the attachment file.</param>
/// <param name="content">The content of the attachment as a byte array.</param>
/// <param name="contentType">The MIME type of the attachment.</param>
public sealed class EmailAttachment(string fileName, byte[] content, MimeType contentType) {
    /// <summary>
    /// Gets or sets the name of the attachment file.
    /// </summary>
    public string FileName { get; set; } = fileName;

    /// <summary>
    /// Gets or sets the content of the attachment as a byte array.
    /// </summary>
    public byte[] Content { get; set; } = content;

    /// <summary>
    /// Gets or sets the MIME type of the attachment.
    /// </summary>
    public MimeType ContentType { get; set; } = contentType;
}
