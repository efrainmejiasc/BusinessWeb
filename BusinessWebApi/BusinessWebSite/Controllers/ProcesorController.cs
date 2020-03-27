
using BusinessWebSite.Engine.Interfaces;
using BusinessWebSite.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BusinessWebSite.Controllers
{
    public class ProcesorController : Controller
    {
        private readonly IEngineProject Funcion;
        private readonly IEngineHttp FuncionHttp;
        private readonly IEngineTool Tool;
        private readonly IEngineProcesor Proceso;
        private readonly IEngineRead Lector;
        private readonly IEngineNotify Notify;

       public ProcesorController(IEngineProject _Funcion, IEngineHttp _FuncionHttp, IEngineTool _Tool, IEngineProcesor _Proceso , IEngineRead _Lector,IEngineNotify _Notify)
        {
            this.Funcion = _Funcion;
            this.FuncionHttp = _FuncionHttp;
            this.Tool = _Tool;
            this.Proceso = _Proceso;
            this.Lector = _Lector;
            this.Notify = _Notify;
        }
        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.Session["User"] == null)
                Response.Redirect("Index");

            return View();
        }


        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            ViewBag.Response = null;
            if (file != null)
            {
                string[] extFile = file.FileName.Split('.');
                if (extFile[1] != "xls" && extFile[1] != "xlsx")
                {
                    ViewBag.Response = "El archivo seleccionado debe ser de tipo Excel";
                    return View("Index");
                }
                try
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/FileExcel/"), fileName);
                    file.SaveAs(path);
                    ViewBag.Response = "Archivo " + file.FileName + " cargado correctamente";
                }
                catch (Exception ex)
                {
                    ViewBag.Response = "ERROR: " + ex.Message.ToString();
                }
            }
            else
            {
                ViewBag.Response = "Selecciona un archivo";
            }
            return View("Index");
        }

        public ActionResult Contact()
        {
            if (System.Web.HttpContext.Current.Session["User"] == null)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> BuscarPersona(string dni)
        {
            if (System.Web.HttpContext.Current.Session["User"] == null)
                return RedirectToAction("Index", "Home");

            string token = string.Empty;
            if (System.Web.HttpContext.Current.Session["AccessToken"] != null)
                token = System.Web.HttpContext.Current.Session["AccessToken"].ToString();

            string jsonPerson = await Proceso.GetPerson(dni, token, FuncionHttp);
            return Json(jsonPerson);
        }

        [HttpPost]
        public async Task<ActionResult> BuscarPersonaGrado(string grado, string grupo )
        {
            if (System.Web.HttpContext.Current.Session["User"] == null)
                return RedirectToAction("Index", "Home");

            string token = string.Empty;
            if (System.Web.HttpContext.Current.Session["AccessToken"] != null)
                token = System.Web.HttpContext.Current.Session["AccessToken"].ToString();

            int idCompany = 0;
            if (System.Web.HttpContext.Current.Session["IdCompany"] != null)
                idCompany = Convert.ToInt32(System.Web.HttpContext.Current.Session["IdCompany"]);
            else
                return Json(null);

            string jsonPerson = await Proceso.GetPerson(grado, grupo, idCompany,token, FuncionHttp);
            return Json(jsonPerson);
        }


        [HttpPost]
        public async Task<ActionResult> GetHistoriaAsistenciaPerson(string dni)
        {
            if (System.Web.HttpContext.Current.Session["User"] == null)
                return RedirectToAction("Index", "Home");

            string token = string.Empty;
            if (System.Web.HttpContext.Current.Session["AccessToken"] != null)
                token = System.Web.HttpContext.Current.Session["AccessToken"].ToString();

            string jsonHistoria= await Proceso.GetHistoriaAsistenciaPerson(dni, token, FuncionHttp);
            return Json(jsonHistoria);
        }


        public ActionResult ReportAssistance()
        {
            if (System.Web.HttpContext.Current.Session["User"] == null)
                return RedirectToAction("Index", "Home");

            return View();
        }

        public ActionResult QueryAssistance()
        {
            if (System.Web.HttpContext.Current.Session["User"] == null)
                return RedirectToAction("Index", "Home");

            return View();
        }


        [HttpPost]
        public async Task<ActionResult> GetGrados()
        {
            if (System.Web.HttpContext.Current.Session["User"] == null)
                return RedirectToAction("Index", "Home");

            string token = string.Empty;
            if (System.Web.HttpContext.Current.Session["AccessToken"] != null)
                token = System.Web.HttpContext.Current.Session["AccessToken"].ToString();
            string jsonGrado = await Proceso.GetGrados(token, FuncionHttp);

            return Json(jsonGrado);
        }

        [HttpPost]
        public async Task<ActionResult> GetGrupos()
        {
            if (System.Web.HttpContext.Current.Session["User"] == null)
                return Json(null);

            string token = string.Empty;
            if (System.Web.HttpContext.Current.Session["AccessToken"] != null)
                token = System.Web.HttpContext.Current.Session["AccessToken"].ToString();

            string jsonGrado = await Proceso.GetGrupos(token, FuncionHttp);

            return Json(jsonGrado);
        }


        [HttpPost]
        public async Task<ActionResult> GetAsistencia (string fecha , string grado ,string grupo)
        {
            if (System.Web.HttpContext.Current.Session["User"] == null)
                return Json(null);

            int idCompany = 0;
            if (System.Web.HttpContext.Current.Session["IdCompany"] != null)
                idCompany = Convert.ToInt32(System.Web.HttpContext.Current.Session["IdCompany"]);
            else
                return Json(null);


            string token = System.Web.HttpContext.Current.Session["AccessToken"].ToString();
            string jsonAsistencia= await Proceso.GetAsistencia (token,fecha, grado, grupo,idCompany,FuncionHttp);

            return Json(jsonAsistencia);
        }

        [HttpPost]
        public async Task<ActionResult> EditAtending (int idAsistencia, string dni , bool status, string materia ,string observacion,string dniAdm)
        {
            if (System.Web.HttpContext.Current.Session["User"] == null)
                return Json(null);

            int idCompany = 0;
            if (System.Web.HttpContext.Current.Session["IdCompany"] != null)
                idCompany = Convert.ToInt32(System.Web.HttpContext.Current.Session["IdCompany"]);
            else
                return Json(null);

            string token = System.Web.HttpContext.Current.Session["AccessToken"].ToString();

            string jsonData = Funcion.BuidObservacionAsistencia(idAsistencia, dni, status, materia, observacion, dniAdm, idCompany);
            bool resultado = await Proceso.UpdateObservacionAsistencia(jsonData, token, FuncionHttp);
            Respuesta respuesta = new Respuesta();
            if (resultado)
                respuesta.Descripcion = "Transaccion Exitosa";
            else
                respuesta.Descripcion = "Transaccion Fallida";

            return Json(respuesta);
        }


        [HttpPost]
        public async Task<ActionResult> EnviarEmail(string dni, string email,string asunto,string mensaje ,bool resumen)
        {
            Respuesta respuesta = new Respuesta();

            string token = string.Empty; string jsonHistoria = string.Empty; string pathAdjunto = string.Empty;
            if (resumen)
            {
                if (System.Web.HttpContext.Current.Session["AccessToken"] != null)
                {
                    token = System.Web.HttpContext.Current.Session["AccessToken"].ToString();
                }
                else
                {
                    respuesta.Descripcion = "Token expiro";
                    return Json(respuesta);
                }

                jsonHistoria = await Proceso.GetHistoriaAsistenciaPerson(dni, token, FuncionHttp);
                List<HistoriaAsistenciaPerson> historia = new List<HistoriaAsistenciaPerson>();
                historia = JsonConvert.DeserializeObject<List<HistoriaAsistenciaPerson>>(jsonHistoria);
            }

            bool resultado = Notify.EnviarEmail(email, asunto, mensaje, pathAdjunto);
            if (resultado)
                respuesta.Descripcion = "Email enviado";
            else
                respuesta.Descripcion = "Fallo el envio";

            return Json(respuesta);
        }

    }
}