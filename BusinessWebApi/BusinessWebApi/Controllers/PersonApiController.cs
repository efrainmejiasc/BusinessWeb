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
    public class PersonApiController : ApiController
    {
        private readonly IEngineTool Tool;
        private readonly IEngineDb Metodo;
        public PersonApiController(IEngineTool _tool, IEngineDb _metodo)
        {
            Tool = _tool;
            Metodo = _metodo;
        }

        [HttpPost]
        [ActionName("CreatePerson")]
        public HttpResponseMessage CreatePerson ([FromBody] List<Person> persons)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            if (persons.Count == 0)
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                response.Content = new StringContent(EngineData.modeloImcompleto, Encoding.Unicode);
                return response;
            }
            bool resultado = Metodo.CreatePerson(persons);
            if (!resultado)
            {
                response.Content = new StringContent(EngineData.falloCrearPersonas, Encoding.Unicode);
            }
            else
            {
                response.Content = new StringContent(EngineData.transaccionExitosa, Encoding.Unicode);
                response.Headers.Location = new Uri(EngineData.UrlBase + EngineData.UrlPersons);
            }
            return response;
        }


        [HttpPut]
        [ActionName("UpdatePerson")]
        public HttpResponseMessage UpdatePerson([FromBody]  List<Person> persons)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            if (persons.Count == 0)
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                response.Content = new StringContent(EngineData.modeloImcompleto, Encoding.Unicode);
                return response;
            }
            bool resultado = Metodo.UpdatePerson(persons);
            if (!resultado)
            {
                response.Content = new StringContent(EngineData.falloUpdatePersonas, Encoding.Unicode);
            }
            else
            {
                response.Content = new StringContent(EngineData.transaccionExitosa, Encoding.Unicode);
                response.Headers.Location = new Uri(EngineData.UrlBase + EngineData.UrlPersons);
            }
            return response;
        }
    }
}
