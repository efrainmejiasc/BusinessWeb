using BusinessDeskTop.Engine.Interfaces;
using BusinessDeskTop.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessDeskTop.Engine
{
    public  class EngineProcesor
    {
        private readonly IEngineTool Tool;
        private readonly IEngineHttp HttpFuncion;
        private readonly IEngineProject Funcion;
        private EngineData Valor = EngineData.Instance();
        public EngineProcesor(IEngineHttp _HttpFuncion , IEngineProject _Funcion, IEngineTool _Tool)
        {
            HttpFuncion = _HttpFuncion;
            Funcion = _Funcion;
            Tool = _Tool;
        }

        public EngineProcesor()
        {
        }

        #region UPLOADLISTA
     

        public bool ProcesarArchivo(string pathArchivo, DataGridView dgv, Label lbl)
        {
            bool resultado = false;
            List<Person> persons = Funcion.LeerArchivo(pathArchivo, Tool);
            resultado = Tool.CreateFolder(Valor.FolderFile); //path carpeta archivos 
            resultado = Tool.CreateFolder(Valor.PathFolderFileEmpresa()); // path carpeta archivos empresa
            resultado = Tool.CreateFolder(Valor.PathFolderImageQr()); // path carpeta qr empresa
            dgv.DataSource = Valor.GetDt();
            dgv.ClearSelection();
            if (dgv.Rows.Count == 0)
            {
                // MessageBox.Show("No existen datos para procesar", "INFORMACION DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //return true;
            }
            lbl.Text = "Numero de errores en el archivo : " + dgv.Rows.Count + Environment.NewLine + pathArchivo + Environment.NewLine +
                       "Insertando datos en Db" + Environment.NewLine + "Espere un momento, esto puede tardar unos segundos";

            string pathFileXlsx = Valor.PathFileXlsx();//path del archivo excel 
            resultado = Funcion.CreateFileXlsx(persons, pathFileXlsx, Tool);
            if (resultado)
                UploadPersonToApi(lbl, "INSERT");

            return resultado;
        }

        public bool UploadPersonToApi(Label lbl, string tipo)
        {
            bool resultado = false;
            List<Person> persons = Valor.GetPersons();
            resultado = HttpFuncion.UploadPersonToApi(persons,Funcion);
               
            if (resultado)
                MessageBox.Show("Transaccion exitosa", "INFORMACION DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Transaccion fallida", "INFORMACION DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return resultado;
        }


        public bool ExportarErrores(DataTable dt)
        {
            bool resultado = false;
            resultado = Funcion.CreateFileXlsx(dt);
            if (resultado)
                MessageBox.Show("Transaccion exitosa", "INFORMACION DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Transaccion fallida", "INFORMACION DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return resultado;
        }

        #endregion

        #region ACTUALIZAR LISTA
        public bool ProcesarArchivoActualizar(string pathArchivo, DataGridView dgv, Label lbl)
        {
            bool resultado = false;
            List<Person> persons = Funcion.LeerArchivo(pathArchivo, Tool);
            resultado = Tool.CreateFolder(Valor.FolderFile); //path carpeta archivos 
            resultado = Tool.CreateFolder(Valor.PathFolderFileEmpresa()); // path carpeta archivos empresa
            resultado = Tool.CreateFolder(Valor.PathFolderImageQr()); // path carpeta qr empresa
            dgv.DataSource = Valor.GetDt();
            dgv.ClearSelection();
            if (dgv.Rows.Count == 0)
            {
                //MessageBox.Show("No existen datos para procesar", "INFORMACION DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //return true;
            }
            lbl.Text = "Numero de errores en el archivo : " + dgv.Rows.Count + Environment.NewLine + pathArchivo + Environment.NewLine +
                       "( Insertando/Actualizando ) datos en Db" + Environment.NewLine + "Espere un momento, esto puede tardar unos segundos";

            string pathFileXlsx = Valor.PathFileXlsx();//path del archivo excel 
            resultado = Funcion.CreateFileXlsx(persons, pathFileXlsx, Tool);
            if (resultado)
                UploadPersonToApi(lbl,"UPDATE");

            return resultado;
        }

        #endregion

        #region ADDCOMPANY
        public void Company(string nombre, string email, string rif, string tlf, string devices)
        {
            AddCompany(nombre, email, rif, tlf, devices);
        }

        public async Task AddCompany (string nombre, string email , string rif , string tlf, string devices)
        {
            bool resultado = false;
            string token = await Funcion.GetAccessTokenAsync(Tool, HttpFuncion);
            if (!string.IsNullOrEmpty(token))
            {
                Company company = new Company() { 
                    NameCompany = nombre,
                    Email = email,
                    Ref = rif,
                    Phone = tlf,
                    NumberDevices = Convert.ToInt32(devices)

                };

                string empresa= JsonConvert.SerializeObject(company);
                resultado = await HttpFuncion.CreateCompany(token, empresa);
                if (resultado)
                    MessageBox.Show("Transaccion exitosa", "INFORMACION DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Transaccion fallida", "INFORMACION DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region QUERYCOMPANY
        public async Task GetAllCompany(DataGridView dgv)
        {
            string token = await Funcion.GetAccessTokenAsync(Tool, HttpFuncion);
            List<Company> companys = await HttpFuncion.GetAllCompany(token);
            DataTable dt = Funcion.BuildDtComppany();
            dt = Funcion.SetDtCompany(companys, dt);
            dgv.DataSource = dt;
            dgv = ColorFila(dgv, Color.WhiteSmoke, Color.Gainsboro);
            dgv.ClearSelection();
        }

        public async Task <bool> UpdateCompany (Company company)
        {
            string token = await Funcion.GetAccessTokenAsync(Tool, HttpFuncion);
            string jsonCompany = JsonConvert.SerializeObject(company);
            bool resultado = await HttpFuncion.UpdateCompany(token,jsonCompany);
            if (resultado)
                MessageBox.Show("Transaccion exitosa", "INFORMACION DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Transaccion fallida", "INFORMACION DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return resultado;
        }
        #endregion


        public DataGridView ColorFila(DataGridView dgv , Color a, Color b)
        {
            int n = 0;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (n % 2 == 0)
                    row.DefaultCellStyle.BackColor = a;
                else
                    row.DefaultCellStyle.BackColor = b;
                n++;
            }
            return dgv;
        }

        /* 
     public async Task  UploadPersonToApi(Label lbl,string tipo)
      {
          bool resultado = false;
          string token = await Funcion.GetAccessTokenAsync(Tool, HttpFuncion);
          if (!string.IsNullOrEmpty(token))
          {
              List<Person> persons = Valor.GetPersons();
              string personas = JsonConvert.SerializeObject(persons);
              if (tipo == "INSERT")
                resultado = await HttpFuncion.UploadPersonToApi(token, personas);
              else
                resultado = await HttpFuncion.UploadPersonToApiUpdate(token, personas);
          }
          if (resultado)
            MessageBox.Show("Transaccion exitosa", "INFORMACION DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Information);
          else
              MessageBox.Show("Transaccion fallida", "INFORMACION DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Error);

          lbl.Text = string.Empty;
      }*/

    }
}
