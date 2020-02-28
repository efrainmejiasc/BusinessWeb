using BusinessWebSite.Engine;
using BusinessWebSite.Engine.Interfaces;
using BusinessWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BusinessWebSite.Controllers
{
    public class HomeController : Controller
    {

        private readonly IEngineProject Funcion;
        private readonly IEngineHttp FuncionHttp;
        private readonly IEngineTool Tool;
        private readonly IEngineProcesor Proceso;

        public HomeController(IEngineProject _Funcion, IEngineHttp _FuncionHttp, IEngineTool _Tool,IEngineProcesor _Proceso)
        {
            this.Funcion = _Funcion;
            this.FuncionHttp = _FuncionHttp;
            this.Tool = _Tool;
            this.Proceso = _Proceso;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> LoginUser(string user, string password)
        {
            Respuesta respuesta = new Respuesta();
            string jsonUserApi = Funcion.BuildUserApiStr(user, password, Tool);
            EngineProcesor proceso = new EngineProcesor();
            TicketAcceso ticket =  await Proceso.GetTicketAccesoAsync(jsonUserApi, FuncionHttp);
            if (!string.IsNullOrEmpty(ticket.access_token))
            {
                respuesta.Descripcion = "Autentificacion Exitosa";
                respuesta.Resultado = true;
            }
            else
            {
                respuesta.Descripcion = "Autentificacion Fallida";
                respuesta.Resultado = false;
                System.Web.HttpContext.Current.Session["User"] = null;
            }
            return Json(respuesta);
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