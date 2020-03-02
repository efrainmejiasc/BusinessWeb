using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessWebApi.Models.Objetos
{
    public class DataEmailNoAsistencia
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public string Email{ get; set; }
        public DateTime Fecha { get; set; }
    }
}