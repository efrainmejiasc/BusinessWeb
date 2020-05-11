using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessWebSite.Models
{
    public class ObservacionClase
    {
        public int Id { get; set; }
        public string Dni { get; set; }
        public int IdCompany { get; set; }
        public bool Status { get; set; }
        public DateTime CreateDate { get; set; }
        public string DniAdm { get; set; }
        public string Observacion { get; set; }
        public string Materia { get; set; }
        public int IdAsistencia { get; set; }
    }
}