using BusinessWebApi.Engine.Interfaces;
using BusinessWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BusinessWebApi.Controllers
{
    public class ToolCompanyController : ApiController
    {
        private readonly IEngineTool Tool;
        private readonly IEngineDb Metodo;
        private readonly IEngineNotify Notificacion;
        public ToolCompanyController(IEngineTool _tool, IEngineDb _metodo, IEngineNotify _notificacion)
        {
            Tool = _tool;
            Metodo = _metodo;
            Notificacion = _notificacion;
        }


        [HttpGet]
        [ActionName("GetMaterias")]
        public List<Materias> GetAllCompany(int idCompany)
        {
            List<Materias> materias = Metodo.GetMaterias(idCompany);
            return materias;
        }
    }
}
