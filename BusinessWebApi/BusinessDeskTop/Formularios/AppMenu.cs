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
    public partial class AppMenu : Form
    {
        public AppMenu()
        {
            InitializeComponent();
        }

        private void subirListaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UploadPerson upload = new UploadPerson();
            upload.Show();
            //this.Hide();
        }

        private void agregarEmpresaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCompany company = new AddCompany();
            company.Show();
        }

        private void actualizarListaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdatePerson upload = new UpdatePerson();
            upload.Show();
        }

        private void AppMenu_Load(object sender, EventArgs e)
        {

            //ObtenerImagenes();
            //UpdatePersonFoto();
            if (DateTime.Now.Date >= Convert.ToDateTime("25/04/2020"))
            {
                MessageBox.Show("El Tiempo de prueba expiro", "INFORMACION DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
            }

        }

        private void consultasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QueryCompany query = new QueryCompany();
            query.Show();
        }

        private void crearQrDesdeImagenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateQrImagen QrI = new CreateQrImagen();
            QrI.Show();
        }

        private void ObtenerImagenes()
        {
            string[] files = Directory.GetFiles(@"C:\Users\ASUS\Downloads\CARNETIZACION\Ado\FOTOS\");
            string savePath = @"C:\Users\ASUS\Downloads\CARNETIZACION\Ado\FOTOS2\";
            for(int i = 0; i<= files.Length -1; i++)
            {
                var nombre = Path.GetFileName(files[i]);
                Bitmap orig = new Bitmap(files[i].Trim());
                Bitmap bmp = new Bitmap(Redimensionar (orig,140,180));
                bmp.Save(savePath + nombre, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        private Bitmap Redimensionar(Image I , int alto , int ancho)
        {
            var radio = Math.Max((double)ancho / I.Width, (double)alto / I.Height);
            int nuevoAncho = Convert.ToInt32(I.Width * radio);
            int nuevoAlto = Convert.ToInt32(I.Height * radio);
            var imgNueva = new Bitmap(nuevoAncho, nuevoAlto);
            Graphics.FromImage(imgNueva).DrawImage(I, 0, 0, nuevoAncho, nuevoAlto);
            Bitmap final = new Bitmap(imgNueva);
            return final;
        }

        private void UpdatePersonFoto()
        {
            var tool = new EngineTool();
            var Funcion = new EngineHttp();
            EngineProject project = new EngineProject();
            project.UpdatePersonFoto(tool, Funcion);
        }
    }
}
