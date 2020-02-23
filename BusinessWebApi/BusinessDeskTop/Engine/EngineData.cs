using BusinessDeskTop.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDeskTop.Engine
{
    public class EngineData
    {
        private static EngineData valor;

        public static EngineData Instance()
        {
            if ((valor == null))
            {
                valor = new EngineData();
            }
            return valor;
        }

        public string NombreEmpresa {get;set; }

        private DataTable dt = new DataTable();
        public void SetDt (DataTable _dt)
        {
            dt = _dt;
        }

        public DataTable GetDt()
        {
            return dt;
        }

        private List<Person> persons = new List<Person>();
        public void SetPersons(List<Person> _persons)
        {
            persons = _persons;
        }

        public List<Person> GetPersons()
        {
            return persons;
        }
    }
}
