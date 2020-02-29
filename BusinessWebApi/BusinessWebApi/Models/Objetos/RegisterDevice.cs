using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessWebApi.Models.Objetos
{
    public class RegisterDevice
    {
        public string User { get; set; }
        public int IdCompany {get;set;}
        public string NameCompany { get; set; }
        public string Email { get; set;}
        public string Phone { get; set;}
        public string Codigo { get; set; }
        public int NumberDevice { get; set; }
        public int IdUserApi { get; set; }
        public string Dni { get; set; }

    }
}