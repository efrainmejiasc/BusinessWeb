using BusinessDeskTop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDeskTop.Engine.Interfaces
{
    public interface IEngineHttp
    {
        Task<string> GetAccessToken(string jsonData);
        bool UpdatePersonFoto(string dni, string foto);
        Task<List<Company>> GetAllCompany(string strToken);
        Task<bool> UpdateCompany(string strToken, string jsonData);
        Task<bool> CreateCompany(string strToken, string jsonData);
        Task<bool> UploadPersonToApi(string strToken, string jsonData);
        Task<bool> UploadPersonToApiUpdate(string strToken, string jsonData);

    }
}
