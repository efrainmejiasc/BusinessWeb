using BusinessWebSite.Engine.Interfaces;
using BusinessWebSite.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using Excel = Microsoft.Office.Interop.Excel;

namespace BusinessWebSite.Engine
{
    public class EngineProject:IEngineProject
    {

        public string BuildUserApiStr(string user, string password,IEngineTool Tool)
        {
            UserApi modelo = new UserApi();
            bool result = Tool.EmailEsValido(user);
            if (result)
            {
                modelo.Email = user;
                modelo.User = "A";
            }
            else
            {
                modelo.User = user;
                modelo.Email = "A";
            }
            modelo.Password = password;
            return JsonConvert.SerializeObject(modelo);
        }

        public string BuildCreateUserApiStr(string user, string email, string password)
        {
            UserApi modelo = new UserApi()
            {
                User = user,
                Email = email,
                Password = password
            };
            return JsonConvert.SerializeObject(modelo);
        }

        public string BuildRegisterDeviceStr(string user, string email, string codigo , string phone,string dni,string nombre)
        {
            RegisterDevice modelo = new RegisterDevice()
            {
                User = user,
                Email = email,
                Codigo = codigo,
                Phone = phone,
                Dni = dni,
                Nombre = nombre
            };
            return JsonConvert.SerializeObject(modelo);
        }

        public string BuidObservacionAsistencia(int idAsistencia, string dni, bool status, string materia,string observacion ,string dniAdm , int idCompany)
        {
            ObservacionClase modelo = new ObservacionClase()
            {
                IdAsistencia = idAsistencia,
                Dni  = dni,
                Status = status,
                Materia = materia,
                Observacion = observacion,
                CreateDate = DateTime.Now.Date,
                DniAdm  = dniAdm,
                IdCompany = idCompany

            };
            return JsonConvert.SerializeObject(modelo);
        }

        public bool BuildXlsxAsistenciaClase(List<HistoriaAsistenciaPerson> asis,string nombre, string apellido, string dni)
        {
            Excel.Application application = new Excel.Application();
            Excel.Workbook workbook = application.Workbooks.Add(System.Reflection.Missing.Value);
            Excel.Worksheet worksheet = workbook.ActiveSheet;
            worksheet.Cells[1, 1] = "Nombre";
            worksheet.Cells[1, 2] = "Apellido";
            worksheet.Cells[1, 3] = "Documento Identidad";
            worksheet.Cells[1, 4] = "Fecha Expedicion";
            worksheet.get_Range("A1", "D1").Interior.Color = Color.Black;
            worksheet.get_Range("A1", "D1").Font.Color = Color.White;
            worksheet.Cells[2, 1] = nombre;
            worksheet.Cells[2, 2] = apellido;
            worksheet.Cells[2, 3] = dni;
            worksheet.Cells[2, 4] = DateTime.Now.Date.ToString("dd/MM/yyyy");
            worksheet.Cells[4, 1] ="Historico de Asistencia";
            worksheet.get_Range("A1", "A1").Interior.Color = Color.Black;
            worksheet.get_Range("A1", "A1").Font.Color = Color.White;
            worksheet.Cells[6, 1] = "Nº";
            worksheet.Cells[6, 2] = "Materia";
            worksheet.Cells[6, 3] = "Numero de Inasistencias";
            worksheet.get_Range("A1", "C1").Interior.Color = Color.Black;
            worksheet.get_Range("A1", "C1").Font.Color = Color.White;

            int row = 7;
            int index = 1;
            int totalInasistencias = 0;
            foreach (var I in asis)
            {
                worksheet.Range["A" + row.ToString(), "C" + row.ToString()].Font.Color = System.Drawing.Color.Black;
                worksheet.Range["A" + row.ToString(), "C" + row.ToString()].Font.Size = 10;

                worksheet.Cells[row, 1] = index.ToString();
                worksheet.Cells[row, 2] = I.Materia;
                worksheet.Cells[row, 3] = I.NumeroInasistencia;
                totalInasistencias = totalInasistencias + Convert.ToInt32(I.NumeroInasistencia);
                index++;
                row++;
            }
            worksheet.Cells[row, 2] = "Total Inasistencias";
            worksheet.Cells[row, 3] = totalInasistencias.ToString();
            worksheet.get_Range("B1", "B1").Interior.Color = Color.Black;
            worksheet.get_Range("B1", "B1").Font.Color = Color.White;

            application.Columns.AutoFit();0
            application.Rows.AutoFit();
            application.Columns.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            application.Rows.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            application.DisplayAlerts = false;
            string nombreArchivo = NombreArchivo(dni, nombre + apellido);
            string path = HttpContext.Current.Server.MapPath("~/App_Data/" + nombreArchivo);
            workbook.SaveAs(path);
            workbook.Close();
            application.Quit();
            return true;
        }

        private string NombreArchivo(string dni, string name)
        {
            string fecha = DateTime.Now.Date.ToString("dd/MM/yyyy").Replace("/", "");
            string nombre = "HistoriaAsistencia" + "_" + name + "_" + dni + "_"  + fecha + ".xlsx";
            return nombre;
        }
    }
}
}