using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessWebSite.Models
{
    public class Person
    {

        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Dni { get; set; }

        public string Matricula { get; set; }
        public string Rh { get; set; }
        public string Grado { get; set; }

        public string Grupo { get; set; }

        public string Email { get; set; }

        public int IdCompany { get; set; }

        public string Company { get; set; }

        public DateTime Date { get; set; }

        public bool Status { get; set; }

        public string Foto { get; set; }

        public string Qr { get; set; }
    }
}