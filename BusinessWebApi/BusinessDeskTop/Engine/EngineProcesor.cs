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
            resultado = Tool.CreateFolder(Valor.FolderFile); //path carpeta archivos 
            resultado = Tool.CreateFolder(Valor.PathFolderFileEmpresa()); // path carpeta archivos empresa
            resultado = Tool.CreateFolder(Valor.PathFolderImageQr()); // path carpeta qr empresa
            dgv.DataSource = Valor.GetDt();
            dgv.ClearSelection();
            if (persons.Count == 0)
            {
                MessageBox.Show("No existen datos para procesar", "INFORMACION DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            string pathFileXlsx = Valor.PathFileXlsx();//path del archivo excel 
            resultado = Funcion.CreateFileXlsx(persons, pathFileXlsx, Tool); 
            if (resultado)
            {
                //persons.Clear();
                persons = Valor.GetPersons();
                MessageBox.Show("Transaccion exitosa", "INFORMACION DEL SISTEMA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

                

            return resultado;
        } 
    }
}
