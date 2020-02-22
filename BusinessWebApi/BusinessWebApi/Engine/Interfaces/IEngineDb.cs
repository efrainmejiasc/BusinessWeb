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
        int GetCompanyId(string nameCompany);
        bool CreatePerson(List<Person> persons);
        bool UpdatePerson(List<Person> persons);
        UserApi GetUser(string password, string password2);
    }
}
