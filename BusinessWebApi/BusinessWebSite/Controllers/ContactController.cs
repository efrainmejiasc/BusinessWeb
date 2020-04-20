
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
            emailTo.Add(email);
            emailTo.Add("tuidentidad.com.co@gmail.com");

         
            respuesta.Resultado = Notify.EnviarEmail(emailTo, asunto, mensaje,null);
            if (respuesta.Resultado)
                respuesta.Descripcion = "Mensaje Enviado";
            else
                respuesta.Descripcion = "Error Enviando";

            return Json(respuesta);
        }
    }
}