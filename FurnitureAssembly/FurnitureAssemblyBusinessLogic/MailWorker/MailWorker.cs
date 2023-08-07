using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FurnitureAssemblyContracts.BindingModels;
using System.Net.Mime;

namespace FurnitureAssemblyBusinessLogic.MailWorker
{
    public class MailWorker
    {
        protected string _mailLogin = "yankalyshev@outlook.com";
        protected string _mailPassword = "250303zyzf";
        protected string _smtpClientHost = "smtp-mail.outlook.com";
        protected int _smtpClientPort = 587;
        private readonly ILogger _logger;
        public MailWorker(ILogger<MailWorker> logger)
        {
            _logger = logger;
        }
        public async void MailSendAsync(MailSendInfoBindingModel info)
        {
            if (string.IsNullOrEmpty(_mailLogin) || string.IsNullOrEmpty(_mailPassword))
            {
                return;
            }
            if (string.IsNullOrEmpty(_smtpClientHost) || _smtpClientPort == 0)
            {
                return;
            }
            if (string.IsNullOrEmpty(info.MailAddress) || string.IsNullOrEmpty(info.Subject) || string.IsNullOrEmpty(info.Text))
            {
                return;
            }
            _logger.LogDebug("Send Mail: {To}, {Subject}", info.MailAddress, info.Subject);
            await SendMailAsync(info);
        }
        protected async Task SendMailAsync(MailSendInfoBindingModel info)
        {
            using var objMailMessage = new MailMessage();
            using var objSmtpClient = new SmtpClient(_smtpClientHost, _smtpClientPort);
            try
            {
                objMailMessage.From = new MailAddress(_mailLogin);
                objMailMessage.To.Add(new MailAddress(info.MailAddress));
                objMailMessage.Subject = info.Subject;
                objMailMessage.Body = info.Text;
                objMailMessage.SubjectEncoding = Encoding.UTF8;
                objMailMessage.BodyEncoding = Encoding.UTF8;
                var path = File.Exists("C:\\temp\\pdf_worker.pdf") ? "C:\\temp\\pdf_worker.pdf" : "C:\\temp\\pdf_storekeeper.pdf";

                Attachment attachment = new Attachment(path, new ContentType(MediaTypeNames.Application.Pdf));
                objMailMessage.Attachments.Add(attachment);

                objSmtpClient.UseDefaultCredentials = false;
                objSmtpClient.EnableSsl = true;
                objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                objSmtpClient.Credentials = new NetworkCredential(_mailLogin, _mailPassword);

                await Task.Run(() => objSmtpClient.Send(objMailMessage));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
