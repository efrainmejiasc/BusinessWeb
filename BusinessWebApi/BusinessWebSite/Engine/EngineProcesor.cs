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
    }
}