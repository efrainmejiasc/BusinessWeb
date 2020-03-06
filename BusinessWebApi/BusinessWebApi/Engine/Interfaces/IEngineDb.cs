using BusinessWebApi.Models;
using BusinessWebApi.Models.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessWebApi.Engine.Interfaces
{
    public interface IEngineDb
    {
        Person GetPerson(string dni);
        bool CreateUser(UserApi user);
        int NumberDevice(string codigo);
        bool UpdatePerson(Person person);
        bool CreatePerson(Person person);
        UserApi GetUser(string[] userApi);
        bool CreateCompany(Company company);
        int GetCompanyId(string nameCompany);
        bool ExistsCodeCompany(string codigo);
        int NumberDeviceRegister(int idCompany);
        Company GetCompanyCodigo(string codigo);
        bool CreatePerson(List<Person> persons);
        bool UpdatePerson(List<Person> persons);
        bool RegisterDevice(DevicesCompany device);
        List<AsistenciaClase> StudentsNonAttending();
        bool UpdateAsistencia(List<AsistenciaClase> asis);
        List<Person> GetPerson(List<AsistenciaClase> asis);
        UserApi GetUser(string password, string password2);
        object GetDniUserApi(int id, int idCompany, string email);
        bool NewAsistenciaClase(List<AsistenciaClase> asistencias);
        UserApi GetUserSuspended(string password, string password2);
        List<RegisterDevice> GetListDevicesRegistered(string codigo);
        bool NewAsistenciaComedor(List<AsistenciaComedor> asistencias);
        bool UpdateUserApi(int idCompany, string nameCompany, string user, string email);
        List<Person> GetPersonList(int idCompany, string grado, string grupo, int idTurno);
    }
}
