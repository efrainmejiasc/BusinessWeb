
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

        #region DEVUELVE VISTAS

        public ActionResult Index()
        {
            if (System.Web.HttpContext.Current.Session["User"] == null)
                Response.Redirect("Index");

            return View();
        }

        public ActionResult Contact()
        {
            if (System.Web.HttpContext.Current.Session["User"] == null)
                return RedirectToAction("Index", "Home");

            return View();
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

        #endregion

        [HttpPost] // Devuelve informacion de una persona especifica 
        public async Task<ActionResult> BuscarPersona(string dni)
        {
            if (System.Web.HttpContext.Current.Session["User"] == null)
                return Json(null);

            string token = string.Empty;
            if (System.Web.HttpContext.Current.Session["AccessToken"] != null)
                token = System.Web.HttpContext.Current.Session["AccessToken"].ToString();

            string jsonPerson = await Proceso.GetPerson(dni, token, FuncionHttp);
            return Json(jsonPerson);
        }

        [HttpPost] // Devuelve informacion lista de personas
        public async Task<ActionResult> BuscarPersonaGrado(string grado, string grupo, int turno)
        {
            if (System.Web.HttpContext.Current.Session["User"] == null)
                return Json(null);

            string token = string.Empty;
            if (System.Web.HttpContext.Current.Session["AccessToken"] != null)
                token = System.Web.HttpContext.Current.Session["AccessToken"].ToString();

            int idCompany = 0;
            if (System.Web.HttpContext.Current.Session["IdCompany"] != null)
                idCompany = Convert.ToInt32(System.Web.HttpContext.Current.Session["IdCompany"]);
            else
                return Json(null);

            string jsonPerson = await Proceso.GetPerson(grado, grupo, idCompany,token, FuncionHttp, turno);
            return Json(jsonPerson);
        }

        [HttpPost] // Devuelve el historial de asistencias de una persona
        public async Task<ActionResult> GetHistoriaAsistenciaPerson(string dni)
        {
            if (System.Web.HttpContext.Current.Session["User"] == null)
                return Json(null);

            string token = string.Empty;
            if (System.Web.HttpContext.Current.Session["AccessToken"] != null)
                token = System.Web.HttpContext.Current.Session["AccessToken"].ToString();

            string jsonHistoria= await Proceso.GetHistoriaAsistenciaPerson(dni, token, FuncionHttp);
            return Json(jsonHistoria);
        }

        [HttpPost]// Devuelve lista de grados 
        public async Task<ActionResult> GetGrados() 
        {
            if (System.Web.HttpContext.Current.Session["User"] == null)
                return Json(null);

            string token = string.Empty;
            if (System.Web.HttpContext.Current.Session["AccessToken"] != null)
                token = System.Web.HttpContext.Current.Session["AccessToken"].ToString();

            int idCompany = 0;
            if (System.Web.HttpContext.Current.Session["IdCompany"] != null)
                idCompany = Convert.ToInt32(System.Web.HttpContext.Current.Session["IdCompany"]);
            else
                return Json(null);

            string jsonGrado = await Proceso.GetGrados(token,idCompany, FuncionHttp);

            return Json(jsonGrado);
        }

        [HttpPost] // Devuelve lista de secciones o grupos
        public async Task<ActionResult> GetGrupos()
        {
            if (System.Web.HttpContext.Current.Session["User"] == null)
                return Json(null);

            string token = string.Empty;
            if (System.Web.HttpContext.Current.Session["AccessToken"] != null)
                token = System.Web.HttpContext.Current.Session["AccessToken"].ToString();

            int idCompany = 0;
            if (System.Web.HttpContext.Current.Session["IdCompany"] != null)
                idCompany = Convert.ToInt32(System.Web.HttpContext.Current.Session["IdCompany"]);
            else
                return Json(null);

            string jsonGrado = await Proceso.GetGrupos(token, idCompany,FuncionHttp);

            return Json(jsonGrado);
        }

        [HttpPost] // Devuelve lista de turnos
        public async Task<ActionResult> GetTurnos()
        {
            if (System.Web.HttpContext.Current.Session["User"] == null)
                return Json(null);

            string token = string.Empty;
            if (System.Web.HttpContext.Current.Session["AccessToken"] != null)
                token = System.Web.HttpContext.Current.Session["AccessToken"].ToString();

            int idCompany = 0;
            if (System.Web.HttpContext.Current.Session["IdCompany"] != null)
                idCompany = Convert.ToInt32(System.Web.HttpContext.Current.Session["IdCompany"]);
            else
                return Json(null);

            string jsonGrado = await Proceso.GetTurnos(token, idCompany, FuncionHttp);

            return Json(jsonGrado);
        }

        [HttpPost] // Devuelve lista de asistencia por grado y grupo
        public async Task<ActionResult> GetAsistencia (string fecha , string grado ,string grupo, int turno)
        {
            if (System.Web.HttpContext.Current.Session["User"] == null)
                return Json(null);

            int idCompany = 0;
            if (System.Web.HttpContext.Current.Session["IdCompany"] != null)
                idCompany = Convert.ToInt32(System.Web.HttpContext.Current.Session["IdCompany"]);
            else
                return Json(null);


            string token = System.Web.HttpContext.Current.Session["AccessToken"].ToString();
            string jsonAsistencia= await Proceso.GetAsistencia (token,fecha, grado, grupo,idCompany,turno,FuncionHttp);

            return Json(jsonAsistencia);
        }

        [HttpPost] // Actualiza una asistencia o inserta una observacion
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


        [HttpPost] // Envia email con historia de asistencia
        public async Task<ActionResult> EnviarEmail(string dni, string nombre, string apellido, string email,string asunto,string mensaje ,bool resumen)
        {
            if (System.Web.HttpContext.Current.Session["User"] == null)
                return Json(null);

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
                if (!string.IsNullOrEmpty(jsonHistoria))
                {
                    List<HistoriaAsistenciaPerson> historia = new List<HistoriaAsistenciaPerson>();
                    historia = JsonConvert.DeserializeObject<List<HistoriaAsistenciaPerson>>(jsonHistoria);
                    pathAdjunto = Funcion.BuildXlsxAsistenciaClase(historia, nombre, apellido, dni);
                }
            }

            bool resultado = Notify.EnviarEmail(email, asunto, mensaje, pathAdjunto);
            if (resultado)
                respuesta.Descripcion = "Email enviado";
            else
                respuesta.Descripcion = "Fallo el envio";

            return Json(respuesta);
        }

        [HttpPost] // Subir archivo excel 
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

    }
}