using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessWebApi.Engine
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
        public static string SecretKey = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
        public static string Audience = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"];
        public static string Issuer = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];
        public static int ExpireToken = Convert.ToInt32(ConfigurationManager.AppSettings["JWT_EXPIRE_MINUTES"]);
        public static string UrlLogin = "api/UserApi/Login";
        public static string UrlPersons = "api/PersonApi/QueryPersonas";

        public static string modeloImcompleto = "modelo incompleto";
        public static string emailNoValido = "email no valido";
        public static string passwordDiferente = "los passwords son diferentes";
        public static string falloCrearUsuario = "error al crear usuario";
        public static string transaccionExitosa = "transaccion exitosa";
        public static string transaccionFallida = "transaccion fallida";
        public static string noExisteUsuario = "no existe usuario";
        public static string falloCrearPersonas = "error al crear personas";
        public static string falloUpdatePersonas = "error al actualizar personas";

        public static string Client = "DataCompany/GetClient?nameCompany=";
        public static string NoData = "No Existen Registros";
    }
}
