﻿using BusinessWebApi.Engine;
using BusinessWebApi.Engine.Interfaces;
using BusinessWebApi.Models;
using BusinessWebApi.Models.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace BusinessWebApi.Controllers
{
    public class AsistenciaClaseApiController : ApiController
    {
        private readonly IEngineTool Tool;
        private readonly IEngineDb Metodo;
        private readonly IEngineProject Funcion;
        private readonly IEngineNotify Notify;
        public AsistenciaClaseApiController(IEngineTool _tool, IEngineDb _metodo, IEngineProject _funcion , IEngineNotify _notyfy)
        {
            Tool = _tool;
            Metodo = _metodo;
            Funcion = _funcion;
            Notify = _notyfy;
        }

        [HttpPost]
        [ActionName("AsistenciaClase")]
        public HttpResponseMessage AsistenciaClase([FromBody] List<AsistenciaClase> model)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

            if (model.Count == 0)
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                response.Content = new StringContent(EngineData.modeloImcompleto, Encoding.Unicode);
                return response;
            }

            bool resultado = true;
            try 
            {
                Metodo.NewAsistenciaClase(model);
                List<AsistenciaClase> noAsistentes = Metodo.StudentsNonAttending();
                List<Person> personas = Metodo.GetPerson(noAsistentes);
                List<DataEmailNoAsistencia> emailNoAsistentes = Funcion.BuildDataEmailNoAsistencia(personas);
                Notify.EnviarEmailNoAsistentes(emailNoAsistentes);
                Metodo.UpdateAsistencia(noAsistentes);
            }
            catch {resultado = false;}
            
            if (!resultado)
            {
                response.Content = new StringContent(EngineData.falloCrearUsuario, Encoding.Unicode);
            }
            else
            {
                response.Content = new StringContent(EngineData.transaccionExitosa, Encoding.Unicode);
                response.Headers.Location = new Uri(EngineData.UrlBase + EngineData.UrlLogin);
            }

            return response;
        }

        [HttpGet]
        [ActionName("GetAsistenciaClase")]
        public List<Asistencia> GetAsistenciaClase(string fecha, string grado, string grupo,int idCompany)
        {
            List<Asistencia> lista = Metodo.GetAsistenciaClase(fecha, grado, grupo, idCompany);
            return lista;
        }
    }
}
