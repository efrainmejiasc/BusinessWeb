using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessWebSite.Engine.Interfaces
{
    public interface IEngineTool
    {
        bool EmailEsValido(string email);
        string ConvertirBase64(string cadena);
        bool CompareString(string a, string b);
        string DecodeBase64(string base64EncodedData);
    }
}