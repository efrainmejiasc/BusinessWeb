using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessWebSite.Models
{
    public class HistoriaAsistenciaPerson
    {
        public string Materia { get; set; }

        public int NumeroInasistencia { get; set; }

        public string FechaInasistencia { get; set; }
    }
}