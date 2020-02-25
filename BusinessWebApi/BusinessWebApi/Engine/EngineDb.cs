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
            string pass1= Tool.ConvertirBase64(user.Email + user.Password);
            string pass2 = Tool.ConvertirBase64(user.User + user.Password);
            user.Password = pass1;
            user.Password2 = pass2;
            user.IdCompany =  GetCompanyId(user.Company);
            user.Status = true;
            try
            {
                using (EngineContext context = new EngineContext())
                {
                    context.UserApi.Add(user);
                    context.SaveChanges();
                }
                resultado = true;
            }
            catch (Exception ex) { }
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
            catch (Exception ex) { }
            return null;
        }

        public UserApi GetUserSuspended(string password, string password2)
        {
            UserApi user = null;
            try
            {
                using (EngineContext context = new EngineContext())
                {
                    user = context.UserApi.Where(s => (s.Password == password || s.Password2 == password2) && s.Status == false).FirstOrDefault();
                    if (user != null)
                        return user;
                }
            }
            catch (Exception ex) { }
            return null;
        }

        public int GetCompanyId(string nameCompany)
        {
            Company company = null;
            try
            {
                using (EngineContext context = new EngineContext())
                {
                    company = context.Company.Where(s => s.NameCompany == nameCompany.Trim()).FirstOrDefault();
                        return company.Id;
                }
            }
            catch (Exception ex){ }
            return 0;
        }

        public bool CreatePerson(List<Person> persons)
        {
            bool resultado = false;
            try
            {
                foreach (Person person in persons)
                {
                    person.IdCompany = GetCompanyId(person.Company);
                    CreatePerson(person);
                }
                resultado = true;
            }
            catch (Exception ex) { }
            return resultado;
        }

        public bool CreatePerson (Person person)
        {
            bool resultado = false;
            try
            {
                using (EngineContext context = new EngineContext())
                {
                    person.Date = DateTime.UtcNow;
                    person.Status = true;
                    context.Person.Add(person);
                    context.SaveChanges();
                }
                resultado = true;
            }
            catch (Exception ex) { }
            return resultado;
        }

        public bool UpdatePerson(List<Person> persons)
        {
            bool resultado = false;
            Person C = new Person();
            try
            {
                foreach (Person person in persons)
                {
                    UpdatePerson(person);
                }
                resultado = true;
            }
            catch (Exception ex) { }
            return resultado;
        }

        public bool UpdatePerson(Person person)
        {
            bool resultado = false;
            Person C = new Person();
            try
            {
                using (EngineContext Context = new EngineContext())
                {
                    C = Context.Person.Where(s => s.Dni == person.Dni).FirstOrDefault();
                    if (C == null)
                    {
                        CreatePerson(person);
                        return true;
                    }
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

                    resultado = true;
                }
            }
            catch (Exception ex) { }
            return resultado;
        }

        public bool CreateCompany( Company company)
        {
            bool resultado = false;
            company.Status = true;
            company.CreateDate = DateTime.UtcNow;
            try
            {
                using (EngineContext context = new EngineContext())
                {
                    context.Company.Add(company);
                    context.SaveChanges();
                    //return company.Id;
                }
                resultado = true;
            }
            catch (Exception ex) { }
            return resultado;
        }

    }
}
