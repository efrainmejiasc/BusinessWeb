using BusinessWebApi.Models;
using BusinessWebApi.Models.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessWebApi.Engine.Interfaces
{
    public interface IEngineProject
    {
        DevicesCompany BuilDeviceCompany(List<RegisterDevice> listDevice, RegisterDevice device, IEngineDb Metodo);
    }
}
