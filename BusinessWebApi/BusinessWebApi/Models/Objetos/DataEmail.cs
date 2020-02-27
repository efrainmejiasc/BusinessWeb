using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessWebApi.Models.Objetos
{
    public class DataEmail
    {
        public string EmailTo { get; set; }

        public string Asunto { get; set; }

        public string Cuerpo { get; set; }
    }
}