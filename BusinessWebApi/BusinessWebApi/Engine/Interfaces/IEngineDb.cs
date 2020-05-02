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
        List<Grupo> GetGrupos(int idCompany);
        List<Grado> GetGrados(int idCompany);
        bool ExistsCodeCompany(string codigo);
        Person GetPerson2(string identificador);
        int NumberDeviceRegister(int idCompany);
        Company GetCompanyCodigo(string codigo);
        bool CreatePerson(List<Person> persons);
        bool UpdatePerson(List<Person> persons);
        List<Materias> GetMaterias(int idCompany);
        bool RegisterDevice(DevicesCompany device); 
        List<AsistenciaClase> StudentsNonAttending();
        bool UpdateAsistenciaClase(int id, bool status);
        UserApi GetUser(string password, string password2);
        List<Person> GetPerson(List<AsistenciaClase> asis);
        List<Models.Objetos.Turno> GetTurnos(int idCompany);
        bool NewObservacionClase(ObservacionClase observacion);
        bool UpdateAsistenciaClase(List<AsistenciaClase> asis);
        object GetDniUserApi(int id, int idCompany, string email);
        bool NewAsistenciaClase(List<AsistenciaClase> asistencias);
        UserApi GetUserSuspended(string password, string password2);
        bool NewObservacionClasePagina(ObservacionClase observacion);
        List<RegisterDevice> GetListDevicesRegistered(string codigo);
        bool NewAsistenciaComedor(List<AsistenciaComedor> asistencias);
        bool UpdateUserApi(string userName, string email, string password);
        List<HistoriaAsistenciaPerson> GetHistoriaAsistenciaPerson(string dni);
        List<HistoriaAsistenciaPerson> GetHistoriaAsistenciaPersonaXlsx(string dni);
        bool UpdateUserApi(int idCompany, string nameCompany, string user, string email);
        List<Person> GetPersonList(int idCompany, string grado, string grupo, int idTurno);
        List<Asistencia> GetDetalleHistoriaAsistenciaPerson(string dni, string materia, string dniAdm);
        List<Asistencia> GetAsistenciaClase(string fecha, string grado, string grupo,int turno, int idCompany);
        AsistenciaClase GetAsistenciaClase(string fecha, string dni, string materia, string grado, string grupo, int idCompany);
    }
}
