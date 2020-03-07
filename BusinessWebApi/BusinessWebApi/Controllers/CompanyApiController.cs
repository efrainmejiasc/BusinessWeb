using BusinessWebApi.Engine;
using BusinessWebApi.Engine.Interfaces;
using BusinessWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace BusinessWebApi.Controllers
{
    public class CompanyApiController : ApiController
    {
        private readonly IEngineTool Tool;
        private readonly IEngineDb Metodo;
        private readonly IEngineNotify Notificacion;
        public CompanyApiController(IEngineTool _tool, IEngineDb _metodo, IEngineNotify _notificacion)
        {
            Tool = _tool;
            Metodo = _metodo;
            Notificacion = _notificacion;
        }

        [HttpPost]
        [ActionName("CreateCompany")]
        public HttpResponseMessage CreateCompany([FromBody] Company company)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            if (company == null)
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                response.Content = new StringContent(EngineData.modeloImcompleto, Encoding.Unicode);
                return response;
            }
            bool resultado = Tool.EmailEsValido(company.Email);
            if (!resultado)
            {
                response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                response.Content = new StringContent(EngineData.emailNoValido, Encoding.Unicode);
                return response;
            }
            company.Codigo = Tool.ConstruirCodigo();
            resultado = Metodo.CreateCompany(company);
            if (!resultado)
            {
                response.Content = new StringContent(EngineData.falloCrearCompany, Encoding.Unicode);
            }
            else
            {
                Notificacion.EnviarEmail(company.Email, company.Codigo, company.NameCompany);
                response.Content = new StringContent(EngineData.transaccionExitosa, Encoding.Unicode);
                response.Headers.Location = new Uri(EngineData.UrlBase + EngineData.UrlCompany);
            }
            return response;
        }

        [HttpGet]
        [ActionName("GetAllCompany")]
        public List<Company> GetAllCompany()
        {
            List<Company> companys = Metodo.GetAllCompany();
            return companys;
        }
    }
}
