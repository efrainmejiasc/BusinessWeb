﻿using BusinessWebApi.Engine.Interfaces;
using BusinessWebApi.Models.Objetos;
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
            mensaje.From = new MailAddress("www.tuidentidad.com.co<tuidentidad.com.co@gmail.com>");
            mensaje.Subject = "Prueba envio email";
            mensaje.SubjectEncoding = System.Text.Encoding.UTF8;
            mensaje.Body = model.Cuerpo;
            mensaje.BodyEncoding = System.Text.Encoding.UTF8;
            mensaje.IsBodyHtml = true;
            mensaje.To.Add(new MailAddress(model.EmailTo));
            //if (pathAdjunto != string.Empty) { mensaje.Attachments.Add(new Attachment(pathAdjunto)); }
            servidor.Credentials = new System.Net.NetworkCredential("tuidentidad.com.co@gmail.com", "1234santiago");
            servidor.Port = 587;
            servidor.Host = "smtp.gmail.com";
            servidor.EnableSsl = true;
            servidor.Send(mensaje);
            mensaje.Dispose();
            result = true;

            return result;
        }

        public bool EnviarEmail(string emailTo, string codigo,string empresa)
        {
            bool result = false;

            MailMessage mensaje = new MailMessage();
            SmtpClient servidor = new SmtpClient();
            mensaje.From = new MailAddress("www.tuidentidad.com.co <tuidentidad.com.co@gmail.com>");
            mensaje.Subject = "Codigo para registro de equipo telefonicos (Digital Comerce)";
            mensaje.SubjectEncoding = System.Text.Encoding.UTF8;
            mensaje.Body = Mensaje(empresa,codigo);
            mensaje.BodyEncoding = System.Text.Encoding.UTF8;
            mensaje.IsBodyHtml = true;
            mensaje.To.Add(new MailAddress(emailTo));
            //if (pathAdjunto != string.Empty) { mensaje.Attachments.Add(new Attachment(pathAdjunto)); }
            servidor.Credentials = new System.Net.NetworkCredential("tuidentidad.com.co@gmail.com", "1234santiago");
            servidor.Port = 587;
            servidor.Host = "smtp.gmail.com";
            servidor.EnableSsl = true;
            servidor.Send(mensaje);
            mensaje.Dispose();
            result = true;

            return result;

        }

        private string Mensaje(string empresa, string codigo)
        {
            DateTime Date = DateTime.Now;

            return "Saludos: " + "Autoridades de " + empresa + "</br></br>   Su codigo para registro de equipos es el siguiente: <h4>" + codigo + "</h4></br>   " +
                    "comparta este codigo para registrar dispositivos desde nuestra aplicacion movil o sitio web.";
        }

        public void EnviarEmailNoAsistentes (List<DataEmailNoAsistencia> emailNoAsistentes)
        {
          string body = string.Empty;
          foreach(DataEmailNoAsistencia i in emailNoAsistentes)
          {
                body = "Saludos... </br> + Hoy: " + i.Fecha + "<br> (Prueba de Plataforma)    Por medio del presente notificamos la ausencia de: <strong>" +
                        i.Nombre + " " + i.Apellido + " </strong> documeto de identidad: <strong>" + i.Dni +
                       "</strong>  </br></br> ATT: Solo Educativas";
                EnviarEmail(i.Email, body);
                body = string.Empty;        
          }
        }

        public bool EnviarEmail(string emailTo, string body)
        {
            bool result = false;

            MailMessage mensaje = new MailMessage();
            SmtpClient servidor = new SmtpClient();
            mensaje.From = new MailAddress("www.tuidentidad.com.co <tuidentidad.com.co@gmail.com>");
            mensaje.Subject = "Notificacion de inasistencia";
            mensaje.SubjectEncoding = System.Text.Encoding.UTF8;
            mensaje.Body = body;
            mensaje.BodyEncoding = System.Text.Encoding.UTF8;
            mensaje.IsBodyHtml = true;
            mensaje.To.Add(new MailAddress(emailTo));
            //if (pathAdjunto != string.Empty) { mensaje.Attachments.Add(new Attachment(pathAdjunto)); }
            servidor.Credentials = new System.Net.NetworkCredential("tuidentidad.com.co@gmail.com", "1234santiago");
            servidor.Port = 587;
            servidor.Host = "smtp.gmail.com";
            servidor.EnableSsl = true;
            servidor.Send(mensaje);
            mensaje.Dispose();
            result = true;

            return result;

        }


        public bool EnviarEmail(string emailTo, string nombreArchivo, string pathAdjunto, bool n = false)
        {
            bool result = false;

            MailMessage mensaje = new MailMessage();
            SmtpClient servidor = new SmtpClient();
            mensaje.From = new MailAddress("www.tuidentidad.com.co <tuidentidad.com.co@gmail.com>");
            mensaje.Subject = "Asistencia: " + nombreArchivo;
            mensaje.SubjectEncoding = System.Text.Encoding.UTF8;
            mensaje.Body = "Asistencia: " + nombreArchivo;
            mensaje.BodyEncoding = System.Text.Encoding.UTF8;
            mensaje.IsBodyHtml = true;
            mensaje.To.Add(new MailAddress(emailTo));
            if (pathAdjunto != string.Empty) { mensaje.Attachments.Add(new Attachment(pathAdjunto)); }
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
