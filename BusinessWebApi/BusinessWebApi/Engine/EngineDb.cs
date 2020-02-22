using BusinessWebApi.Engine.Interfaces;
using BusinessWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessWebApi.Engine
{
    public class EngineDb: IEngineDb
    {
        private readonly IEngineProject Funcion;
        private readonly IEngineTool Tool;

        public EngineDb(IEngineProject _funcion,IEngineTool _tool)
        {
            Funcion = _funcion;
            Tool = _tool;
        }

        public bool CreateUser (UserApi user)
        {
            bool resultado = false;
            user.CreateDate = DateTime.UtcNow;
            user.Password = Tool.ConvertirBase64(user.Email + user.Password);
            user.Password2 = Tool.ConvertirBase64(user.User + user.Password);
            user.IdCompany =  GetCompanyId(user.Company);
            try
            {
                using (EngineContext context = new EngineContext())
                {
                    context.UserApi.Add(user);
                    context.SaveChanges();
                }
                resultado = true;
            }
            catch { }
            return resultado;
        }

        public UserApi GetUser(string password , string password2)
        {
            UserApi user = null;
            try
            {
                using (EngineContext context = new EngineContext())
                {
                    user = context.UserApi.Where(s => (s.Password == password || s.Password2 == password2) && s.Status == true).FirstOrDefault();
                    if (user != null)
                        return user;
                }
            }
            catch { }
            return null;
        }

        public int GetCompanyId(string nameCompany)
        {
            Company company = null;
            try
            {
                using (EngineContext context = new EngineContext())
                {
                    company = context.Company.Where(s => s.NameCompany == nameCompany).FirstOrDefault();
                        return company.Id;
                }
            }
            catch { }
            return 0;
        }

        public bool CreatePerson (List<Person> persons)
        {
            bool resultado = false;
            try
            {
                using (EngineContext context = new EngineContext())
                {
                    foreach(Person person in persons)
                    {
                        context.Person.Add(person);
                        context.SaveChanges();
                    }
                }
                resultado = true;
            }
            catch { }
            return resultado;
        }

        public bool UpdatePerson(List<Person> persons)
        {
            bool resultado = false;
            Person C = new Person();
            try
            {
                using (EngineContext Context = new EngineContext())
                {
                    foreach (Person person in persons)
                    { 
                        C = Context.Person.Where(s => s.Dni == person.Dni).FirstOrDefault();
                        Context.Person.Attach(C);
                        C.Nombre = person.Nombre;
                        C.Apellido = person.Apellido;
                        C.Company = person.Company;
                        C.Date = DateTime.UtcNow;
                        C.Email = person.Email;
                        C.Foto = person.Foto;
                        C.Qr = person.Qr;
                        C.Rh = person.Rh;
                        C.Grado = person.Grado;
                        C.Grupo = person.Grupo;
                        C.Matricula = person.Matricula;
                        C.Status = person.Status;
                        Context.Configuration.ValidateOnSaveEnabled = false;
                        Context.SaveChanges();
                    }
                    resultado = true;
                }
            }
            catch { }
            return resultado;
        }

    }
}
