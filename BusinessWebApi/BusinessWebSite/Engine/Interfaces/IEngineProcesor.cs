using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessWebSite.Engine.Interfaces
{
    public interface IEngineProcesor
    {
        Task GetTicketAccesoAsync(string jsonUserApi, IEngineHttp FuncionHttp);
    }
}
