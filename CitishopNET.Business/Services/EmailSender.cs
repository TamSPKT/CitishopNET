using CitishopNET.Business.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CitishopNET.Business.Services
{
	public class EmailSender : IEmailSender
	{
		private readonly AuthMessageSenderOptions _options;
		private readonly ILogger _logger;

		public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor,
						   ILogger<EmailSender> logger)
		{
			_options = optionsAccessor.Value;
			_logger = logger;
		}

		public async Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			if (string.IsNullOrEmpty(_options.SendGridKey))
			{
				throw new Exception("Null SendGridKey");
			}
			await Execute(_options.SendGridKey, subject, htmlMessage, email);
		}

		public async Task Execute(string apiKey, string subject, string message, string toEmail)
		{
			var client = new SendGridClient(apiKey);
			var msg = new SendGridMessage()
			{
				From = new EmailAddress("18110359@student.hcmute.edu.vn", "Citishop Email"),
				Subject = subject,
				PlainTextContent = message,
				HtmlContent = message
			};
			msg.AddTo(new EmailAddress(toEmail));

			// Disable click tracking.
			// See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
			msg.SetClickTracking(false, false);
			var response = await client.SendEmailAsync(msg);
			_logger.LogInformation(response.IsSuccessStatusCode
				? $"Email to {toEmail} queued successfully!"
				: $"Failure Email to {toEmail}: {response.StatusCode}");
		}
	}
}
