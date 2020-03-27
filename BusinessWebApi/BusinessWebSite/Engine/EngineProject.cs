using BusinessWebSite.Engine.Interfaces;
using BusinessWebSite.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessWebSite.Engine
{
    public class EngineProject:IEngineProject
    {

        public string BuildUserApiStr(string user, string password,IEngineTool Tool)
        {
            UserApi modelo = new UserApi();
            bool result = Tool.EmailEsValido(user);
            if (result)
            {
                modelo.Email = user;
                modelo.User = "A";
            }
            else
            {
                modelo.User = user;
                modelo.Email = "A";
            }
            modelo.Password = password;
            return JsonConvert.SerializeObject(modelo);
        }

        public string BuildCreateUserApiStr(string user, string email, string password)
        {
            UserApi modelo = new UserApi()
            {
                User = user,
                Email = email,
                Password = password
            };
            return JsonConvert.SerializeObject(modelo);
        }

        public string BuildRegisterDeviceStr(string user, string email, string codigo , string phone,string dni,string nombre)
        {
            RegisterDevice modelo = new RegisterDevice()
            {
                User = user,
                Email = email,
                Codigo = codigo,
                Phone = phone,
                Dni = dni,
                Nombre = nombre
            };
            return JsonConvert.SerializeObject(modelo);
        }

        public string BuidObservacionAsistencia(int idAsistencia, string dni, bool status, string materia,string observacion ,string dniAdm , int idCompany)
        {
            ObservacionClase modelo = new ObservacionClase()
            {
                IdAsistencia = idAsistencia,
                Dni  = dni,
                Status = status,
                Materia = materia,
                Observacion = observacion,
                CreateDate = DateTime.Now.Date,
                DniAdm  = dniAdm,
                IdCompany = idCompany

            };
            return JsonConvert.SerializeObject(modelo);
        }


    }
}