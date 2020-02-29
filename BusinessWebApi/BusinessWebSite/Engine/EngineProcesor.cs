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
    }
}