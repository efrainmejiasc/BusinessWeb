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
        public static string UrlCompany = "api/CompanyApi/QueryCompany";
        public static string UrlAsistenciaClase = "api/PersonApi/NOMBREMETODO";
        public static string UrlAsistenciaComedor= "api/CompanyApi/QueryAsistenciaComedor";
        public static string UrlDevices = "api/DevicesApi/QueryDevicesApi";

        public static string modeloImcompleto = "modelo incompleto";
        public static string emailNoValido = "email no valido";
        public static string passwordDiferente = "los passwords son diferentes";
        public static string falloCrearUsuario = "error al crear usuario";
        public static string transaccionExitosa = "transaccion exitosa";
        public static string transaccionFallida = "transaccion fallida";
        public static string noExisteUsuario = "no existe usuario";
        public static string falloCrearPersonas = "error al crear personas";
        public static string falloUpdatePersonas = "error al actualizar personas";
        public static string falloCrearCompany = "error al crear company";

        public static string Client = "DataCompany/GetClient?nameCompany=";
        public static string NoData = "No Existen Registros";

        public static string[] AlfabetoG = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };//0-25
        public static string[] AlfabetoP = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
    }
}
