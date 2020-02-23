using BusinessDeskTop.Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessDeskTop.Engine
{
    public class EngineTool : IEngineTool
    {
        public bool EmailEsValido(string email)
        {
            string expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            bool resultado = false;
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                {
                    resultado = true;
                }
            }
            return resultado;
        }

        public bool ExistsFile(string pathArchivo)
        {
            bool resultado = false;
            if (File.Exists(pathArchivo))
                resultado = true;
            return resultado;
        }

        public bool CreateFile(string pathArchivo)
        {
            bool resultado = false;
            try
            {
                if (!File.Exists(pathArchivo))
                    resultado = true;
            }
            catch { }
            return resultado;
        }

        public bool DeletFile(string pathArchivo)
        {
            bool resultado = false; 
            try
            {
                if (File.Exists(pathArchivo))
                {
                    File.Delete(pathArchivo);
                    resultado = true;
                }
            } 
            catch { }
            return resultado;
        }

        public bool CreateFolder(string path)
        {
            bool resultado = false;
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    resultado = true;
                }
            }
            catch { }
            return resultado;
        }
    }
}
