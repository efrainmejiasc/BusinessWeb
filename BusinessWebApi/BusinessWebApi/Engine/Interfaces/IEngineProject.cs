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
        SucesoLog ConstruirSucesoLog(string cadena);
        List<DataEmailNoAsistencia> BuildDataEmailNoAsistencia(List<Person> personas);
        DevicesCompany BuilDeviceCompany(Company company, RegisterDevice device, IEngineDb Metodo);
        DevicesCompany BuilDeviceCompany(List<RegisterDevice> listDevice, RegisterDevice device, IEngineDb Metodo);
    }
}
