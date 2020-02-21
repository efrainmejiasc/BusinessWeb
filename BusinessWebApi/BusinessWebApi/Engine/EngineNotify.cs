using BusinessWebApi.Engine.Interfaces;
using BusinessWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BusinessWebApi.Engine
{
    public class EngineNotify:IEngineNotify
    {
        public bool EnviarEmail(DataEmail model)
        {
            bool result = false;

            MailMessage mensaje = new MailMessage();
            SmtpClient servidor = new SmtpClient();
            mensaje.From = new MailAddress("Test App <sudokuparatodos@gmail.com>");
            mensaje.Subject = "Prueba envio email";
            mensaje.SubjectEncoding = System.Text.Encoding.UTF8;
            mensaje.Body = model.Cuerpo;
            mensaje.BodyEncoding = System.Text.Encoding.UTF8;
            mensaje.IsBodyHtml = true;
            mensaje.To.Add(new MailAddress(model.EmailTo));
            //if (pathAdjunto != string.Empty) { mensaje.Attachments.Add(new Attachment(pathAdjunto)); }
            servidor.Credentials = new System.Net.NetworkCredential("sudokuparatodos@gmail.com", "1234santiago");
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
