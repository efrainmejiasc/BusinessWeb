using BusinessDeskTop.Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;
using System.IO;
using BusinessDeskTop.Modelo;

namespace BusinessDeskTop.Engine
{
    public class EngineProject : IEngineProject
    {
        private EngineData Valor = EngineData.Instance();
        public List<Person> LeerArchivo(string pathArchivo , IEngineTool Tool)
        {
            DataTable dt = new DataTable();
            dt = BuildColumnDataTable(dt);
            Person p = new Person();
            List<Person> lp = new List<Person>();
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(pathArchivo);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            string strValue = string.Empty;
            int idx = 0;
            bool existeFoto = false;
            for (int fila  = 2; fila <= rowCount; fila++)
            {
                for (int columna = 1; columna <= colCount - 1 ; columna++)
                {
                    if (xlRange.Cells[fila, columna] != null && xlRange.Cells[fila, columna].Value != null)
                    {
                        if (columna == 1)
                        {
                            existeFoto = Tool.ExistsFile(xlRange.Cells[fila, columna].Value.ToString());
                            if (!existeFoto)
                            {
                               strValue = strValue + "NO_FOTO" + "#";
                            }
                            else
                            {
                                strValue = strValue + xlRange.Cells[fila, columna].Value.ToString() + "#";
                            }
                        }
                        else
                        {
                            strValue = strValue + xlRange.Cells[fila, columna].Value.ToString() + "#";
                        }   
                    }
                    else
                    {
                        strValue = strValue + "NO_DEFINIDO" + "#";
                    }
                    if (columna == colCount - 1)
                    {
                        if (!strValue.Contains("NO_DEFINIDO") && !strValue.Contains("NO_FOTO"))
                        {
                            p = SetListPerson(strValue,Tool);
                            if (!string.IsNullOrEmpty(p.Email))
                            {
                                lp.Insert(idx, p);
                                idx++;
                            }
                            else
                            {
                                dt = SetDataTable(strValue, dt, fila - 1);
                            }
                        }
                        else
                        {
                            dt = SetDataTable(strValue, dt, fila - 1);
                        }
                    }
                }
                strValue = string.Empty;
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
            ReadWriteTxt(pathArchivo);
            Valor.SetDt(dt);
            return lp;
        }

        public DataTable BuildColumnDataTable(DataTable dt)
        {
            dt.Columns.Add("LINEA ERROR");
            dt.Columns.Add("FOTOS");
            dt.Columns.Add("NOMBRE");
            dt.Columns.Add("APELLIDO");
            dt.Columns.Add("DNI");
            dt.Columns.Add("MATRICULA");
            dt.Columns.Add("RH");
            dt.Columns.Add("GRADO");
            dt.Columns.Add("GRUPO");
            dt.Columns.Add("EMAIL");
            dt.Columns.Add("EMPRESA");
            dt.Columns.Add("QR");
            return dt;
        }

        public void ReadWriteTxt(string pathArchivo)
        {
            FileAttributes atributosAnteriores = File.GetAttributes(pathArchivo);
            File.SetAttributes(pathArchivo, atributosAnteriores & ~FileAttributes.ReadOnly);
        }

        public Person SetListPerson(string strValue , IEngineTool Tool)
        {
            string[] x = strValue.Split('#');
            Person p = new Person();
            p.Foto = x[0];
            p.Nombre = x[1];
            p.Apellido = x[2];
            p.Dni = x[3];
            p.Matricula = x[4];
            p.Rh = x[5];
            p.Grado = x[6];
            p.Grupo = x[7];

            if (Tool.EmailEsValido(x[8]))
            p.Email = x[8];
            else
            p.Email = string.Empty;

            p.Company = x[9];
            p.Qr = string.Empty;


            return p;
        }

        public DataTable SetDataTable(string strValue, DataTable dt , int idx)
        {
            string[] x = strValue.Split('#');
            dt.Rows.Add(idx, x[0], x[1], x[2], x[3], x[4], x[5], x[6], x[7], x[8], x[9], string.Empty);
            return dt;
        }

        public bool CreateFileXlsx (List<Person> p , string pathFile)
        {

        }

        
    }
}
