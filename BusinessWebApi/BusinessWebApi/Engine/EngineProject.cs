using BusinessWebApi.Engine.Interfaces;
using BusinessWebApi.Models;
using BusinessWebApi.Models.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                Dni = device.Dni
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
    }
}
