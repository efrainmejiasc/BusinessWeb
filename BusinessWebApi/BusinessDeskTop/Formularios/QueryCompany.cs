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
    public partial class QueryCompany : Form
    {
        private EngineData Valor = EngineData.Instance();
        private EngineProject Funcion = new EngineProject();
        private EngineHttp FuncionHttp = new EngineHttp();
        private EngineTool Tool = new EngineTool();
        public QueryCompany()
        {
            InitializeComponent();
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
    }
}
