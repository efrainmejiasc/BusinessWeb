using BusinessWebApi.Models.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessWebApi.Engine.Interfaces
{
    public interface IEngineNotify
    {
        bool EnviarEmail(DataEmail model);
        bool EnviarEmail(string emailTo, string codigo, string empresa);
    }
}
