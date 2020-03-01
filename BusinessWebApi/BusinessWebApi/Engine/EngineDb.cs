using BusinessWebApi.Engine.Interfaces;
using BusinessWebApi.Models;
using BusinessWebApi.Models.Objetos;
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
            catch (Exception ex) 
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/CreateUser*" + " "));
            }
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
            catch (Exception ex) 
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/GetUser1*" + password));
            }
            return null;
        }

        public UserApi GetUser(string [] userApi)
        {
            UserApi user = null;
            string email = userApi[0];
            string usuario = userApi[1];
            try
            {
                using (EngineContext context = new EngineContext())
                {
                    user = context.UserApi.Where(s => s.Email == email  && s.User == usuario ).FirstOrDefault();
                    if (user != null)
                        return user;
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/GetUser2*" + user));
            }
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
            catch (Exception ex) 
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/GetUserSuspend*" + password));
            }
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
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/GetCompanyId*" + nameCompany));
            }
            return 0;
        }

        public Company GetCompanyCodigo(string codigo)
        {
            Company company = null;
            try
            {
                using (EngineContext context = new EngineContext())
                {
                    company = context.Company.Where(s => s.Codigo == codigo.Trim()).FirstOrDefault();
                }
            }
            catch (Exception ex) 
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/GetCompanyCodigo*" + codigo));
            }
            return company;
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
            catch (Exception ex) 
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/CreatePerson*" + person.Email));
            }
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
            catch (Exception ex) 
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/UpdatePerson*" + person.Email));
            }
            return resultado;
        }

        public bool CreateCompany(Company company)
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
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/CreateCompany*" + company.Email));
            }
            return resultado;
        }

        public bool NewAsistenciaClase (List<AsistenciaClase> asistencias)
        {
            bool resultado = false;
            try
            {
                foreach (AsistenciaClase asistencia in asistencias)
                {
                    NewAsistenciaClase(asistencia);
                }
                resultado = true;
            }
            catch (Exception ex) { }
            return resultado;
        }

        public bool NewAsistenciaClase (AsistenciaClase asistencia)
        {
            bool resultado = false;
            try
            {
                using (EngineContext context = new EngineContext())
                {
                    context.AsistenciaClase.Add(asistencia);
                    context.SaveChanges();
                }
                resultado = true;
            }
            catch (Exception ex) 
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/NewAsistenciaClase*" + asistencia.Dni));
            }
            return resultado;
        }

        public bool NewAsistenciaComedor(List<AsistenciaComedor> asistencias)
        {
            bool resultado = false;
            try
            {
                foreach (AsistenciaComedor asistencia in asistencias)
                {
                    NewAsistenciaComedor(asistencia);
                }
                resultado = true;
            }
            catch (Exception ex) { }
            return resultado;
        }

        public bool NewAsistenciaComedor(AsistenciaComedor asistencia)
        {
            bool resultado = false;
            try
            {
                using (EngineContext context = new EngineContext())
                {
                    context.AsistenciaComedor.Add(asistencia);
                    context.SaveChanges();
                }
                resultado = true;
            }
            catch (Exception ex) 
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/NewAsistenciaComedor*" + asistencia.Dni));
            }
            return resultado;
        }

        public bool ExistsCodeCompany(string codigo)
        {
            bool resultado = false;
            Company company = new Company();
            try
            {
                using (EngineContext context = new EngineContext())
                {
                    company = context.Company.Where(s => s.Codigo == codigo).FirstOrDefault();
                    if (company.Id > 0)
                        resultado = true;
                }
            }
            catch (Exception ex) 
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/ExistsCodeCompany*" + codigo));
            }
            return resultado;
        }

        public int NumberDevice (string codigo)
        {
            Company company = new Company();
            try
            {
                using (EngineContext context = new EngineContext())
                {
                    company = context.Company.Where(s => s.Codigo == codigo).FirstOrDefault();
                    return company.NumberDevices;
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/NumberDevice*" + codigo));
            }
            return 0;
        }

        public int NumberDeviceRegister(int idCompany)
        {
            int count = 0;
            try
            {
                using (EngineContext context = new EngineContext())
                {
                     count = (from DeviceCompany in context.DeviceCompany where DeviceCompany.IdCompany == idCompany from s in context.DeviceCompany select s).Count();
                     return count;
                }
            }
            catch (Exception ex) 
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/NumberDeviceRegister*" + idCompany.ToString()));
            }
            return 0;
        }

        public List<RegisterDevice> GetListDevicesRegistered(string codigo)
        {
            List<RegisterDevice> device = new List<RegisterDevice>();
            try
            {
                using (EngineContext context = new EngineContext())
                {

                   device = (from Company in context.Company join DevicesCompany in context.DeviceCompany 
                                                             on Company.Id equals DevicesCompany.IdCompany
                                                             where Company.Codigo == codigo && Company.Status == true
                                                             select new RegisterDevice() 
                                                             { 
                                                                IdCompany = Company.Id,
                                                                NameCompany = Company.NameCompany,
                                                                NumberDevice = Company.NumberDevices
                                                             }).ToList();
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/GetListDevicesRegistered*" + codigo));
            }
            return device;
        }
        public bool RegisterDevice (DevicesCompany device)
        {
            bool resultado = false;
            try
            {
                using (EngineContext context = new EngineContext())
                {
                    context.DeviceCompany.Add(device);
                    context.SaveChanges();
                }
                resultado = true;
            }
            catch (Exception ex) 
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/RegisterDevice*" + device.IdUserApi.ToString()));
            }
            return resultado;
        }

        public bool UpdateUserApi (int idCompany , string nameCompany ,string user,string email)
        {
            bool resultado = false;
            UserApi C = new UserApi();
            try
            {
                using (EngineContext Context = new EngineContext())
                {
                    C = Context.UserApi.Where(s => s.User == user && s.Email == email).FirstOrDefault();
                    Context.UserApi.Attach(C);
                    C.IdCompany = idCompany;
                    C.Company = nameCompany;
                    C.IdTypeUser = 2;
                    Context.Configuration.ValidateOnSaveEnabled = false;
                    Context.SaveChanges();

                    resultado = true;
                }
            }
            catch (Exception ex) 
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/UpdateUserApi*" + email));
            }
            return resultado;
        }

        public Person GetPerson (string dni)
        {
           Person person = null;
            try
            {
                using (EngineContext context = new EngineContext())
                {
                    person = context.Person.Where(s => s.Dni == dni ).FirstOrDefault();
                    if (person != null)
                        return person;
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/GetPerson*" + dni));
            }
            return null;
        }

        public bool InsertarSucesoLog(SucesoLog model)
        {
            bool resultado = false;
            try
            {
                using (EngineContext Context = new EngineContext())
                {
                    Context.SucesoLog.Add(model);
                    Context.SaveChanges();
                    resultado = true;
                }
            }
            catch { }
            return resultado;
        }

    }
}
