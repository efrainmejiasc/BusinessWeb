using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BusinessWebSite.Engine
{
    public class EngineData
    {
        private static EngineData valor;

        public static EngineData Instance()
        {
            if ((valor == null))
            {
                valor = new EngineData();
            }
            return valor;
        }

        public static string UrlBase = ConfigurationManager.AppSettings["URL_BASE"];
    }
}