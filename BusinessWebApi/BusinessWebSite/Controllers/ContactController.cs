
using BusinessWebSite.Engine.Interfaces;
using BusinessWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessWebSite.Controllers
{
    public class ContactController : Controller
    {
        private readonly IEngineProject Funcion;
        private readonly IEngineHttp FuncionHttp;
        private readonly IEngineTool Tool;
        private readonly IEngineProcesor Proceso;
        private readonly IEngineNotify Notify;

        public ContactController(IEngineProject _Funcion, IEngineHttp _FuncionHttp, IEngineTool _Tool, IEngineProcesor _Proceso, IEngineNotify _Notify)
        {
            this.Funcion = _Funcion;
            this.FuncionHttp = _FuncionHttp;
            this.Tool = _Tool;
            this.Proceso = _Proceso;
            this.Notify = _Notify;
        }
        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Support()
        {
            return View();
        }

        public ActionResult Services()
        {
            return View();
        }


        [HttpPost] 
        public ActionResult MensajeContacto(string email, string asunto, string mensaje)
        {
            Respuesta respuesta = new Respuesta();
            respuesta.Resultado = Tool.EmailEsValido(email);
            if (!respuesta.Resultado)
            {
                respuesta.Descripcion = "Email No Valido";
                return Json(respuesta);
            }

            List<string> emailTo = new List<string>();
            emailTo.Insert(0, "tuidentidad.com.co@gmail.com");
            emailTo.Insert(1,email);
            
            foreach(string mail in emailTo)
            {
                respuesta.Resultado = Notify.EnviarEmail(mail, asunto, mensaje, null);
                asunto = "Contacto www.tuidentidad.com.co";
                mensaje = "Gracias por contactarnos... <br> Hemos recibido tu consulta y te responderemos en la brevedad de lo posible.<br><br> ATT: www.tuidentidad.com.co";
            }
         
            if (respuesta.Resultado)
                respuesta.Descripcion = "Mensaje Enviado";
            else
                respuesta.Descripcion = "Error Enviando";

            return Json(respuesta);
        }


        [HttpPost]
        public ActionResult MensajeSoporte(string email, string tema , string mensaje)
        {
            Respuesta respuesta = new Respuesta();
            respuesta.Resultado = Tool.EmailEsValido(email);
            if (!respuesta.Resultado)
            {
                respuesta.Descripcion = "Email No Valido";
                return Json(respuesta);
            }

            List<string> emailTo = new List<string>();
            emailTo.Insert(0, "tuidentidad.com.co@gmail.com");
            emailTo.Insert(1, email);

            foreach (string mail in emailTo)
            {
                respuesta.Resultado = Notify.EnviarEmail(mail, tema , mensaje, null);
                tema = "Contacto www.tuidentidad.com.co";
                mensaje = "Gracias por contactarnos... <br> Hemos recibido tu consulta y te responderemos en la brevedad de lo posible.<br><br> ATT: www.tuidentidad.com.co";
            }


            if (respuesta.Resultado)
                respuesta.Descripcion = "Mensaje Enviado";
            else
                respuesta.Descripcion = "Error Enviando";

            return Json(respuesta);
        }
    }
}