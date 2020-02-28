using BusinessWebSite.Engine;
using BusinessWebSite.Engine.Interfaces;
using BusinessWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessWebSite.Controllers
{
    public class HomeController : Controller
    {

        private readonly IEngineProject Funcion;
        private readonly IEngineHttp FuncionHttp;
        private readonly IEngineTool Tool;

        public HomeController(IEngineProject _Funcion, IEngineHttp _FuncionHttp, IEngineTool _Tool)
        {
            this.Funcion = _Funcion;
            this.FuncionHttp = _FuncionHttp;
            this.Tool = _Tool;
        }
        public ActionResult Index(string user, string password)
        {
            ViewBag.Respuesta = null;
            if (Request.HttpMethod == "POST")
            {
                string jsonUserApi = Funcion.BuildUserApiStr(user, password,Tool);
                EngineProcesor proceso = new EngineProcesor();
                proceso.GetTicketAccesoAsync(jsonUserApi, FuncionHttp);
              
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}