using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Tsp.Infraestructura.Modelo
{
    public interface IMailer
    {
        OperationResult Send(string subject, string body, List<string> tos, string replyTo, List<Attachment> attachments);
        OperationResult Send(string subject, string body, string replyTo);
        OperationResult SendEmail(string email, string subject, string message);
        void Send_(string subject, string body, List<string> tos, string replyTo, List<Attachment> attachments);

    }
}
