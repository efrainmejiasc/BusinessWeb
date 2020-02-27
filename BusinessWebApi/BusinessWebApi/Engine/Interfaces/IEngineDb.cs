using BusinessWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessWebApi.Engine.Interfaces
{
    public interface IEngineDb
    { 
        bool CreateUser(UserApi user);
        bool UpdatePerson(Person person);
        bool CreatePerson(Person person);
        bool CreateCompany(Company company);
        int GetCompanyId(string nameCompany);
        bool CreatePerson(List<Person> persons);
        bool UpdatePerson(List<Person> persons);
        UserApi GetUser(string password, string password2);
        bool NewAsistenciaClase(List<AsistenciaClase> asistencias);
        UserApi GetUserSuspended(string password, string password2);
        bool NewAsistenciaComedor(List<AsistenciaComedor> asistencias);
    }
}
