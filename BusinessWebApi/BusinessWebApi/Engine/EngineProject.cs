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
            string[] arrayUser = { listDevice[0].Email, listDevice[0].User };
            UserApi userApi = Metodo.GetUser(arrayUser);
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
    }
}
