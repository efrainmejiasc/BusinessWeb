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

        int n = 15;

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

            string file = string.Empty;
            string foto = string.Empty;
            string nombre = string.Empty;
            string apellido= string.Empty;
            string dni = string.Empty;
            string matricula = string.Empty;
            string rh = string.Empty;
            string grado = string.Empty;
            string grupo = string.Empty;
            string email = string.Empty;
            string empresa = string.Empty;
            string turno = string.Empty;
            string s = "#";

            for (int fila = 2; fila <= rowCount; fila++)
            {
                strValue = string.Empty;

                for (int columna = 1; columna <= colCount; columna++)
                {
                    try 
                    {
                        if (xlRange.Cells[fila, columna] != null) 
                        {
                            if (columna == 1)//foto
                            {
                                if (xlRange.Cells[fila, columna].Value != null)
                                {
                                    var g = (xlRange.Cells[fila, columna].Value.ToString()).Replace("/", "#").Trim();
                                    string[] part = g.Split('#');
                                    file = @"C:\Users\ASUS\Downloads\CARNETIZACION\Rosa\List\FOTO\" + part[1].Trim();
                                    validate = Tool.ExistsFile(file);
                                    if (validate)
                                    {
                                        foto = file;
                                    }
                                    else
                                    {
                                        file = @"C:\Users\ASUS\Downloads\CARNETIZACION\Rosa\List\FOTO\" + part[1].Trim();
                                        foto = file;
                                    }
                                }
                                else
                                {
                                    file = "NO_DEFINIDO";
                                    foto = file;
                                }
                                strValue = strValue + foto + s;
                                foto = string.Empty;
                                file = string.Empty;
                            }
                            else if (columna == 2)//nombre
                            {
                                if (xlRange.Cells[fila, columna].Value != null)
                                    nombre = xlRange.Cells[fila, columna].Value.ToString().ToUpper().Trim();
                                else
                                    nombre = xlRange.Cells[fila, columna].Value.ToString().ToUpper().Trim();

                                strValue = strValue + nombre + s;
                                nombre = string.Empty;
                            }
                            else if (columna == 3)//apellido
                            {
                                if (xlRange.Cells[fila, columna].Value != null)
                                    apellido = xlRange.Cells[fila, columna].Value.ToString().ToUpper().Trim();
                                else
                                    apellido = xlRange.Cells[fila, columna].Value.ToString().ToUpper().Trim();

                                strValue = strValue + apellido + s;
                                apellido = string.Empty;
                            }
                            else if (columna == 4)//dni documento
                            {
                                if (xlRange.Cells[fila, columna].Value != null)
                                    dni = xlRange.Cells[fila, columna].Value.ToString().ToUpper().Trim().Trim();
                                else
                                    dni = xlRange.Cells[fila, columna].Value.ToString().ToUpper().Trim();

                                strValue = strValue + dni + s;
                                dni = string.Empty;
                            }
                            else if (columna == 5)//matricula
                            {
                                if (xlRange.Cells[fila, columna].Value != null)
                                    matricula = xlRange.Cells[fila, columna].Value.ToString().ToUpper().Trim();
                                else
                                    matricula = xlRange.Cells[fila, columna].Value.ToString().ToUpper().Trim();

                                strValue = strValue + matricula + s;
                                matricula = string.Empty;
                            }
                            else if (columna == 6)//rh
                            {
                                if (xlRange.Cells[fila, columna].Value != null)
                                    rh = xlRange.Cells[fila, columna].Value.ToString().ToUpper().Trim();
                                else
                                    rh = xlRange.Cells[fila, columna].Value.ToString().ToUpper().Trim();

                                strValue = strValue + rh + s;
                                rh = string.Empty;
                            }
                            else if (columna == 7)//grado
                            {
                                if (xlRange.Cells[fila, columna].Value != null)
                                    grado = xlRange.Cells[fila, columna].Value.ToString().ToUpper().Trim();
                                else
                                    grado = xlRange.Cells[fila, columna].Value.ToString().ToUpper().Trim();

                                strValue = strValue + grado + s;
                                grado = string.Empty;
                            }
                            else if (columna == 8)//grupo
                            {
                                if (xlRange.Cells[fila, columna].Value != null)
                                    grupo = xlRange.Cells[fila, columna].Value.ToString().ToUpper().Trim();
                                else
                                    grupo = xlRange.Cells[fila, columna].Value.ToString().ToUpper().Trim();

                                strValue = strValue + grupo + s;
                                grupo = string.Empty;
                            }
                            else if (columna == 9)//email
                            {
                                if (xlRange.Cells[fila, columna].Value != null)
                                {
                                    validate = Tool.EmailEsValido(xlRange.Cells[fila, columna].Value.ToString().ToLower().Trim());
                                    if (validate)
                                        email = xlRange.Cells[fila, columna].Value.ToString().ToLower().Trim();
                                    else
                                        email = NoDefinido(xlRange.Cells[fila, columna].Value.ToString().ToLower().Trim());
                                }
                                else
                                {
                                    email = NoDefinido("nodefinido@gmail.com");
                                }
                                strValue = strValue + email + s;
                                email = string.Empty;
                            }
                            else if (columna == 10)//empresa
                            {
                                if (xlRange.Cells[fila, columna].Value != null)
                                    empresa = xlRange.Cells[fila, columna].Value.ToString().ToUpper().Trim();
                                else
                                    empresa = xlRange.Cells[fila, columna].Value.ToString().ToUpper().Trim();

                                strValue = strValue + empresa + s;
                                empresa = string.Empty;
                            }
                            else if (columna == 11)//turno
                            {
                                if (xlRange.Cells[fila, columna].Value != null)
                                {
                                    if (xlRange.Cells[fila, columna].Value.ToString().ToUpper().Trim() == "MAÑANA")
                                        turno = "1";
                                    else if (xlRange.Cells[fila, columna].Value.ToString().ToUpper().Trim() == "TARDE")
                                        turno = "2";
                                    else if (xlRange.Cells[fila, columna].Value.ToString().ToUpper().Trim() == "NOCHE")
                                        turno = "3";
                                    else
                                        turno = "1";
                                }
                                else
                                {
                                    turno = "1";
                                }

                                strValue = strValue + turno + s;
                                turno = string.Empty;
                            }
                        }
                      

                     
                        //*************************************************************************************************************************************************************************
                        if (columna == colCount)
                        {
                            if (!strValue.Contains("NO_DEFINIDO") && !strValue.Contains("NO_FOTO") && !strValue.Contains("EMAIL_NO_VALIDO") && !string.IsNullOrEmpty(strValue) && strValue.Trim() != "#")
                            {
                                p = SetListPerson(strValue, Tool);

                                if (!string.IsNullOrEmpty(p.Email))
                                {
                                    lp.Insert(idx, p);
                                    idx++;
                                }
                                else
                                {
                                    p.Email = NoDefinido("nodefinido@gmail.com");
                                    lp.Insert(idx, p);
                                    idx++;
                                }
                            }
                            else
                            {
                                dt = SetDataTable(strValue, dt, fila);
                            }
                        }
                        // foto = string.Empty; nombre = string.Empty; apellido = string.Empty; dni = string.Empty; matricula = string.Empty; grado = string.Empty; email = string.Empty; empresa = string.Empty; turno = string.Empty;
                    }
                    catch (Exception ex)
                    {
                        string err = ex.ToString();
                    }
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


        public string NoDefinido (string e)
        {
            if (e == "nodefinido@gmail.com")
            {
                string[] p = e.Split('@');
                e = p[0] + n.ToString() + "@" + "gmail.com";
                n++;
            }
            return e;
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
            if (!string.IsNullOrEmpty(strValue) && strValue.Trim() != "#")
            {
                string[] x = strValue.Split('#');
                dt.Rows.Add(idx, x[0], x[1], x[2], x[3], x[4], x[5], x[6], x[7], x[8], x[9], string.Empty, x[10]);
            }
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
