using Microsoft.Extensions.Logging;
using StockaProSSO.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockaProSSO.Logic.Services
{

    public class MessageService : IEmailService, ISmsService
    {
        private readonly ILogger<MessageService> _logger;

        public MessageService(ILogger<MessageService> logger)
        {
            _logger = logger;
        }
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            _logger.LogInformation("Email: {email}, Subject: {subject}, Message: {message}", email, subject, message);
            return Task.FromResult(0);
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            _logger.LogInformation("SMS: {number}, Message: {message}", number, message);
            return Task.FromResult(0);
        }
    }
}
