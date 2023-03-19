using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using Tsp.Sigescom.Modelo.Entidades;

namespace Tsp.Sigescom.Modelo.Interfaces.Infraestructura
{
    public interface IMailer
    {
        OperationResult Send(string subject, string body, List<string> tos, string replyTo, List<Attachment> attachments);
        OperationResult Send(string subject, string body, List<string> tos, string replyTo, List<Attachment> attachments, List<LinkedResource> resources);
        OperationResult Send(string subject, string body, string replyTo);
        OperationResult SendEmail(string email, string subject, string message);
        Task SendEmailAsync(string email, string subject, string message);

    }
}
