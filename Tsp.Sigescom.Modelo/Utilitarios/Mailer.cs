using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Humanizer;
using Tsp.Sigescom.Config;
using Tsp.Sigescom.Modelo.Entidades;
using Tsp.Sigescom.Modelo.Entidades.Exceptions;
//using Tsp.Sigescom.Utilitarios;

namespace Tsp.Sigescom.Modelo
{
    //public class Mailer__
    //{


    //    public static OperationResult Send(string subject, string body, List<string> tos, string replyTo, List<Attachment> attachments)
    //    {
    //        try
    //        {
    //           Mailer_.Send_(subject, body, tos, replyTo, attachments);
    //            return new OperationResult(OperationResultEnum.Success);
    //        }
    //        catch (Exception e)
    //        {
    //            return new OperationResult(e, "Error al intentar enviar correo electrónico");
    //        }

    //    }

    //    public static OperationResult SendEmail(string email, string subject, string message)
    //    {
    //        OperationResult result;
    //        try
    //        {
    //            //Here must be use the email where from you want to send email
    //            var _email = AplicacionSettings.Default.MailAccount;
    //            var _epass = AplicacionSettings.Default.MailPassword;
    //            var _dispName = AplicacionSettings.Default.MailAccount;
    //            MailMessage myMessage = new MailMessage();
    //            myMessage.To.Add(email);
    //            myMessage.From = new MailAddress(_email, _dispName);
    //            myMessage.Subject = subject;
    //            myMessage.Body = message;
    //            myMessage.IsBodyHtml = true;

    //            using (SmtpClient smtp = new SmtpClient())
    //            {
    //                //smtp.EnableSsl = true;
    //                smtp.Host = AplicacionSettings.Default.MailServer;
    //                smtp.Port = 25;
    //                smtp.UseDefaultCredentials = false;
    //                smtp.Credentials = new NetworkCredential(_email, _epass);
    //                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
    //                smtp.SendCompleted += (s, e) => { smtp.Dispose(); };
    //                smtp.SendMailAsync(myMessage);
    //            }
    //            result = new OperationResult();
    //        }
    //        catch (Exception e)
    //        {
    //            result = new OperationResult(e);
    //        }
    //        return result;
    //    }


    //    /// <summary>
    //    /// send a mail to mailAccount established in config file
    //    /// </summary>
    //    /// <param name="body"></param>
    //    /// <param name="replyTo"></param>
    //    /// <returns>
    //    /// 1: success
    //    /// 0: fail
    //    /// </returns>
    //    public static OperationResult Send(string subject, string body, string replyTo)
    //    {
    //        List<string> tos = new List<string>();
    //        tos.Add(AplicacionSettings.Default.MailAccount);
    //        return Send(subject, body, tos, replyTo, null);
    //    }
   // }
}
