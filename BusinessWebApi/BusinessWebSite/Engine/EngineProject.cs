using BusinessWebSite.Engine.Interfaces;
using BusinessWebSite.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        public bool BuildXlsxAsistenciaClase(List<HistoriaAsistenciaPerson> asis)
        {
            string nombreProfesor = Metodo.NameDevice(asis[0].DniAdm);
            Excel.Application application = new Excel.Application();
            Excel.Workbook workbook = application.Workbooks.Add(System.Reflection.Missing.Value);
            Excel.Worksheet worksheet = workbook.ActiveSheet;
            worksheet.Cells[1, 1] = "Institucion";
            worksheet.Cells[1, 2] = Metodo.GetCompanyName(asis[0].IdCompany);
            worksheet.get_Range("A1", "B1").Interior.Color = Color.Black;
            worksheet.get_Range("A1", "B1").Font.Color = Color.White;
            worksheet.Cells[2, 1] = "Fecha";
            worksheet.Cells[2, 2] = "Materia";
            worksheet.Cells[2, 3] = "Grado";
            worksheet.Cells[2, 4] = "Grupo";
            worksheet.Cells[2, 5] = "DI - Profesor";
            worksheet.get_Range("A2", "E2").Interior.Color = Color.Black;
            worksheet.get_Range("A2", "E2").Font.Color = Color.White;
            worksheet.Cells[3, 1] = DateTime.Now.Date.ToString("dd/MM/yyyy");
            worksheet.Cells[3, 2] = asis[0].Materia;
            worksheet.Cells[3, 3] = asis[0].Grado;
            worksheet.Cells[3, 4] = asis[0].Grupo;
            worksheet.Cells[3, 5] = asis[0].DniAdm + " - " + nombreProfesor;

            worksheet.Cells[5, 1] = "Nª";
            worksheet.Cells[5, 2] = "Nombre";
            worksheet.Cells[5, 3] = "Apellido";
            worksheet.Cells[5, 4] = "Documento de Identidad";
            worksheet.Cells[5, 5] = "Fecha";
            worksheet.Cells[5, 6] = "Estado";
            worksheet.get_Range("A5", "F5").Interior.Color = Color.Black;
            worksheet.get_Range("A5", "F5").Font.Color = Color.White;

            int row = 6;
            int index = 1;
            foreach (var I in asis)
            {
                //FORMAT CONTENT
                worksheet.Range["A" + row.ToString(), "F" + row.ToString()].Font.Color = System.Drawing.Color.Black;
                worksheet.Range["A" + row.ToString(), "F" + row.ToString()].Font.Size = 10;

                var P = Metodo.GetPerson(I.Dni);

                worksheet.Cells[row, 1] = index.ToString();
                worksheet.Cells[row, 2] = P.Nombre;
                worksheet.Cells[row, 3] = P.Apellido;
                worksheet.Cells[row, 4] = I.Dni;
                worksheet.Cells[row, 5] = DateTime.Now.Date.ToString("dd/MM/yyyy");
                worksheet.Cells[row, 6] = Estado(I.Status);
                index++;
                row++;
            }
            application.Columns.AutoFit();
            application.Rows.AutoFit();
            application.Columns.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            application.Rows.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            application.DisplayAlerts = false;
            string nombreArchivo = NombreArchivo(asis, nombreProfesor);
            string path = HttpContext.Current.Server.MapPath("~/App_Data/" + nombreArchivo);
            workbook.SaveAs(path);
            workbook.Close();
            application.Quit();
            string emailTo = Metodo.EmailCompany(asis[0].IdCompany);
            EngineNotify notify = new EngineNotify();
            notify.EnviarEmail(emailTo, nombreArchivo, path, false);
            return true;
        }
    }
}