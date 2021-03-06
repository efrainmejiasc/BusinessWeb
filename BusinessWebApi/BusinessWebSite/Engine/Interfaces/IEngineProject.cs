﻿using BusinessWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessWebSite.Engine.Interfaces
{
    public interface IEngineProject
    {
        bool CrearDirectorioSiNoExiste(string folder);
        string BuildUserApiStr(string user, string password);
        string BuildUserApiStr(string user, string password, IEngineTool Tool);
        string BuildCreateUserApiStr(string user, string email, string password);
        string BuildRegisterDeviceStr(string user, string email, string codigo, string phone, string dni,string nombre);
        string BuildXlsxAsistenciaClase(List<HistoriaAsistenciaPerson> asis, List<ObservacionClase> observacion, string nombre, string apellido, string dni);
        string BuildObservacionAsistencia(int idAsistencia, string dni, bool status, string materia, string observacion, string dniAdm, int idCompany);
    }
}
