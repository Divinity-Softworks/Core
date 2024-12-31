namespace DivinitySoftworks.Core.Net.Mail;

/// <summary>
/// Represents an email attachment, containing a file name, content, and MIME type.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="EmailAttachment"/> class.
/// </remarks>
public sealed class EmailAttachment {

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailAttachment"/> class.
    /// </summary>
    public EmailAttachment() {

    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailAttachment"/> class with specified file name, content, and MIME type.
    /// </summary>
    /// <param name="fileName">The name of the attachment file.</param>
    /// <param name="content">The content of the attachment as a byte array.</param>
    /// <param name="contentType">The MIME type of the attachment.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="fileName"/> or <paramref name="content"/> is null.</exception>

    public EmailAttachment(string fileName, byte[] content, MimeType contentType) {
        FileName = fileName ?? throw new ArgumentNullException(nameof(fileName), "FileName cannot be null.");
        Content = content ?? throw new ArgumentNullException(nameof(content), "Content cannot be null.");
        ContentType = contentType;
    }

    /// <summary>
    /// Gets or sets the name of the attachment file.
    /// </summary>
    public string FileName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the content of the attachment as a byte array.
    /// </summary>
    public byte[] Content { get; set; } = default!;

    /// <summary>
    /// Gets or sets the MIME type of the attachment.
    /// </summary>
    public MimeType ContentType { get; set; } = default!;
}
