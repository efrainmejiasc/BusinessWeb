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
        Task<bool> UpdateUserApi(string jsonUserApi, IEngineHttp FuncionHttp);
        Task<bool> CreateUserApi(string jsonUserApi, IEngineHttp FuncionHttp);
        Task<string> GetPerson(string dni, string accessToken, IEngineHttp FuncionHttp);
        Task<string> GetGrupos(string accessToken, int idCompany, IEngineHttp FuncionHttp);
        Task<string> GetGrados(string accessToken, int idCompany, IEngineHttp FuncionHttp);
        Task<string> GetTurnos(string accessToken, int idCompany, IEngineHttp FuncionHttp);
        Task <TicketAcceso> GetTicketAccesoAsync(string jsonUserApi, IEngineHttp FuncionHttp);
        Task<bool> RegisterDevice(string jsonData, string accessToken, IEngineHttp FuncionHttp);
        Task<string> GetObservacionClase(string dni, string accessToken, IEngineHttp FuncionHttp);
        Task<string> GetHistoriaAsistenciaPerson(string dni, string accessToken, IEngineHttp FuncionHttp);
        Task<bool> UpdateObservacionAsistencia(string jsonData, string accessToken, IEngineHttp FuncionHttp);
        Task<string> GetHistoriaAsistenciaPersonaXlsx(string dni, string accessToken, IEngineHttp FuncionHttp);
        Task<string> GetPerson(string grado, string grupo, int idCompany, string accessToken, IEngineHttp FuncionHttp , int turno = 1);
        Task<string> GetHistoriaAsistenciaMateria(string accessToken, string dni, string materia, string dniAdm, IEngineHttp FuncionHttp);
        Task<string> GetAsistencia(string accessToken, string fecha, string grado, string grupo,int turno,int idCompany, IEngineHttp FuncionHttp);
    }
}
