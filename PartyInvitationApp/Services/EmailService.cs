using System.Net;
using System.Net.Mail;

public class EmailService
{
    public static void SendInvitation(string toEmail, string guestName, string partyName, string partyLocation, DateTime partyDate)
    {
        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("your-email@gmail.com", "your-app-password"),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress("your-email@gmail.com"),
            Subject = $"You're Invited to {partyName}!",
            Body = $"<h1>Hello {guestName},</h1><p>You're invited to {partyName} at {partyLocation} on {partyDate}.</p>",
            IsBodyHtml = true,
        };

        mailMessage.To.Add(toEmail);
        smtpClient.Send(mailMessage);
    }
}
