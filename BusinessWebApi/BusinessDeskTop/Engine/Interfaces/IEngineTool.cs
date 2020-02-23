using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDeskTop.Engine.Interfaces
{
    public interface IEngineTool
    {
        bool CreateFolder(string path);
        bool EmailEsValido(string email);
        bool DeletFile(string pathArchivo);
        bool CreateFile(string pathArchivo);
        bool ExistsFile(string pathArchivo);
        string ConvertirBase64(string cadena);
        string ConvertImgTo64Img(string pathFile);
        string CreateQrCode(string source, string pathFile);
    }
}
