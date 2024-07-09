using Business.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Business.Concrete
{
	public class EmailManager : IEmailService
	{
		private readonly SmtpClient _smtpClient;

        public EmailManager()
        {
			_smtpClient = new SmtpClient("smtp.gmail.com")
			{
				Port = 587,
				Credentials = new NetworkCredential("onursemseys@gmail.com", "syed obtb izzf jnrk"),
				EnableSsl = true,
			};
		}
        public async Task SendEmailAsync(string toEmail, string subject, string message)
		{
			var mailMessage = new MailMessage("onursemseys@gmail.com", toEmail, subject, message)
			{
				IsBodyHtml = true,
			};

			await _smtpClient.SendMailAsync(mailMessage);
		}

	}
}

