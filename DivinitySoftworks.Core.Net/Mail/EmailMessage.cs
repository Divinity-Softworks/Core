using System.Net.Mail;

namespace DivinitySoftworks.Core.Net.Mail;

/// <summary>
/// Represents an email message with sender, recipients, subject, body, attachments, and additional properties.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="EmailMessage"/> class with the specified sender, subject, and text body.
/// </remarks>
/// <param name="sender">The sender of the email.</param>
/// <param name="subject">The subject of the email.</param>
public sealed class EmailMessage(MailAddress sender, string subject) {
    /// <summary>
    /// Gets or sets the sender of the email.
    /// </summary>
    public MailAddress Sender { get; set; } = sender;

    /// <summary>
    /// Gets or sets the list of recipients for the email.
    /// </summary>
    public List<MailAddress> To { get; set; } = [];

    /// <summary>
    /// Gets or sets the list of CC (carbon copy) recipients for the email.
    /// </summary>
    public List<MailAddress> CC { get; set; } = [];

    /// <summary>
    /// Gets or sets the list of BCC (blind carbon copy) recipients for the email.
    /// </summary>
    public List<MailAddress> BCC { get; set; } = [];

    /// <summary>
    /// Gets or sets the subject of the email.
    /// </summary>
    public string Subject { get; set; } = subject;

    /// <summary>
    /// Gets or sets the HTML body of the email.
    /// </summary>
    public string HtmlBody { get; set; } = default!;

    /// <summary>
    /// Gets or sets the plain text body of the email.
    /// </summary>
    public string TextBody { get; set; } = default!;

    /// <summary>
    /// Gets or sets the priority of the email.
    /// </summary>
    public MailPriority Priority { get; set; } = MailPriority.Normal;

    /// <summary>
    /// Gets or sets the list of attachments for the email.
    /// </summary>
    public List<EmailAttachment> Attachments { get; set; } = [];

    /// <summary>
    /// Gets or sets the date and time when the email was sent.
    /// </summary>
    public DateTime SentDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the list of reply-to addresses for the email.
    /// </summary>
    public List<MailAddress>? ReplyTo { get; set; }

    /// <summary>
    /// Gets or sets additional headers for advanced email configuration.
    /// </summary>
    public Dictionary<string, string>? Headers { get; set; }
}
