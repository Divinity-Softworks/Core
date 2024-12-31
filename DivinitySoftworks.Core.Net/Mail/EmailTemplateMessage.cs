using System.Net.Mail;
using System.Text.Json.Serialization;

namespace DivinitySoftworks.Core.Net.Mail;

/// <summary>
/// Represents an email message that uses a predefined template.
/// </summary>
public sealed class EmailTemplateMessage {

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailTemplateMessage"/> class.
    /// </summary>
    public EmailTemplateMessage() {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmailTemplateMessage"/> class with a sender and template name.
    /// </summary>
    /// <param name="sender">The sender of the email.</param>
    /// <param name="template">The name of the email template.</param>
    public EmailTemplateMessage(MailAddress sender, string template) {
        Sender = sender;
        Template = template;
    }

    /// <summary>
    /// Gets or sets the sender of the email.
    /// </summary>
    [JsonConverter(typeof(MailAddressJsonConverter))]
    public MailAddress Sender { get; set; } = default!;

    /// <summary>
    /// Gets or sets the list of recipients for the email.
    /// </summary>
    [JsonConverter(typeof(MailAddressListJsonConverter))]
    public List<MailAddress> To { get; set; } = [];

    /// <summary>
    /// Gets or sets the list of CC (carbon copy) recipients for the email.
    /// </summary>
    [JsonConverter(typeof(MailAddressListJsonConverter))]
    public List<MailAddress> CC { get; set; } = [];

    /// <summary>
    /// Gets or sets the list of BCC (blind carbon copy) recipients for the email.
    /// </summary>
    [JsonConverter(typeof(MailAddressListJsonConverter))]
    public List<MailAddress> BCC { get; set; } = [];

    /// <summary>
    /// Gets or sets the subject of the email.
    /// </summary>
    public string? Subject { get; set; }

    /// <summary>
    /// Gets or sets the name of the email template.
    /// </summary>
    public string Template { get; set; } = default!;

    /// <summary>
    /// Gets or sets the parameters to replace placeholders in the template.
    /// </summary>
    public Dictionary<string, string> Parameters { get; set; } = [];

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
    [JsonConverter(typeof(MailAddressJsonConverter))]
    public List<MailAddress>? ReplyTo { get; set; }

    /// <summary>
    /// Gets or sets additional headers for advanced email configuration.
    /// </summary>
    public Dictionary<string, string>? Headers { get; set; }
}
