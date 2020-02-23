using BusinessDeskTop.Engine.Interfaces;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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

        public string ConvertirBase64(string cadena)
        {
            var comprobanteXmlPlainTextBytes = Encoding.UTF8.GetBytes(cadena);
            var cadenaBase64 = Convert.ToBase64String(comprobanteXmlPlainTextBytes);
            return cadenaBase64;
        }

        public string ConvertImgTo64Img(string pathFile)
        {
            byte[] imageArray = System.IO.File.ReadAllBytes(pathFile);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            return base64ImageRepresentation;
        }

        public string CreateQrCode(string source,string pathFile)
        {
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();
            qrEncoder.TryEncode(source, out qrCode);
            GraphicsRenderer renderer = new GraphicsRenderer(new FixedCodeSize(400, QuietZoneModules.Zero), Brushes.Black, Brushes.White);
            MemoryStream ms= new MemoryStream();
            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
            Bitmap imagenTemporal = new Bitmap(ms);
            var imagen = new Bitmap(imagenTemporal, new Size(new Point(200, 200)));
            imagen.Save(pathFile, ImageFormat.Png);

            return pathFile;
        }
    }
}
