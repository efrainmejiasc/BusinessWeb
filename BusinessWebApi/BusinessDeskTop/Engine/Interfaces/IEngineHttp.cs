﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDeskTop.Engine.Interfaces
{
    public interface IEngineHttp
    {
        Task<string> GetAccessToken (string jsonData);
        Task<bool> CreateCompany(string strToken, string jsonData);
        Task<bool> UploadPersonToApi(string strToken, string jsonData);
        Task<bool> UploadPersonToApiUpdate(string strToken, string jsonData);
    }
}