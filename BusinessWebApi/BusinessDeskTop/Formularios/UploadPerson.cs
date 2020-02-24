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
        private EngineData Valor = EngineData.Instance();
        private string tipo = string.Empty;
        public UploadPerson()
        {
            InitializeComponent();
        }

        private void UploadPerson_Load(object sender, EventArgs e)
        {       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmpresa.Text))
            {
                MessageBox.Show("Ingrese nombre empresa", "INFORMACION DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Valor.NombreEmpresa = txtEmpresa.Text.Trim().Replace(' ', '_') ;
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
            EngineTool Tool = new EngineTool();
            EngineProcesor Proceso = new EngineProcesor(FuncionHttp ,Funcion,Tool);
            try {
                result = Proceso.ProcesarArchivo(pathArchivo,dgv);
                result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
    }
}
