using BusinessWebApi.Engine;
using BusinessWebApi.Engine.Interfaces;
using BusinessWebApi.Models;
using BusinessWebApi.Models.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace BusinessWebApi.Controllers
{
    public class DeviceApiController : ApiController
    {
        private readonly IEngineTool Tool;
        private readonly IEngineDb Metodo;
        private readonly IEngineProject Funcion;
        public DeviceApiController(IEngineTool _tool, IEngineDb _metodo, IEngineProject _funcion)
        {
            Tool = _tool;
            Metodo = _metodo;
            Funcion = _funcion;
        }

        [HttpPost]
        [ActionName("RegisterDevice")]
        public HttpResponseMessage RegisterDevice([FromBody] RegisterDevice device)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            if (string.IsNullOrEmpty(device.Codigo) || string.IsNullOrEmpty(device.Email) || string.IsNullOrEmpty(device.User) || string.IsNullOrEmpty(device.Dni))
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                response.Content = new StringContent(EngineData.modeloImcompleto, Encoding.Unicode);
                return response;
            }

            Company company = Metodo.GetCompanyCodigo(device.Codigo);
            if (company == null || company.Id == 0)
            {
                response.Content = new StringContent("No existe el codigo ingresado", Encoding.Unicode);
                return response;
            }
            DevicesCompany deviceCompany = new DevicesCompany();
            int countDevice = Metodo.NumberDeviceRegister(company.Id);
            if (countDevice == 0)
            {
                deviceCompany = Funcion.BuilDeviceCompany(company, device, Metodo);
            }
            else
            {
                List<RegisterDevice> listDevice = Metodo.GetListDevicesRegistered(device.Codigo);
                if (listDevice.Count > countDevice)
                {
                    response.Content = new StringContent("Limite para registrar dispositivos superado", Encoding.Unicode);
                    return response;
                }
                else
                {
                    deviceCompany = Funcion.BuilDeviceCompany(listDevice, device, Metodo);
                    if (deviceCompany == null)
                    {
                        response.Content = new StringContent("El usuario no existe", Encoding.Unicode);
                        return response;
                    }
                } 
            }
            bool resultado = Metodo.RegisterDevice(deviceCompany);
            if (resultado) 
            {
                Metodo.UpdateUserApi(company.Id, company.NameCompany, device.User, device.Email);
                response.Content = new StringContent(EngineData.transaccionExitosa, Encoding.Unicode);
                response.Headers.Location = new Uri(EngineData.UrlBase + EngineData.UrlDevices);
            }       
            else 
            { 
                response.Content = new StringContent(EngineData.transaccionFallida, Encoding.Unicode);
                response.Headers.Location = new Uri(EngineData.UrlBase + EngineData.UrlDevices);
            }
            return response;
        }
    }
}
