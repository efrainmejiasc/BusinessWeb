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
    public partial class AddCompany : Form
    {
        private EngineData Valor = EngineData.Instance();
        public AddCompany()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EngineProject Funcion = new EngineProject();
            EngineHttp FuncionHttp = new EngineHttp();
            EngineTool Tool = new EngineTool();
            EngineProcesor Proceso = new EngineProcesor(FuncionHttp, Funcion, Tool);
            if (string.IsNullOrEmpty(txtName.Text)|| string.IsNullOrEmpty(txtRef.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtDevice.Text) || string.IsNullOrEmpty(txtTlf.Text))
            {
                MessageBox.Show("Todos los campos son requeridos", "INFORMACION DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!Tool.EmailEsValido(txtEmail.Text))
            {
                MessageBox.Show("Email no valido", "INFORMACION DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
             Proceso.Company(txtName.Text, txtEmail.Text, txtRef.Text, txtTlf.Text, txtDevice.Text);
        }
    }
}
