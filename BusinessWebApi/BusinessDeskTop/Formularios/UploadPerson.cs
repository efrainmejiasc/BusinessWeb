using BusinessDeskTop.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessDeskTop.Formularios
{
    public partial class UploadPerson : Form
    {
        private string tipo = string.Empty;
        public UploadPerson()
        {
            InitializeComponent();
        }

        public UploadPerson(string _tipo)
        {
            this.tipo = _tipo;
        }

        private void UploadPerson_Load(object sender, EventArgs e)
        {
            this.openFileDialog1.FileName = string.Empty;
            this.openFileDialog1.Filter = "Archivo Excel (*.xlsx)|*.xlsx| Archivo Excel *.xls)|*.xls|Todos los archivos (*.*)|*.*";
            this.openFileDialog1.Title = "Buscar lista";
            this.openFileDialog1.ShowDialog();
            string pathArchivo = openFileDialog1.FileName;
            if (!string.IsNullOrEmpty(pathArchivo))
            {
                LeerArchivo(pathArchivo);
            }
        }

        private bool LeerArchivo (string pathArchivo)
        {
            bool result = false;
            EngineProject Funcion = new EngineProject();
            EngineHttp FuncionHttp = new EngineHttp();
            EngineProcesor Proceso = new EngineProcesor(FuncionHttp ,Funcion);
            result = Proceso.LeerArchivo(pathArchivo);
            return result;
        }
    }
}
