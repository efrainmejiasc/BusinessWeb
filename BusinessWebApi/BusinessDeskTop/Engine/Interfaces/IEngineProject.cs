﻿using BusinessDeskTop.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDeskTop.Engine.Interfaces
{
    public interface IEngineProject
    {
        bool CreateFileXlsx(DataTable dt);
        List<Person> LeerArchivo(string pathArchivo, IEngineTool tool);
        Task<string> GetAccessTokenAsync(IEngineTool Tool, IEngineHttp HttpFuncion);
        bool CreateFileXlsx(List<Person> persons, string pathFile, IEngineTool Tool);
    }
}