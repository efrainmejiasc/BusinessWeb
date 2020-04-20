using BusinessWebSite.Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace BusinessWebSite.Engine
{
    public class EngineNotify:IEngineNotify
    {

        public bool EnviarEmail(string emailTo,string asunto, string body, string pathAdjunto)
        {
            bool result = false;

            MailMessage mensaje = new MailMessage();
            SmtpClient servidor = new SmtpClient();
            mensaje.From = new MailAddress("www.tuidentidad.com.co <tuidentidad.com.co@gmail.com>");
            mensaje.Subject = asunto;
            mensaje.SubjectEncoding = System.Text.Encoding.UTF8;
            mensaje.Body = body;
            mensaje.BodyEncoding = System.Text.Encoding.UTF8;
            mensaje.IsBodyHtml = true;
            mensaje.To.Add(new MailAddress(emailTo));
            if (!string.IsNullOrEmpty(pathAdjunto)) 
                mensaje.Attachments.Add(new Attachment(pathAdjunto)); 

            servidor.Credentials = new System.Net.NetworkCredential("tuidentidad.com.co@gmail.com", "1234santiago");
            servidor.Port = 587;
            servidor.Host = "smtp.gmail.com";
            servidor.EnableSsl = true;
            servidor.Send(mensaje);
            mensaje.Dispose();
            result = true;

            return result;

        }

        public bool EnviarEmail(List<string> emailTo, string asunto, string body, string pathAdjunto)
        {
            bool result = false;

            MailMessage mensaje = new MailMessage();
            SmtpClient servidor = new SmtpClient();
            mensaje.From = new MailAddress("www.tuidentidad.com.co <tuidentidad.com.co@gmail.com>");
            mensaje.Subject = asunto;
            mensaje.SubjectEncoding = System.Text.Encoding.UTF8;
            mensaje.Body = body;
            mensaje.BodyEncoding = System.Text.Encoding.UTF8;
            mensaje.IsBodyHtml = true;
            foreach(string email in emailTo)
            mensaje.To.Add(new MailAddress(email));

            if (!string.IsNullOrEmpty(pathAdjunto))
                mensaje.Attachments.Add(new Attachment(pathAdjunto));

            servidor.Credentials = new System.Net.NetworkCredential("tuidentidad.com.co@gmail.com", "1234santiago");
            servidor.Port = 587;
            servidor.Host = "smtp.gmail.com";
            servidor.EnableSsl = true;
            servidor.Send(mensaje);
            mensaje.Dispose();
            result = true;

            return result;

        }

    }
}