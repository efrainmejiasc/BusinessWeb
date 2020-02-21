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
        public DbSet<UserApi> UserApi { get; set; }
        // 1. Instalar Entity Framework
        // 2. Crear clase que herede de DbContext
        // 3. Ejecutar enable-migratios
        // 4. Ejecutar update-database -force -verbose
    }
}