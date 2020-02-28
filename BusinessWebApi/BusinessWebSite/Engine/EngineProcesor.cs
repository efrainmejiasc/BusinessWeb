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
        public async Task GetTicketAccesoAsync(string jsonUserApi,IEngineHttp FuncionHttp)
        {
           string ticket = await FuncionHttp.GetAccessToken(jsonUserApi);
        }
    }
}