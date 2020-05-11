using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDeskTop.Engine.Interfaces
{
    public interface IEngineTool
    {
        string ConstruirCodigo();
        string DataLoginUserApi();
        bool CreateFolder(string path);
        bool EmailEsValido(string email);
        bool DeletFile(string pathArchivo);
        bool CreateFile(string pathArchivo);
        bool ExistsFile(string pathArchivo);
        string ConvertirBase64(string cadena);
        byte[] ImageToByteArray(Image imageIn);
        string ConvertImgTo64Img(string pathFile);
        Image ByteArrayToImage(byte[] byteArrayIn);
        string CreateQrCode(string source, string pathFile);
    }
}
