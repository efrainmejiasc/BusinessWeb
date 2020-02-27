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
        private EngineProject Funcion = new EngineProject();
        private EngineHttp FuncionHttp = new EngineHttp();
        private EngineTool Tool = new EngineTool();
        public UploadPerson()
        {
            InitializeComponent();
        }

        private void UploadPerson_Load(object sender, EventArgs e)
        {       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lblMsj.Text = string.Empty;
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
               ProcesarArchivo(pathArchivo);
            }
        }

        private bool ProcesarArchivo(string pathArchivo)
        {
            bool result = false;
            EngineProcesor Proceso = new EngineProcesor(FuncionHttp ,Funcion,Tool);
            try {
                result = Proceso.ProcesarArchivo(pathArchivo,dgv,lblMsj);
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar a excel", "INFORMACION DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            EngineProcesor Proceso = new EngineProcesor(FuncionHttp, Funcion, Tool);
            DataTable dt = new DataTable();
            dt = (DataTable) dgv.DataSource;
            Proceso.ExportarErrores(dt);
        }
    }
}
