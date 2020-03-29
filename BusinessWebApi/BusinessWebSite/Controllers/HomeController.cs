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

        public ActionResult Autentication()
        {
            return View();
        }

        [HttpPost] //LOGIN DE USUARIO
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
                System.Web.HttpContext.Current.Session["User"] = user;
                System.Web.HttpContext.Current.Session["Password"] = password;
                System.Web.HttpContext.Current.Session["Email"] = ticket.email;
                System.Web.HttpContext.Current.Session["AccessToken"] = ticket.access_token;
                System.Web.HttpContext.Current.Session["IdCompany"] = ticket.idCompany;
            }
            else
            {
                respuesta.Descripcion = "Autentificacion Fallida";
                respuesta.Resultado = false;
                System.Web.HttpContext.Current.Session["User"] = null;
                System.Web.HttpContext.Current.Session["Email"] = null;
                System.Web.HttpContext.Current.Session["AccessToken"] = null;
                System.Web.HttpContext.Current.Session["IdCompany"] = null;
                System.Web.HttpContext.Current.Session["Password"] = null;
            }
            return Json(respuesta);
        }

        public async Task<ActionResult> Contact (string user,string email,string password, string password2) //REGISTRO DE USUARIO
        {
            ViewBag.Response = null;
            if (Request.HttpMethod == "GET" || string.IsNullOrEmpty(user) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return View();
   
            bool result = Tool.EmailEsValido(email);
            if (!result)
                ViewBag.Response = "Email no valido";

            string jsonUserApi = Funcion.BuildCreateUserApiStr(user,email,password);
            bool resultado = await Proceso.CreateUserApi(jsonUserApi, FuncionHttp);
            if (resultado)
                ViewBag.Response = "Registro satisfactorio";
            else
                ViewBag.Response = "Registro fallido. Intente con otro usuario y asegurese de que su email no este registrado";
            return View();
        }

        public async Task<ActionResult> About(string phone,string dni,string codigo,string nombre) //REGISTRO DE DISPOSITIVO
        {
             if (System.Web.HttpContext.Current.Session["User"] == null)
                 Response.Redirect("Index");

            ViewBag.Response = null;
            if (Request.HttpMethod == "GET" || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(dni) || string.IsNullOrEmpty(codigo) || 
                                                                           System.Web.HttpContext.Current.Session["User"] == null || System.Web.HttpContext.Current.Session["Email"] == null)
                return View();

            string user = System.Web.HttpContext.Current.Session["User"].ToString();
            string email = System.Web.HttpContext.Current.Session["Email"].ToString();
            string token = System.Web.HttpContext.Current.Session["AccessToken"].ToString();
            string jsonData = Funcion.BuildRegisterDeviceStr(user, email, codigo, phone, dni,nombre);
            bool resultado = await Proceso.RegisterDevice(jsonData, token, FuncionHttp);
            if (resultado)
                ViewBag.Response = "Registro satisfactorio";
            else
                ViewBag.Response = "Registro fallido";
            return View();
        }

        public async Task<ActionResult> UpdatePassword(string user,string password)
        {
            if (string.IsNullOrEmpty(user) && string.IsNullOrEmpty(password))
               return View();

            string jsonUserApi = Funcion.BuildUserApiStr(user, password);
            bool resultado = await Proceso.UpdateUserApi(jsonUserApi, FuncionHttp);
            if (resultado)
                ViewBag.Response = "Contraseña actualizada";
            else
                ViewBag.Response = "Fallo actualizar contraseña";
            return View();
        }
    }
}