using BusinessWebApi.Engine.Interfaces;
using BusinessWebApi.Models;
using BusinessWebApi.Models.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public bool UpdateUserApi(int idCompany, string nameCompany, string user, string email)
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

        public UserApi GetUserApi(string strValue)
        {
            UserApi C = null;
            try
            {
                using (EngineContext Context = new EngineContext())
                {
                    C = Context.UserApi.Where(s => s.User == strValue || s.Email == strValue).FirstOrDefault();
                    return C;
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/GetUserApi*" + strValue));
            }
            return C;
        }


        public bool ExistsUserApi(string strValue)
        {
            bool resultado = false;
            UserApi C = new UserApi();
            try
            {
                using (EngineContext Context = new EngineContext())
                {
                    C = Context.UserApi.Where(s => s.User == strValue || s.Email == strValue).FirstOrDefault();
                    if (C != null)
                        resultado = true;
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/ExistsUserApi*" + strValue));
            }
            return resultado;
        }

        public bool UpdateUserApi(string userName, string email, string password)
        {
            bool resultado = false;
            string pass1 = Tool.ConvertirBase64(email + password);
            string pass2 = Tool.ConvertirBase64(userName + password);
            UserApi C = new UserApi();
            try
            {
                using (EngineContext Context = new EngineContext())
                {
                    C = Context.UserApi.Where(s => s.User == userName || s.Email == email).FirstOrDefault();
                    Context.UserApi.Attach(C);
                    C.Password = pass1;
                    C.Password = pass2;
                    Context.Configuration.ValidateOnSaveEnabled = false;
                    Context.SaveChanges();
                }
                resultado = true;
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/UpdateUserApi*" + email));
            }
            return resultado;
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

        public string GetCompanyName(int idCompany)
        {
            Company company = null;
            try
            {
                using (EngineContext context = new EngineContext())
                {
                    company = context.Company.Where(s => s.Id == idCompany).FirstOrDefault();
                    return company.NameCompany;
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/GetCompanyNamme*" + idCompany.ToString()));
            }
            return string.Empty;
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
                    person.Identificador = Tool.ConvertirBase64(person.Nombre + "#" + person.Apellido + "#"+ person.Dni);
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
                    person.Identificador = Tool.ConvertirBase64(person.Nombre + "#" + person.Apellido + "#" + person.Dni);
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
                    C.Identificador = person.Identificador;
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
                asistencia.CreateDate = DateTime.UtcNow.Date;
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

        public List<Asistencia> GetAsistenciaClase(string fecha, string grado, string grupo,int turno, int idCompany)
        {
            List<Asistencia> lista = new List<Asistencia>();
            DateTime date = Convert.ToDateTime(fecha);
            using (EngineContext context = new EngineContext())
            {
                lista = (from P in context.Person
                         join A in context.AsistenciaClase
                         on P.Dni equals A.Dni
                         where A.CreateDate == date && P.Grado == grado && P.Grupo == grupo && P.Turno == turno && P.IdCompany == idCompany && A.IdCompany == idCompany
                         select new Asistencia()
                         {
                             Id = A.Id,
                             Nombre = P.Nombre,
                             Apellido = P.Apellido,
                             Status = A.Status,
                             CreateDate = A.CreateDate,
                             Dni = P.Dni,
                             Email = P.Email,
                             Grado = P.Grado,
                             Grupo = P.Grupo,
                             IdCompany = P.IdCompany,
                             Materia = A.Materia,
                             Foto = P.Foto,
                             DniAdm = A.DniAdm
                         }).ToList();
            }
            return lista;
        }

        public AsistenciaClase GetAsistenciaClase(string fecha, string dni , string materia, string grado, string grupo, int idCompany)
        {
            AsistenciaClase asistencia = new AsistenciaClase();

            DateTime date = Convert.ToDateTime(fecha);
            using (EngineContext context = new EngineContext())
            {
               asistencia = context.AsistenciaClase.Where(x => x.CreateDate == date && x.Dni == dni && x.Materia == materia && x.Grado == grado && x.Grupo == grupo && x.IdCompany == idCompany).FirstOrDefault();
            }
            return asistencia;
        }



        public bool UpdateAsistenciaClase(List<AsistenciaClase> asis)
        {
            bool resultado = false;
            AsistenciaClase C = new AsistenciaClase();
            try
            {
                using (EngineContext Context = new EngineContext())
                {
                    foreach (AsistenciaClase a in asis)
                    {
                        C = Context.AsistenciaClase.Where(s => s.Dni == a.Dni && s.CreateDate == a.CreateDate && s.EmailSend == a.EmailSend && s.Status == false).FirstOrDefault();
                        Context.AsistenciaClase.Attach(C);
                        C.EmailSend = a.EmailSend;
                        Context.Configuration.ValidateOnSaveEnabled = false;
                        Context.SaveChanges();

                        resultado = true;
                    }
                }
            }
            catch (Exception ex)
            {
                string refe = asis[0].IdCompany.ToString() + " " + asis[0].Materia + " " + DateTime.UtcNow.Date.ToString();
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/UpdateAsistencia*" + refe));
            }
            return resultado;
        }

        public bool UpdateAsistenciaClase(int id , bool status)
        {
            bool resultado = false;
            AsistenciaClase C = new AsistenciaClase();
            try
            {
                using (EngineContext Context = new EngineContext())
                {
                    C = Context.AsistenciaClase.Where(s => s.Id == id).FirstOrDefault();
                    Context.AsistenciaClase.Attach(C);
                    C.Status = status;
                    Context.Configuration.ValidateOnSaveEnabled = false;
                    Context.SaveChanges();
                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/UpdateAsistencia*" + id.ToString()));
            }
            return resultado;
        }

        public bool NewObservacionClase(ObservacionClase observacion)
        {
            bool resultado = false;
            try
            {
                observacion.CreateDate = DateTime.UtcNow.Date;
                using (EngineContext context = new EngineContext())
                {
                    context.ObservacionClase.Add(observacion);
                    context.SaveChanges();
                }
               resultado  =  UpdateAsistenciaClase(observacion.IdAsistencia, observacion.Status);
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/NewObservacionClase*" + observacion.Dni));
            }
            return resultado;
        }

        public List<AsistenciaClase> StudentsNonAttending()
        {
            List<AsistenciaClase> noAsistentes = new List<AsistenciaClase>();

            DateTime Time = DateTime.UtcNow.Date;
            try {
                using (EngineContext context = new EngineContext())
                {
                    noAsistentes = context.AsistenciaClase.Where(x => x.Status == false && x.CreateDate == Time).ToList();
                }
            }
            catch(Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/StudentsNonAttending*" + "SS"));
            }
         
            return noAsistentes;
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

        public string EmailCompany(int idCompany)
        {
            Company company = new Company();
            try
            {
                using (EngineContext context = new EngineContext())
                {
                    company = context.Company.Where(s => s.Id == idCompany).FirstOrDefault();
                    return company.Email;
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/EmailCompany*" + idCompany.ToString()));
            }
            return string.Empty;
        }

        public string NameDevice(string dni)
        {
            DevicesCompany device = new DevicesCompany();
            try
            {
                using (EngineContext context = new EngineContext())
                {
                    device = context.DeviceCompany.Where(s => s.Dni == dni).FirstOrDefault();
                    return device.Nombre;
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/NameDevice*" + dni));
            }
            return string.Empty;
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
        public object GetDniUserApi (int id , int idCompany,string email)
        {
            try
            {
                using (EngineContext context = new EngineContext())
                {
                    var dni = (from A in context.UserApi join B in context.DeviceCompany
                               on A.Id equals B.IdUserApi
                               where A.Id == id && A.Email == email && B.IdCompany == idCompany
                               select new { B.Dni }).FirstOrDefault();
                    return dni;
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/GetDniUser*" + id.ToString()));
            }
            return string.Empty;
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

        public List<Person> GetPerson(List<AsistenciaClase> asis)
        {
            List<Person> personas = new List<Person>();
            Person person = new Person();
            foreach(AsistenciaClase i in asis)
            {
                person = GetPerson(i.Dni);
                if (person != null)
                      personas.Add(person);
            }
            return personas;
        }

        public Person GetPerson(string dni)
        {
            Person person = null;
            try
            {
                using (EngineContext context = new EngineContext())
                {
                    person = context.Person.Where(s => s.Dni == dni).FirstOrDefault();
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

        public Person GetPerson2 (string identificador)
        {
           Person person = null;
            try
            {
                using (EngineContext context = new EngineContext())
                {
                    person = context.Person.Where(s => s.Identificador == identificador ).FirstOrDefault();
                    if (person != null)
                        return person;
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/GetPerson*" + Tool.DecodeBase64(identificador)));
            }
            return null;
        }

       public List<Person> GetPersonList(int idCompany, string grado, string grupo, int idTurno)
        {
            List<Person> persons = new List<Person>();
            try
            {
                using (EngineContext context = new EngineContext())
                {
                    persons = context.Person.Where(s => s.IdCompany == idCompany && s.Grado == grado && s.Grupo == grupo && s.Turno == idTurno).ToList();
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/GetPersonList*" + idCompany.ToString()));
            }
            return persons;
        }

        public List<Company> GetAllCompany()
        {
            List<Company> companys = new List<Company>();
            using (EngineContext context = new EngineContext())
            {
                companys = context.Company.ToList();
            }
            return companys;
        }

        public bool UpdateCompany(Company company)
        {
            bool resultado = false;
            Company C = new Company();
            try
            {
                using (EngineContext Context = new EngineContext())
                {
                    C = Context.Company.Where(s => s.Id == company.Id).FirstOrDefault();
                    Context.Company.Attach(C);
                    C.NameCompany = company.NameCompany;
                    C.Ref = company.Ref;
                    C.Phone = company.Phone;
                    C.NumberDevices = company.NumberDevices;
                    C.Email = company.Email;
                    C.Status = company.Status;
                    Context.Configuration.ValidateOnSaveEnabled = false;
                    Context.SaveChanges();

                    resultado = true;
                }
            }
            catch (Exception ex)
            {
                InsertarSucesoLog(Funcion.ConstruirSucesoLog(ex.ToString() + "*EngineDb/UpdateCompany*" + company.Email));
            }
            return resultado;
        }

        public List<Grado> GetGrados(int idCompany)
        {
            List<Grado> grados = new List<Grado>();
            List<Person> p = new List<Person>();
            List<string> l = new List<string>();
            using (EngineContext context = new EngineContext())
            {
                p = context.Person.Where(s => s.IdCompany == idCompany).ToList();
            }
            if (p.Count > 0)
            {
                Grado grado;
                int n = 0;
                foreach (Person i in p)
                {
                    grado = new Grado();
                    grado.Id = n;
                    grado.NombreGrado = i.Grado.Trim();

                    if (!l.Contains(i.Grado.Trim()))
                    {
                        l.Add(grado.NombreGrado);
                        grados.Insert(n,grado);
                        n++;
                    }
                }
            }
            return grados;
        }

        public List<Grupo> GetGrupos(int idCompany)
        {
            List<Grupo> grupos = new List<Grupo>();
            List<Person> p = new List<Person>();
            List<string> l = new List<string>();
            using (EngineContext context = new EngineContext())
            {
                p = context.Person.Where(s => s.IdCompany == idCompany).ToList();
            }
            if (p.Count > 0)
            {
                Grupo grupo;
                int n = 0;
                foreach (Person i in p)
                {
                    grupo = new Grupo();
                    grupo.Id = n;
                    grupo.NombreGrupo = i.Grupo.Trim();

                    if (!l.Contains(i.Grupo.Trim()))
                    {
                        l.Add(grupo.NombreGrupo);
                        grupos.Insert(n, grupo);
                        n++;
                    }
                }
            }
            return grupos;
        }

        public List<Models.Objetos.Turno> GetTurnos(int idCompany)
        {
            List<Models.Objetos.Turno> turnos = new List<Models.Objetos.Turno>();
            List<Person> p = new List<Person>();
            List<string> l = new List<string>();
            using (EngineContext context = new EngineContext())
            {
                p = context.Person.Where(s => s.IdCompany == idCompany).ToList();
            }
            if(p.Count > 0)
            {
                Models.Objetos.Turno turno;
                int n = 0;
                foreach (Person i in p)
                {
                    turno = new Models.Objetos.Turno();
                    turno.Id = n;
                    if (i.Turno == 1)
                        turno.NombreTurno = "MAÑANA";
                    else if (i.Turno == 2)
                        turno.NombreTurno = "TARDE";
                    else if (i.Turno == 3)
                        turno.NombreTurno = "NOCHE";

                    if (!l.Contains(i.Grupo.Trim()))
                    {
                        l.Add(turno.NombreTurno);
                        turnos.Insert(n, turno);
                        n++;
                    }
                }

            }

            return turnos;
        }

        public List<HistoriaAsistenciaPerson> GetHistoriaAsistenciaPerson(string dni)
        {
            SqlConnection conexion = new SqlConnection(EngineData.CNX);
            List<HistoriaAsistenciaPerson> registros = new List<HistoriaAsistenciaPerson>();
            using (conexion)
            {
                conexion.Open();
                SqlCommand command = new SqlCommand("Sp_GetHistoriaAsistenciaPerson", conexion);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Dni", dni);
                SqlDataReader lector = command.ExecuteReader();
                while (lector.Read())
                {
                    HistoriaAsistenciaPerson registro = new HistoriaAsistenciaPerson()
                    {
                        Materia = lector.GetString(0),
                        NumeroInasistencia = lector.GetInt32(1), 
                    };
                    registros.Add(registro);
                }
                lector.Close();
                conexion.Close();
            }

            return registros;
        }

        public List<Materias> GetMaterias(int idCompany)
        {
            List<Materias> materias = new List<Materias>();
            using (EngineContext context = new EngineContext())
            {
                materias = context.Materias.Where(s => s.IdCompany == idCompany ).ToList();
            }
            return materias;
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
