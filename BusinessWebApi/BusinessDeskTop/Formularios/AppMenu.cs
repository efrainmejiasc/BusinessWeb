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
            if (DateTime.Now.Date >= Convert.ToDateTime("20/03/2020"))
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
    }
}
