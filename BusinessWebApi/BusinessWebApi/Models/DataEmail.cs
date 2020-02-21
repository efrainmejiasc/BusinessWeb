using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessWebApi.Models
{
    public class DataEmail
    {
        public string EmailTo { get; set; }

        public string Asunto { get; set; }

        public string Cuerpo { get; set; }
    }
}
