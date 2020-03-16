using BusinessDeskTop.Models;
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
        DataTable BuildDtPerson();
        DataTable BuildDtComppany();
        bool CreateFileXlsx(DataTable dt);
        bool SetLXlsxOut(string pathArchivo);
        string SetJsonPerson(List<Person> p);
        DataTable SetDtCompany(List<Company> companys, DataTable dt);
        List<Person> LeerArchivo(string pathArchivo, IEngineTool tool);
        Task<string> GetAccessTokenAsync(IEngineTool Tool, IEngineHttp HttpFuncion);
        bool CreateFileXlsx(List<Person> persons, string pathFile, IEngineTool Tool);
    }
}
