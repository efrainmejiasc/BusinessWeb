using BusinessDeskTop.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDeskTop.Engine.Interfaces
{
    public interface IEngineProject
    {
        List<Person> LeerArchivo(string pathArchivo, IEngineTool tool);
    }
}
