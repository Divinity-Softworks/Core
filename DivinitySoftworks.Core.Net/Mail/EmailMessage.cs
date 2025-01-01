using System.Net.Mail;

namespace DivinitySoftworks.Core.Net.Mail;

/// <summary>
/// Represents an email message with customizable properties such as recipients, body, and attachments.
/// </summary>
public sealed class EmailMessage {

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailMessage"/> class.
    /// </summary>
    public EmailMessage() {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailMessage"/> class with a sender and subject.
    /// </summary>
    /// <param name="sender">The sender of the email.</param>
    /// <param name="subject">The subject of the email.</param>
    public EmailMessage(string sender, string subject) {
        Sender = sender;
        Subject = subject;
    }

    /// <summary>
    /// Gets or sets the sender of the email.
    /// </summary>
    public string Sender { get; set; } = default!;

    /// <summary>
    /// Gets or sets the list of recipients for the email.
    /// </summary>
    public List<string> To { get; set; } = [];

    /// <summary>
    /// Gets or sets the list of CC (carbon copy) recipients for the email.
    /// </summary>
    public List<string> CC { get; set; } = [];

    /// <summary>
    /// Gets or sets the list of BCC (blind carbon copy) recipients for the email.
    /// </summary>
    public List<string> BCC { get; set; } = [];

    /// <summary>
    /// Gets or sets the subject of the email.
    /// </summary>
    public string? Subject { get; set; }

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
    public List<string> ReplyTo { get; set; } = [];

    /// <summary>
    /// Gets or sets additional headers for advanced email configuration.
    /// </summary>
    public Dictionary<string, string>? Headers { get; set; }
}
