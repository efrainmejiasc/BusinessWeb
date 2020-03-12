using BusinessDeskTop.Engine.Interfaces;
using BusinessDeskTop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace BusinessDeskTop.Engine
{
    public class EngineProject : IEngineProject
    {
        private EngineData Valor = EngineData.Instance();

        public async Task<string> GetAccessTokenAsync(IEngineTool Tool, IEngineHttp HttpFuncion)
        {
            string strValid = Tool.DataLoginUserApi();
            strValid = await HttpFuncion.GetAccessToken(strValid);
            Valor.AccesToken = strValid;
            return strValid;
        }
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
            xlApp.DisplayAlerts = false;
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;
            string strValue = string.Empty;
            int idx = 0;
            bool validate = false;
            for (int fila = 2; fila <= rowCount; fila++)
            {
                for (int columna = 1; columna <= colCount; columna++)
                {
                    if (xlRange.Cells[fila, columna] != null && xlRange.Cells[fila, columna].Value != null)
                    {
                        if (columna == 1)
                        {
                            validate = Tool.ExistsFile(@"C:\Users\HOME\Desktop\Carnetizacion\List\" + xlRange.Cells[fila, columna].Value.ToString().Replace("/","\\"));
                            if (!validate)
                            {
                                strValue = strValue + "NO_FOTO" + "#";
                            }
                            else
                            {
                                strValue = strValue + xlRange.Cells[fila, columna].Value.ToString() + "#";
                            }
                        }
                        else if (columna == 9)
                        {
                            validate = Tool.EmailEsValido(xlRange.Cells[fila, columna].Value.ToString().ToLower());
                            if (!validate)
                            {
                                strValue = strValue + "email@nodefinido.com" + "#";
                            }
                            else
                            {
                                strValue = strValue + xlRange.Cells[fila, columna].Value.ToString().ToLower() + "#";
                            }
                        }
                        else if (columna == 11)
                        {
                            if (xlRange.Cells[fila, columna].Value.ToString().ToUpper() == "MAÑANA")
                                strValue = strValue + "1" + "#";
                            else if (xlRange.Cells[fila, columna].Value.ToString().ToUpper() == "TARDE")
                                strValue = strValue + "2" + "#";
                            else if (xlRange.Cells[fila, columna].Value.ToString().ToUpper() == "NOCHE")
                                strValue = strValue + "3" + "#";
                            else
                                strValue = strValue + xlRange.Cells[fila, columna].Value.ToString() + "#";
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


                 /*   if (columna == colCount)
                    {
                        if (!strValue.Contains("NO_DEFINIDO") && !strValue.Contains("NO_FOTO") && !strValue.Contains("EMAIL_NO_VALIDO"))
                        {
                            p = SetListPerson(strValue, Tool);
                            if (!string.IsNullOrEmpty(p.Email))
                            {
                                lp.Insert(idx, p);
                                idx++;
                            }
                            else
                            {
                                dt = SetDataTable(strValue, dt, fila);
                            }
                        }
                        else
                        {
                            dt = SetDataTable(strValue, dt, fila);
                        }
                    }*/
                }
                strValue = string.Empty;
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            while (Marshal.ReleaseComObject(xlRange) != 0) ;
            while (Marshal.ReleaseComObject(xlWorksheet) != 0) ;
            xlWorkbook.Close();
            while (Marshal.ReleaseComObject(xlWorkbook) != 0) ;
            xlApp.Quit();
            while (Marshal.ReleaseComObject(xlApp) != 0) ;
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
            dt.Columns.Add("TURNO");
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
            p.Turno = x[10]; ;


            return p;
        }

        public DataTable SetDataTable(string strValue, DataTable dt , int idx)
        {
            string[] x = strValue.Split('#');
            dt.Rows.Add(idx, x[0], x[1], x[2], x[3], x[4], x[5], x[6], x[7], x[8], x[9], string.Empty,x[10]);
            return dt;
        }

        public bool CreateFileXlsx (List<Person> persons , string pathFile,IEngineTool Tool)
        {
            bool resultado = false;

            Excel.Application excel = default(Excel.Application);       
            Excel.Workbook libro = default(Excel.Workbook);
            Excel.Worksheet hoja = default(Excel.Worksheet);
            excel = new Excel.Application();
            excel.DisplayAlerts = false;
            try
            {
                libro = excel.Workbooks.Add();
                hoja = libro.Worksheets[1];
                hoja.Activate();

                hoja.Range["A1"].Value = "FOTO";
                hoja.Range["B1"].Value = "NOMBRE";
                hoja.Range["C1"].Value = "APELLIDO";
                hoja.Range["D1"].Value = "DNI";
                hoja.Range["E1"].Value = "MATRICULA";
                hoja.Range["F1"].Value = "RH";
                hoja.Range["G1"].Value = "GRADO";
                hoja.Range["H1"].Value = "GRUPO";
                hoja.Range["I1"].Value = "QR";

                int n = 2;
                string foto64 = string.Empty;
                string sourceQr = string.Empty;
                string qr64 = string.Empty;
                string pathFoto = string.Empty;
                string pathQr = string.Empty;
                foreach (Person p in persons)
                {
                    foto64 = Tool.ConvertImgTo64Img(p.Foto);
                    sourceQr = p.Nombre + p.Apellido + p.Dni;
                    sourceQr = Tool.ConvertirBase64(sourceQr);
                    p.Qr = Tool.CreateQrCode(sourceQr, Valor.PathFolderImageQr() + @"\" + p.Dni + ".png");
                    qr64 = Tool.ConvertImgTo64Img(Valor.PathFolderImageQr() + @"\" + p.Dni + ".png");

                    var f = p.Foto.Split('\\');
                    var q = p.Qr.Split('\\');
                    hoja.Range["A" + n].Value = "FOTO/" + f[f.Length - 1];
                    hoja.Range["B" + n].Value = p.Nombre;
                    hoja.Range["C" + n].Value = p.Apellido;
                    hoja.Range["D" + n].Value = p.Dni;
                    hoja.Range["E" + n].Value = p.Matricula;
                    hoja.Range["F" + n].Value = p.Rh;
                    hoja.Range["G" + n].Value = p.Grado;
                    hoja.Range["H" + n].Value = p.Grupo;
                    hoja.Range["I" + n].Value = "QR/" + q[q.Length - 1]; ;

                    p.Foto = foto64;
                    p.Qr = qr64;
                    n++;
                }
                excel.ActiveWindow.Zoom = 100;
                excel.Columns.AutoFit();
                excel.Rows.AutoFit();
                libro.SaveAs(pathFile);
                ReadWriteTxt(pathFile);
                excel.Quit();
                Valor.SetPersons(persons);
                resultado = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error:  " + ex.ToString());
            }
   
            return resultado;
        }

        public bool CreateFileXlsx(DataTable dt)
        {
            bool resultado = false;

            Excel.Application excel = default(Excel.Application);
            Excel.Workbook libro = default(Excel.Workbook);
            Excel.Worksheet hoja = default(Excel.Worksheet);
            excel = new Excel.Application();
            excel.DisplayAlerts = false;
            try
            {
                libro = excel.Workbooks.Add();
                hoja = libro.Worksheets[1];
                hoja.Activate();

                hoja.Range["A1"].Value = "FOTO";
                hoja.Range["B1"].Value = "NOMBRE";
                hoja.Range["C1"].Value = "APELLIDO";
                hoja.Range["D1"].Value = "DNI";
                hoja.Range["E1"].Value = "MATRICULA";
                hoja.Range["F1"].Value = "RH";
                hoja.Range["G1"].Value = "GRADO";
                hoja.Range["H1"].Value = "GRUPO";
                hoja.Range["I1"].Value = "EMAIL";
                hoja.Range["J1"].Value = "EMPRESA";
                hoja.Range["K1"].Value = "TURNO";

                int n = 2;
               
                foreach (DataRow p in dt.Rows)
                {
                    hoja.Range["A" + n].Value = p[1];
                    hoja.Range["B" + n].Value = p[2];
                    hoja.Range["C" + n].Value = p[3];
                    hoja.Range["D" + n].Value = p[4];
                    hoja.Range["E" + n].Value = p[5];
                    hoja.Range["F" + n].Value = p[6];
                    hoja.Range["G" + n].Value = p[7];
                    hoja.Range["H" + n].Value = p[8];
                    hoja.Range["I" + n].Value = p[9];
                    hoja.Range["J" + n].Value = p[10];
                    hoja.Range["k" + n].Value = p[12];
                    n++;
                }
                excel.ActiveWindow.Zoom = 100;
                excel.Columns.AutoFit();
                excel.Rows.AutoFit();
                excel.Visible = true;
                resultado = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:  " + ex.ToString());
            }

            return resultado;
        }


        public DataTable BuildDtComppany()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("NOMBRE EMPRESA");
            dt.Columns.Add("NIT");
            dt.Columns.Add("EMAIL");
            dt.Columns.Add("TELEFONO");
            dt.Columns.Add("ESTADO");
            dt.Columns.Add("NUMERO EQUIPOS");
            return dt;
        }

        public DataTable SetDtCompany (List<Company> companys , DataTable dt)
        { 
            foreach (Company c in companys)
            {
                dt.Rows.Add(c.Id, c.NameCompany, c.Ref, c.Email, c.Phone, c.Status, c.NumberDevices);
            }
            return dt;
        }

        public bool SetLXlsxOut (string pathArchivo)
        {
            bool result = false;
            ReadWriteTxt(pathArchivo);
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(pathArchivo);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            xlApp.DisplayAlerts = false;
            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            xlWorksheet.Columns["I"].Delete();
            xlWorksheet.Columns["J"].Delete();
            xlWorksheet.Columns["K"].Delete();
            xlWorksheet.Columns["M"].Delete();
            xlWorksheet.Columns["N"].Delete();

            xlWorkbook.SaveAs(pathArchivo);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            while (Marshal.ReleaseComObject(xlRange) != 0) ;
            while (Marshal.ReleaseComObject(xlWorksheet) != 0) ;
            xlWorkbook.Close();
            while (Marshal.ReleaseComObject(xlWorkbook) != 0) ;
            xlApp.Quit();
            while (Marshal.ReleaseComObject(xlApp) != 0) ;
         
            return result;
        }
    }
}
