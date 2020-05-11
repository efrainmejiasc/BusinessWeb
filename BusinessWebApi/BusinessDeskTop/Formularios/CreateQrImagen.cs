using BusinessDeskTop.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessDeskTop.Formularios
{
    public partial class CreateQrImagen : Form
    {
        private EngineProject Funcion = new EngineProject();
        private EngineHttp FuncionHttp = new EngineHttp();
        private EngineTool Tool = new EngineTool();
        public CreateQrImagen()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = @"C:\Users\ASUS\Downloads\CARNETIZACION\JPEG\";
            string pathDestino = @"C:\Users\ASUS\Downloads\CARNETIZACION\QR_IMAGEN\";
            DirectoryInfo d = new DirectoryInfo(@"C:\Users\ASUS\Downloads\CARNETIZACION\JPEG");
            FileInfo[] Files = d.GetFiles("*.jpg");
            EngineProcesor Proceso = new EngineProcesor(FuncionHttp, Funcion, Tool);
            foreach (FileInfo file in Files)
            {
                string [] p = file.Name.Split('.');
                Proceso.CreateQRImagen(path + file.Name, pathDestino + p[0] + ".png"); 
            }

          
          
        }
    }
}
