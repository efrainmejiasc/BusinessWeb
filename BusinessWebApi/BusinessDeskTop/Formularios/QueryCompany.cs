using BusinessDeskTop.Engine;
using BusinessDeskTop.Models;
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
    public partial class QueryCompany : Form
    {
        private EngineData Valor = EngineData.Instance();
        private EngineProject Funcion = new EngineProject();
        private EngineHttp FuncionHttp = new EngineHttp();
        private EngineTool Tool = new EngineTool();
        int ide = 0;
        public QueryCompany()
        {
            InitializeComponent();
        }
        private void QueryCompany_Load(object sender, EventArgs e)
        {
            estado.Items.Add("ACTIVO");
            estado.Items.Add("INACTIVO");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetAllCompanyAsync();
        }

        public async Task GetAllCompanyAsync()
        {
            EngineProcesor proceso = new EngineProcesor(FuncionHttp, Funcion, Tool);
            await proceso.GetAllCompany(dgv);
        }

        private void dgv_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            EngineProcesor proceso = new EngineProcesor();
            dgv = proceso.ColorFila(dgv, Color.WhiteSmoke, Color.Gainsboro);
            dgv.ClearSelection();
        }

        private void dgv_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dgv.CurrentRow;
            Company  company = new Company()
            {
                Id = Convert.ToInt32(row.Cells["ID"].Value),
                NameCompany = row.Cells["NOMBRE EMPRESA"].Value.ToString(),
                Ref = row.Cells["NIT"].Value.ToString(),
                Email= row.Cells["EMAIL"].Value.ToString(),
                Phone = row.Cells["TELEFONO"].Value.ToString(),
                Status = Convert.ToBoolean(row.Cells["ESTADO"].Value.ToString().ToLower()),
                NumberDevices = Convert.ToInt32(row.Cells["NUMERO EQUIPOS"].Value)
            };
            this.ide = company.Id;
            name.Text = company.NameCompany;
            rifi.Text = company.Ref;
            mail.Text = company.Email;
            tlf.Text = company.Phone;
            numero.Text = company.NumberDevices.ToString();
            if (company.Status)
                estado.SelectedIndex = 0;
            else if (!company.Status)
                estado.SelectedIndex = 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Company c = new Company();
            c.Id = this.ide;
            c.NameCompany = name.Text;
            c.Ref = rifi.Text;
            c.Email = mail.Text;
            c.Phone = tlf.Text;
            c.NumberDevices = Convert.ToInt32(numero.Text);
            if (estado.SelectedIndex == 0)
                c.Status = true;
            else if (estado.SelectedIndex == 1)
                c.Status = false;

            EngineProcesor Proceso = new EngineProcesor(FuncionHttp, Funcion, Tool);
            Proceso.UpdateCompany(c);
        }
    }
}
