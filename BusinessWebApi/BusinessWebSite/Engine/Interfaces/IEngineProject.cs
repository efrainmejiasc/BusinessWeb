using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessWebSite.Engine.Interfaces
{
    public interface IEngineProject
    {
        string BuildUserApiStr(string user, string password, IEngineTool Tool);
        string BuildCreateUserApiStr(string user, string email, string password);
        string BuildRegisterDeviceStr(string user, string email, string codigo, string phone, string dni,string nombre);
        string BuidObservacionAsistencia(int idAsistencia, string dni, bool status, string materia, string observacion, string dniAdm, int idCompany);
    }
}
