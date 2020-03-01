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
        Task<bool> CreateUserApi(string jsonUserApi, IEngineHttp FuncionHttp);
        Task<string> GetPerson(string dni, string accessToken, IEngineHttp FuncionHttp);
        Task <TicketAcceso> GetTicketAccesoAsync(string jsonUserApi, IEngineHttp FuncionHttp);
        Task<bool> RegisterDevice(string jsonData, string accessToken, IEngineHttp FuncionHttp);
    }
}
