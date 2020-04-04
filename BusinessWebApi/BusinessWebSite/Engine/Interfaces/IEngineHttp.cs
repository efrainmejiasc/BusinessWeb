using BusinessWebApi.Models.Objetos;
using BusinessWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessWebSite.Engine.Interfaces
{
    public interface IEngineHttp
    {
        Task<string> GetGrupos(string strToken);
        Task<string> GetGrados(string strToken);
        Task<bool> UpdateUserApi(string jsonData);
        Task<bool> CreateUserApi(string jsonData);
        Task<string> GetRefreshToken(string jsonData);
        Task<TicketAcceso> GetAccessToken(string jsonData);
        Task<string> GetPerson(string dni, string strToken);
        Task<bool> RegisterDevice(string jsonData, string strToken);
        Task<string> GetHistoriaAsistenciaPerson(string dni, string strToken);
        Task<bool> UpdateObservacionAsistencia(string jsonData, string strToken);
        Task<string> GetPerson(string grado, string grupo, int idCompany, string strToken , int turno = 1 );
        Task<string> GetAsistencia(string strToken, string fecha, string grado, string grupo,int turno, int idCompany);
    }
}
