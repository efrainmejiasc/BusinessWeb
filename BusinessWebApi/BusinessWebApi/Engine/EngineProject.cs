using BusinessWebApi.Engine.Interfaces;
using BusinessWebApi.Models;
using BusinessWebApi.Models.Objetos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Excel = Microsoft.Office.Interop.Excel;

namespace BusinessWebApi.Engine
{
    public class EngineProject: IEngineProject
    {
        public DevicesCompany BuilDeviceCompany(List<RegisterDevice> listDevice, RegisterDevice device,IEngineDb Metodo)
        {
            string[] arrayUser = { device.Email, device.User };
            UserApi userApi = Metodo.GetUser(arrayUser);
            if (userApi != null)
            {
                DevicesCompany devicesCompany = new DevicesCompany()
                {
                    IdCompany = listDevice[0].IdCompany,
                    IdUserApi = userApi.Id,
                    IdTypeUser = 2,
                    CreateDate = DateTime.UtcNow,
                    Phone = device.Phone,
                    Dni = device.Dni
                };
                return devicesCompany;
            }

            return null;
        }

        public DevicesCompany BuilDeviceCompany(Company company, RegisterDevice device,IEngineDb Metodo)
        {
            string[] arrayUser = { device.Email, device.User};
            UserApi userApi = Metodo.GetUser(arrayUser);
            DevicesCompany devicesCompany = new DevicesCompany()
            {
                IdCompany = company.Id,
                IdUserApi = userApi.Id,
                IdTypeUser = 2,
                CreateDate = DateTime.UtcNow,
                Phone = device.Phone,
                Dni = device.Dni,
                Nombre = device.Nombre
            };
            return devicesCompany;
        }

        public List<DataEmailNoAsistencia> BuildDataEmailNoAsistencia(List<Person> personas)
        {
            List<DataEmailNoAsistencia> list = new List<DataEmailNoAsistencia>();
            DataEmailNoAsistencia s = new DataEmailNoAsistencia();
            foreach(Person i in personas)
            {
                s = new DataEmailNoAsistencia()
                {
                    Nombre = i.Nombre,
                    Apellido = i.Apellido,
                    Dni = i.Dni,
                    Email = i.Email,
                    Fecha = DateTime.UtcNow.Date
                };
                list.Add(s);
            }
            return list; 
        }

        public SucesoLog ConstruirSucesoLog(string cadena)
        {
            string[] x = cadena.Split('*');
            SucesoLog modelo = new SucesoLog()
            {
                Fecha = DateTime.UtcNow,
                Excepcion = x[0],
                Metodo = x[1],
                Email = x[2]
            };
            return modelo;
        }

        public bool BuildXlsxAsistenciaClase(List<AsistenciaClase> asis, IEngineDb Metodo)
        {
            bool resultado = false;
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
            worksheet.Cells[2, 5] = "Profesor";
            worksheet.get_Range("A2", "E2").Interior.Color = Color.Black;
            worksheet.get_Range("A2", "E2").Font.Color = Color.White;
            worksheet.Cells[3, 1] = DateTime.Now.Date.ToString("dd/MM/yyyy");
            worksheet.Cells[3, 2] = asis[0].Materia;
            worksheet.Cells[3, 3] = asis[0].Grado;
            worksheet.Cells[3, 4] = asis[0].Grupo;
            worksheet.Cells[3, 5] = asis[0].DniAdm + " " + Metodo.NameDevice(asis[0].DniAdm);

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
            string nombreArchivo = NombreArchivo(asis);
            string path = HttpContext.Current.Server.MapPath("~/App_Data/" + nombreArchivo);
            workbook.SaveAs(path);
            workbook.Close();
            application.Quit();
            string emailTo = Metodo.EmailCompany(asis[0].IdCompany);
            EngineNotify notify = new EngineNotify();
            notify.EnviarEmail(emailTo, nombreArchivo, path, false);
            return resultado;
        }

        private string Estado(bool s)
        {
            if (s) return "Asistente";
            else return "Inasistente";
        }

        private string NombreArchivo(List<AsistenciaClase> asis)
        {
            string fecha = DateTime.Now.Date.ToString("dd/MM/yyyy").Replace("/", "");
            string nombre = asis[0].Materia + "_" + asis[0].DniAdm + "_" + asis[0].Grado + "_" + asis[0].Grupo + "_" + fecha + ".xlsx";
            return nombre;
        }
    }
}
