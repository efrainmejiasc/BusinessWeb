
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

        public ProcesorController(IEngineProject _Funcion, IEngineHttp _FuncionHttp, IEngineTool _Tool, IEngineProcesor _Proceso , IEngineRead _Lector)
        {
            this.Funcion = _Funcion;
            this.FuncionHttp = _FuncionHttp;
            this.Tool = _Tool;
            this.Proceso = _Proceso;
            this.Lector = _Lector;
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


        public ActionResult ReportAssistance()
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
                return RedirectToAction("Index", "Home");

            string token = string.Empty;
            if (System.Web.HttpContext.Current.Session["AccessToken"] != null)
                token = System.Web.HttpContext.Current.Session["AccessToken"].ToString();

            string jsonGrado = await Proceso.GetGrupos(token, FuncionHttp);

            return Json(jsonGrado);
        }


        [HttpPost]
        public async Task<ActionResult> GetAsistencia (string fecha , string grado ,string grupo)
        {
            if (System.Web.HttpContext.Current.Session["AccessToken"] == null)
                return RedirectToAction("Index", "Home");

            int idCompany = 0;
            if (System.Web.HttpContext.Current.Session["IdCompany"] != null)
            {
                idCompany = Convert.ToInt32(System.Web.HttpContext.Current.Session["IdCompany"]);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            string token = System.Web.HttpContext.Current.Session["AccessToken"].ToString();
            string jsonGrado = await Proceso.GetAsistencia (token,fecha, grado, grupo,idCompany,FuncionHttp);

            return Json(jsonGrado);
        }
    }
}