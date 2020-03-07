using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDeskTop.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string NameCompany { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; }
        public int NumberDevices { get; set; }
        public string Ref { get; set; }
    }
}
