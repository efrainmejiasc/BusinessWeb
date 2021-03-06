﻿using BusinessWebApi.Engine;
using BusinessWebApi.Engine.Interfaces;
using BusinessWebApi.Models;
using BusinessWebApi.Models.Objetos;
using Newtonsoft.Json;
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
            if ( persons == null || persons.Count == 0)
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

        [HttpGet]
        [ActionName("GetPerson")]
        public Person GetPerson(string identificador)
        {
            Person person = Metodo.GetPerson2(identificador);
            return person;
        }

        [HttpGet]
        [ActionName("GetPersonEspecific")]
        public Person GetPersonEspecific(string dni)
        {
            Person person = Metodo.GetPerson(dni);
            return person;
        }

        [HttpGet]
        [ActionName("GetPersonList")]
        public List<Person> GetPersonList(int idCompany, string grado, string grupo, int idTurno = 1)
        {
            List<Person> person = Metodo.GetPersonList(idCompany, grado, grupo, idTurno);
            foreach(var i in person)
            {
                i.Foto = string.Empty;
                i.Qr = string.Empty;
            }
            return person;
        }

        [HttpGet]
        [ActionName("GetPersonFull")]
        public List<Person> GetPersonFull(int idCompany, string grado, string grupo, int idTurno = 1)
        {
            List<Person> person = Metodo.GetPersonList(idCompany, grado, grupo, idTurno);
            foreach (var i in person)
            {
                i.Qr = string.Empty;
            }
            return person;
        }

        [HttpGet]
        [ActionName("GetUrlCarnet")]
        public string  GetUrlCarnet(int dni)
        {
            return EngineData.UrlDominio + "digitalcard/" + dni + ".jpg";
        }

        [HttpGet]
        [ActionName("GetGrados")]
        public List<Grado> GetGrados(int idCompany)
        {
           List<Grado> grado = Metodo.GetGrados(idCompany);
           return grado;
        }


        [HttpGet]
        [ActionName("GetGrupos")]
        public List<Grupo> GetGrupos(int idCompany)
        {
            List<Grupo> grado = Metodo.GetGrupos(idCompany);
            return grado;
        }

        [HttpGet]
        [ActionName("GetTurnos")]
        public List<Models.Objetos.Turno> GetTurnos(int idCompany)
        {
            List <Models.Objetos.Turno> turno = Metodo.GetTurnos(idCompany);
            return turno;
        }
    }
}
