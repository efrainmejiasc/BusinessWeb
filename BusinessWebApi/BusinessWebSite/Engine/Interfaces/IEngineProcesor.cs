using BusinessWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessWebSite.Engine.Interfaces
{
    public interface IEngineProcesor
    {
        Task<string> GetGrupos(string accessToken, IEngineHttp FuncionHttp);
        Task<string> GetGrados(string accessToken, IEngineHttp FuncionHttp);
        Task<bool> CreateUserApi(string jsonUserApi, IEngineHttp FuncionHttp);
        Task<string> GetPerson(string dni, string accessToken, IEngineHttp FuncionHttp);
        Task <TicketAcceso> GetTicketAccesoAsync(string jsonUserApi, IEngineHttp FuncionHttp);
        Task<bool> RegisterDevice(string jsonData, string accessToken, IEngineHttp FuncionHttp);
        Task<string> GetAsistencia(string accessToken, string fecha, string grado, string grupo,int idCompany, IEngineHttp FuncionHttp);
    }
}
