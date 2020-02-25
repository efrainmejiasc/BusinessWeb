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
using System.Windows.Forms;
using System.Drawing;

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
            xlApp.DisplayAlerts = false;
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
                hoja.Range["I1"].Value = "EMAIL";
                hoja.Range["J1"].Value = "EMPRESA";
                hoja.Range["K1"].Value = "QR";
                hoja.Range["L1"].Value = "FOTO64";
                hoja.Range["M1"].Value = "QR64";

                int n = 2;
                string foto64 = string.Empty;
                string sourceQr = string.Empty;
                string qr64 = string.Empty;
                byte[] byteFoto;
                byte[] byteQr;
                foreach (Person p in persons)
                {
                    using (Image imagen = Image.FromFile(p.Foto))
                    {
                        using (MemoryStream m = new MemoryStream())
                        {
                            imagen.Save(m, imagen.RawFormat);
                            byteFoto = m.ToArray();
                            foto64 = Convert.ToBase64String(byteFoto);
                        }
                    }
                    //foto64 = Tool.ConvertImgTo64Img(p.Foto);
                    sourceQr = p.Nombre + p.Apellido + p.Dni;
                    sourceQr = Tool.ConvertirBase64(sourceQr);
                    p.Qr = Tool.CreateQrCode(sourceQr, Valor.PathFolderImageQr() + @"\" + p.Dni + ".png");

                    using (Image imagen = Image.FromFile(Valor.PathFolderImageQr() + @"\" + p.Dni + ".png"))
                    {
                        using (MemoryStream m = new MemoryStream())
                        {
                            imagen.Save(m, imagen.RawFormat);
                            byteQr = m.ToArray();
                            qr64 = Convert.ToBase64String(byteQr);
                        }
                    }
                    // qr64 = Tool.ConvertImgTo64Img(Valor.PathFolderImageQr() + @"\" + p.Dni + ".png");

                    hoja.Range["A" + n].Value = p.Foto;
                    hoja.Range["B" + n].Value = p.Nombre;
                    hoja.Range["C" + n].Value = p.Apellido;
                    hoja.Range["D" + n].Value = p.Dni;
                    hoja.Range["E" + n].Value = p.Matricula;
                    hoja.Range["F" + n].Value = p.Rh;
                    hoja.Range["G" + n].Value = p.Grado;
                    hoja.Range["H" + n].Value = p.Grupo;
                    hoja.Range["I" + n].Value = p.Email;
                    hoja.Range["J" + n].Value = p.Company;
                    hoja.Range["K" + n].Value = p.Qr;
                    hoja.Range["L" + n].Value = foto64;
                    hoja.Range["M" + n].Value = qr64;

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


        public async Task<string> GetAccessTokenAsync(IEngineTool Tool, IEngineHttp HttpFuncion)
        {
            string strValid = Tool.DataLoginUserApi();
            strValid = await HttpFuncion.GetAccessToken(strValid);
            return strValid;
        }

        
    }
}
