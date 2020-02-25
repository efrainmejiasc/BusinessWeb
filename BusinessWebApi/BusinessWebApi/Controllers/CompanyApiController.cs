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
        public CompanyApiController(IEngineTool _tool, IEngineDb _metodo)
        {
            Tool = _tool;
            Metodo = _metodo;
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
            int r = Metodo.CreateCompany(company);
            if (r == 0)
            {
                response.Content = new StringContent(EngineData.falloCrearCompany, Encoding.Unicode);
            }
            else
            {
                response.Content = new StringContent(EngineData.transaccionExitosa, Encoding.Unicode);
                response.Headers.Location = new Uri(EngineData.UrlBase + EngineData.UrlCompany + "/" + r.ToString());
            }
            return response;
        }

    }
}
