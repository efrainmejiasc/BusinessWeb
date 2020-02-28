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
            System.Web.HttpContext.Current.Session["User"] = user;
            return JsonConvert.SerializeObject(modelo);
        }
    }
}