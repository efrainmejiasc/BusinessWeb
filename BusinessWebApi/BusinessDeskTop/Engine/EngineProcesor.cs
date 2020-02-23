using BusinessDeskTop.Engine.Interfaces;
using BusinessDeskTop.Modelo;
using System;
using System.Collections.Generic;
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

        public  bool ProcesarArchivo  (string pathArchivo,DataGridView dgv)
        {
            bool resultado = false;
            List<Person> persons = Funcion.LeerArchivo(pathArchivo,Tool);
            resultado = Tool.CreateFolder(@"C:\QR_ARCHIVOS");
            resultado = Tool.CreateFolder(@"C:\QR_ARCHIVOS\" + Valor.NombreEmpresa);
            resultado = Tool.CreateFolder(@"C:\QR_ARCHIVOS\" + Valor.NombreEmpresa + "\\" + Valor.NombreEmpresa + "_QR");
            dgv.DataSource = Valor.GetDt();
            dgv.ClearSelection();
            if (persons.Count == 0)
            {
                MessageBox.Show("No existen datos para procesar", "INFORMACION DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            string path = @"C:\QR_ARCHIVOS\" + Valor.NombreEmpresa + "\\" + Valor.NombreEmpresa + DateTime.Now.ToString().Replace("\\", "").Replace(' ', '_').Replace(":", "") + ".xlsx";
            path = path.Replace("/", "");
            resultado = Funcion.CreateFileXlsx(persons, path,Tool); 
            if (resultado)
                MessageBox.Show("Transaccion exitosa", "INFORMACION DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return resultado;
        } 
    }
}
