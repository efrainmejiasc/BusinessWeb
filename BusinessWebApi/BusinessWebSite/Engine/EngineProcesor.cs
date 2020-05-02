using BusinessWebSite.Engine.Interfaces;
using BusinessWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BusinessWebSite.Engine
{
    public class EngineProcesor : IEngineProcesor
    {
        public async Task<TicketAcceso> GetTicketAccesoAsync(string jsonUserApi,IEngineHttp FuncionHttp)
        {
            TicketAcceso ticket = await FuncionHttp.GetAccessToken(jsonUserApi);
            return ticket;
        }

        public async Task<bool> CreateUserApi (string jsonUserApi, IEngineHttp FuncionHttp)
        {
            return await FuncionHttp.CreateUserApi(jsonUserApi);
        }

        public async Task<bool> RegisterDevice(string jsonData, string accessToken , IEngineHttp FuncionHttp)
        {
            return await FuncionHttp.RegisterDevice(jsonData,accessToken);
        }

        public async Task<string> GetPerson(string dni, string accessToken, IEngineHttp FuncionHttp)
        {
            return await FuncionHttp.GetPerson(dni, accessToken);
        }

        public async Task<string> GetPerson(string grado , string grupo, int idCompany , string accessToken, IEngineHttp FuncionHttp, int turno = 1 )
        {
            return await FuncionHttp.GetPerson(grado, grupo, idCompany, accessToken,turno);
        }

        public async Task<string> GetGrados(string accessToken, int idCompany,IEngineHttp FuncionHttp)
        {
            return await FuncionHttp.GetGrados(accessToken,idCompany);
        }

        public async Task<string> GetGrupos(string accessToken, int idCompany, IEngineHttp FuncionHttp)
        {
            return await FuncionHttp.GetGrupos(accessToken,idCompany);
        }

        public async Task<string> GetTurnos(string accessToken, int idCompany, IEngineHttp FuncionHttp)
        {
            return await FuncionHttp.GetTurnos(accessToken, idCompany);
        }
    
        public async Task<string> GetAsistencia (string accessToken, string fecha, string grado,string grupo, int turno, int idCompany, IEngineHttp FuncionHttp)
        {
            return await FuncionHttp.GetAsistencia(accessToken,fecha,grado,grupo,turno,idCompany);
        }

        public async Task<bool> UpdateObservacionAsistencia(string jsonData, string accessToken, IEngineHttp FuncionHttp)
        {
            return await FuncionHttp.UpdateObservacionAsistencia(jsonData, accessToken);
        }

        public async Task<string> GetHistoriaAsistenciaPerson(string dni, string accessToken, IEngineHttp FuncionHttp)
        {
            return await FuncionHttp.GetHistoriaAsistenciaPerson(dni, accessToken);
        }

        public async Task<bool> UpdateUserApi(string jsonUserApi,IEngineHttp FuncionHttp)
        {
            return await FuncionHttp.UpdateUserApi(jsonUserApi);
        }

        public async Task<string> GetHistoriaAsistenciaMateria(string accessToken, string dni,string materia,string dniAdm, IEngineHttp FuncionHttp)
        {
            return await FuncionHttp.GetHistoriaAsistenciaMateria(accessToken, dni, materia, dniAdm);
        }

        public async Task<string> GetHistoriaAsistenciaPersonaXlsx(string dni, string accessToken, IEngineHttp FuncionHttp)
        {
            return await FuncionHttp.GetHistoriaAsistenciaPersonaXlsx(dni, accessToken);
        }

        public async Task<string> GetObservacionClase(string dni, string accessToken, IEngineHttp FuncionHttp)
        {
            return await FuncionHttp.GetObservacionClase(dni, accessToken);
        }
    }
}