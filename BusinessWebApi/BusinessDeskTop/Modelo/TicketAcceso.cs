using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDeskTop.Modelo
{
    public class TicketAcceso
    {
        public string access_token { get; set; }
        public string expire_token { get; set; }
        public string type_token { get; set; }
        public string refresh_token { get; set; }
    }
}
