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

        private DataTable dt = new DataTable();

        public void SetDt (DataTable _dt)
        {
            dt = _dt;
        }

        public DataTable GetDt()
        {
            return dt;
        }
    }
}
