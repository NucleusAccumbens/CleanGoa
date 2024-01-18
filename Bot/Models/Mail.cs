using MimeKit;

namespace Bot.Models;

public class Mail
{
    private readonly string _body;

    public Mail(string body)
    {
        _body = body;
    }

    public MailboxAddress From => new("Elizaveta", "bezlissa@gmail.com");

    public List<MailboxAddress> To => new()
    {
        new(String.Empty, "bezlissa@gmail.com"),
        new(String.Empty, "rg.ngt@nic.in"),
        new(String.Empty, "mail.gspcb@gov.in"),
        new(String.Empty, "chairman-gspcb.goa@nic.in"),
        new(String.Empty, "ms-gspcb.goa@nic.in"),
        new(String.Empty, "dir-env.goa@nic.in"),
        new(String.Empty, "sanjucat@yahoo.com"),
    };

    public string Subject => "Illegal garbage incineration";

    public string Body => _body;

    public string? PhotoPath { get; set; }
}
