using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Interfaces.Infraestructura;

namespace Tsp.Sigescom.Utilitarios
{
    public class Mailer : IMailer
    {

        public void Send_(string subject, string body, List<string> tos, string replyTo, List<Attachment> attachments, List<LinkedResource> resources)
        {
            MailMessage msg = new MailMessage();
            try
            {
                SmtpClient smtp = new SmtpClient();
                MailAddress from = new MailAddress(AplicacionSettings.Default.MailAccount);
                smtp.Credentials = new NetworkCredential(AplicacionSettings.Default.MailAccount, AplicacionSettings.Default.MailPassword);
                smtp.Host = AplicacionSettings.Default.MailServer;
                smtp.Port = AplicacionSettings.Default.MailPort;
                if (tos != null)
                {
                    tos.ForEach(t => msg.To.Add(t));
                }
                else
                {
                    throw new Exception("No se han especificado destinatarios");
                }
                msg.From = from;
                msg.Subject = subject;
                msg.Body = body;
                msg.IsBodyHtml = true;
                if (attachments != null)
                {
                    attachments.ForEach(a => msg.Attachments.Add(a));
                }
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, Encoding.UTF8, MediaTypeNames.Text.Html);
                // Create a plain text message for client that don't support HTML
                AlternateView plainView = AlternateView.CreateAlternateViewFromString(Regex.Replace(body, "<[^>]+?>", string.Empty), Encoding.UTF8, MediaTypeNames.Text.Plain);
                if (resources != null)
                {
                    resources.ForEach(r => htmlView.LinkedResources.Add(r));
                }
                msg.AlternateViews.Add(plainView);
                msg.AlternateViews.Add(htmlView);
                msg.ReplyToList.Add(new MailAddress(replyTo));
                smtp.Send(msg);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                msg.Dispose();
            }
        }

        public OperationResult Send(string subject, string body, List<string> tos, string replyTo, List<Attachment> attachments)
        {
            try
            {
                Send_(subject, body, tos, replyTo, attachments, null);
                return new OperationResult(OperationResultEnum.Success);
            }
            catch (Exception e)
            {
                return new OperationResult(e, "Error al intentar enviar correo electrónico");
            }

        }
        public OperationResult Send(string subject, string body, List<string> tos, string replyTo, List<Attachment> attachments, List<LinkedResource> resources)
        {
            try
            {
                Send_(subject, body, tos, replyTo, attachments, resources);
                return new OperationResult(OperationResultEnum.Success);
            }
            catch (Exception e)
            {
                return new OperationResult(e, "Error al intentar enviar correo electrónico");
            }

        }
        public OperationResult SendEmail(string email, string subject, string message)
        {
            OperationResult result;
            try
            {
                //Here must be use the email where from you want to send email
                var _email = AplicacionSettings.Default.MailAccount;
                var _epass = AplicacionSettings.Default.MailPassword;
                var _dispName = AplicacionSettings.Default.MailAccount;
                MailMessage myMessage = new MailMessage();
                myMessage.To.Add(email);
                myMessage.From = new MailAddress(_email, _dispName);
                myMessage.Subject = subject;
                myMessage.Body = message;
                myMessage.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient())
                {
                    //smtp.EnableSsl = true;
                    smtp.Host = AplicacionSettings.Default.MailServer;
                    smtp.Port = 25;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(_email, _epass);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.SendCompleted += (s, e) => { smtp.Dispose(); };
                    smtp.SendMailAsync(myMessage);
                }
                result = new OperationResult();
            }
            catch (Exception e)
            {
                result = new OperationResult(e);
            }
            return result;
        }


        /// <summary>
        /// send a mail to mailAccount established in config file
        /// </summary>
        /// <param name="body"></param>
        /// <param name="replyTo"></param>
        /// <returns>
        /// 1: success
        /// 0: fail
        /// </returns>
        public OperationResult Send(string subject, string body, string replyTo)
        {
            List<string> tos = new List<string>();
            tos.Add(AplicacionSettings.Default.MailAccount);
            return Send(subject, body, tos, replyTo, null);
        }


        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                //Here must be use the email where from you want to send email
                var _email = AplicacionSettings.Default.MailAccount;
                var _epass = AplicacionSettings.Default.MailPassword;
                var _dispName = AplicacionSettings.Default.MailAccount;
                MailMessage myMessage = new MailMessage();
                myMessage.To.Add(email);
                myMessage.From = new MailAddress(_email, _dispName);
                myMessage.Subject = subject;
                myMessage.Body = message;
                myMessage.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient())
                {
                    //smtp.EnableSsl = true;
                    smtp.Host = AplicacionSettings.Default.MailServer;
                    smtp.Port = 25;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(_email, _epass);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.SendCompleted += (s, e) => { smtp.Dispose(); };
                    await smtp.SendMailAsync(myMessage);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al enviar correo ", e);
            }
        }
    }
}