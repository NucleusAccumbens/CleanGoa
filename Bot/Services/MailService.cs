using Bot.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Bot.Services;

public class MailService
{
    public static async Task SendEmailAsync(Mail mail)
    {
        var message = new MimeMessage();

        message.From.Add(mail.From);

        foreach (var to in mail.To)
        {
            message.To.Add(to);
        }
        message.Subject = mail.Subject;

        var bodyBuilder = new BodyBuilder();
        bodyBuilder.TextBody = mail.Body;
        //bodyBuilder.Attachments.Add(mail.PhotoPath);
        message.Body = bodyBuilder.ToMessageBody();


        var client = new SmtpClient();

        client.Connect(MailSettings.Host, MailSettings.Port, SecureSocketOptions.Auto);
        client.Authenticate(MailSettings.Username, MailSettings.Password);

        client.Send(message);
        client.Disconnect(true);

        Console.WriteLine("Email sent");
    }
}
