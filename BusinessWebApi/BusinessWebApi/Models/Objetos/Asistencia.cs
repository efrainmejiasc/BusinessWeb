using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessWebApi.Models.Objetos
{
    public class Asistencia
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido  { get; set; }
        public string Dni { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        public DateTime CreateDate { get; set; }
        public string Grado { get; set; }
        public string Grupo { get; set; }
        public int IdCompany { get; set; }
        public string Materia{ get; set; }
        public string Foto { get; set; }
        public string DniAdm { get; set; }
        public string NombreProfesor { get; set; }
        public string Observacion { get; set; }
    }
}