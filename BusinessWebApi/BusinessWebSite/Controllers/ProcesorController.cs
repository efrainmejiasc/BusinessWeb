
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

        public ProcesorController(IEngineProject _Funcion, IEngineHttp _FuncionHttp, IEngineTool _Tool, IEngineProcesor _Proceso)
        {
            this.Funcion = _Funcion;
            this.FuncionHttp = _FuncionHttp;
            this.Tool = _Tool;
            this.Proceso = _Proceso;
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
            if (System.Web.HttpContext.Current.Session["AccessToken"] == null)
                return RedirectToAction("Index", "Home");

            Person persona = new Person();
            string token = System.Web.HttpContext.Current.Session["AccessToken"].ToString();
            string jsonPerson = await Proceso.GetPerson(dni, token, FuncionHttp);
            return Json(jsonPerson);
        }
    }
}