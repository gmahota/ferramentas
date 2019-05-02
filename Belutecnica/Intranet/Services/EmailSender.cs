using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Intranet.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        //dependency injection
        private SendGridOptions _sendGridOptions { get; }
        private INetcoreService _netcoreService { get; }
        private SmtpOptions _smtpOptions { get; }
        private readonly IConfiguration _configuration;


        public EmailSender(IConfiguration configuration, IOptions<SendGridOptions> sendGridOptions,
            INetcoreService netcoreService,
            IOptions<SmtpOptions> smtpOptions)
        {
            _configuration = configuration;
            _sendGridOptions = sendGridOptions.Value;
            _netcoreService = netcoreService;
            _smtpOptions = smtpOptions.Value;
        }
        

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {

                await _netcoreService.SendEmailBySendGridAsync(_sendGridOptions.SendGridKey, _sendGridOptions.FromEmail,
                    _sendGridOptions.FromFullName,  subject,  message,  email);


                //using (var client = new SmtpClient())
                //{
                //    var credential = new NetworkCredential
                //    {
                //        UserName = _configuration["Email:Username"],
                //        Password = _configuration["Email:Password"]
                //    };

                //    client.Credentials = credential;
                //    client.Host = _configuration["Email:Host"];
                //    client.Port = int.Parse(_configuration["Email:Port"]);
                //    client.EnableSsl = true;

                //    using (var emailMessage = new MailMessage())
                //    {
                //        emailMessage.To.Add(new MailAddress(email));
                //        emailMessage.From = new MailAddress(_configuration["Email:Username"]);
                //        emailMessage.Subject = subject;

                //        emailMessage.IsBodyHtml = true;

                //        emailMessage.Body = message;

                //        client.Send(emailMessage);
                //    }
                //}
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {

            }

        }

        public async Task SendEmail(string email, string subject, string message)
        {
            using (var client = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = _configuration["Email:Username"],
                    Password = _configuration["Email:Password"]
                };

                client.Credentials = credential;
                client.Host = _configuration["Email:Host"];
                client.Port = int.Parse(_configuration["Email:Port"]);
                client.EnableSsl = true;

                using (var emailMessage = new MailMessage())
                {
                    emailMessage.To.Add(new MailAddress(email));
                    emailMessage.From = new MailAddress(_configuration["Email:Username"]);
                    emailMessage.Subject = subject;
                    emailMessage.Body = message;
                    client.Send(emailMessage);
                }
            }
            await Task.CompletedTask;
        }

        public async Task SendEmail(string email, string subject, string message, bool templateHtml)
        {
            using (var client = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = _configuration["Email:Username"],
                    Password = _configuration["Email:Password"]
                };

                client.Credentials = credential;
                client.Host = _configuration["Email:Host"];
                client.Port = int.Parse(_configuration["Email:Port"]);
                client.EnableSsl = true;

                using (var emailMessage = new MailMessage())
                {
                    emailMessage.To.Add(new MailAddress(email));
                    emailMessage.From = new MailAddress(_configuration["Email:Username"]);
                    emailMessage.Subject = subject;

                    emailMessage.IsBodyHtml = true;

                    if (templateHtml) emailMessage.Body = message;

                    client.Send(emailMessage);
                }
            }
            await Task.CompletedTask;
        }
    }
}
