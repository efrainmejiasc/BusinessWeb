using BusinessDeskTop.Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDeskTop.Engine
{
    public  class EngineProcesor
    {
        private readonly IEngineHttp HttpFuncion;
        private readonly IEngineProject Funcion;
        public EngineProcesor(IEngineHttp _HttpFuncion , IEngineProject _Funcion)
        {
            HttpFuncion = _HttpFuncion;
            Funcion = _Funcion;
        }


    }
}
