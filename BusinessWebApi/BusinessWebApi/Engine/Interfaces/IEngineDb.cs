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
        List<Grupo> GetGrupos();
        List<Grado> GetGrados();
        Person GetPerson(string dni);
        bool CreateUser(UserApi user);
        List<Company> GetAllCompany();
        string NameDevice(string dni);
        int NumberDevice(string codigo);
        bool UpdatePerson(Person person);
        bool CreatePerson(Person person);
        UserApi GetUser(string[] userApi);
        string EmailCompany(int idCompany);
        UserApi GetUserApi(string strValue);
        bool ExistsUserApi(string strValue);
        bool UpdateCompany(Company company);
        bool CreateCompany(Company company);
        string GetCompanyName(int idCompany);
        int GetCompanyId(string nameCompany);
        bool ExistsCodeCompany(string codigo);
        List<Models.Objetos.Turno> GetTurnos();
        Person GetPerson2(string identificador);
        int NumberDeviceRegister(int idCompany);
        Company GetCompanyCodigo(string codigo);
        bool CreatePerson(List<Person> persons);
        bool UpdatePerson(List<Person> persons);
        bool RegisterDevice(DevicesCompany device);
        List<AsistenciaClase> StudentsNonAttending();
        bool UpdateAsistenciaClase(int id, bool status);
        UserApi GetUser(string password, string password2);
        List<Person> GetPerson(List<AsistenciaClase> asis);
        bool NewObservacionClase(ObservacionClase observacion);
        bool UpdateAsistenciaClase(List<AsistenciaClase> asis);
        object GetDniUserApi(int id, int idCompany, string email);
        bool NewAsistenciaClase(List<AsistenciaClase> asistencias);
        UserApi GetUserSuspended(string password, string password2);
        List<RegisterDevice> GetListDevicesRegistered(string codigo);
        bool NewAsistenciaComedor(List<AsistenciaComedor> asistencias);
        bool UpdateUserApi(string userName, string email, string password);
        List<HistoriaAsistenciaPerson> GetHistoriaAsistenciaPerson(string dni);
        bool UpdateUserApi(int idCompany, string nameCompany, string user, string email);
        List<Person> GetPersonList(int idCompany, string grado, string grupo, int idTurno);
        List<Asistencia> GetAsistenciaClase(string fecha, string grado, string grupo,int turno, int idCompany);
    }
}
