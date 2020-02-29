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
        Task<bool> CreateUserApi(string jsonData);
        Task<string> GetRefreshToken(string jsonData);
        Task<TicketAcceso> GetAccessToken(string jsonData);
        Task<bool> RegisterDevice(string jsonData, string strToken);
    }
}
