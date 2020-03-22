using BusinessWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BusinessWebApi.Engine
{
    public class EngineContext:DbContext
    {
        public EngineContext() : base("CNX_DB") { }
        public DbSet<Turno> Turno { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<UserApi> UserApi { get; set; }
        public DbSet<TypeUser> TypeUser { get; set; }
        public DbSet<SucesoLog> SucesoLog { get; set; }
        public DbSet<DevicesCompany> DeviceCompany { get; set; }
        public DbSet<AsistenciaClase> AsistenciaClase { get; set; }
        public DbSet<AsistenciaComedor> AsistenciaComedor{ get; set; }
        public DbSet<ObservacionClase> ObservacionClase { get; set; }


        // 1. Instalar Entity Framework
        // 2. Crear clase que herede de DbContext
        // 3. Ejecutar enable-migratios
        // 4. Ejecutar update-database -force -verbose
    }
}