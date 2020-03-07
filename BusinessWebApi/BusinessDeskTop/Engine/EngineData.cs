using BusinessDeskTop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDeskTop.Engine
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

        public string AccesToken { get; set; }

        public static string UrlBase = ConfigurationManager.AppSettings["URL_BASE"];
        public bool ResultAsync { get; set; }
        public  string NombreEmpresa {get;set; }

        public  string PathFolderFotos = @"C:\Fotos\";


        public string FolderFile = @"C:\APPArchivosExcel\";
        private string pathFolderFile = string.Empty;

        public string PathFolderFileEmpresa()
        {
            pathFolderFile = FolderFile + NombreEmpresa;
            return pathFolderFile;
        }

        private string pathFolderQr = string.Empty;
        public string PathFolderImageQr()
        {
            pathFolderQr = FolderFile + NombreEmpresa + @"\" + NombreEmpresa + "_QR";
            return pathFolderQr;
        }

        private string pathFileXlsx = string.Empty;

        public string PathFileXlsx()
        {
            string diferenciador = DateTime.Now.Date.ToString().Replace("/", "");
            diferenciador = diferenciador.Replace("\\", "");
            diferenciador = diferenciador.Replace(":", "");
            diferenciador = diferenciador.Replace(" ", "_");
            string[] p = diferenciador.Split('_');
            diferenciador = p[0];
            pathFileXlsx = FolderFile + NombreEmpresa + @"\" + NombreEmpresa + diferenciador + ".xlsx" ;
            return pathFileXlsx;
        }

        private DataTable dt = new DataTable();
        public void SetDt (DataTable _dt)
        {
            dt = _dt;
        }

        public DataTable GetDt()
        {
            return dt;
        }

        private List<Person> persons = new List<Person>();
        public void SetPersons(List<Person> _persons)
        {
            persons = _persons;
        }

        public List<Person> GetPersons()
        {
            return persons;
        }
    }
}
