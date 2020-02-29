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
            int numDevices = Metodo.NumberDevice(device.Codigo);
            if (numDevices == 0)
            {
                response.Content = new StringContent("no existe el codigo", Encoding.Unicode);
            }
            else
            {
                List<RegisterDevice> listDevice = Metodo.GetListDevicesRegistered(device.Codigo);
                if (listDevice.Count == numDevices)
                {
                    response.Content = new StringContent("limite para registrar alcanzado", Encoding.Unicode);
                }
                else
                {
                    DevicesCompany deviceCompany = Funcion.BuilDeviceCompany(listDevice, device, Metodo);
                    bool resultado = Metodo.RegisterDevice(deviceCompany);
                    if (resultado)
                        response.Content = new StringContent(EngineData.transaccionExitosa, Encoding.Unicode);
                    else
                        response.Content = new StringContent(EngineData.transaccionFallida, Encoding.Unicode);

                    response.Headers.Location = new Uri(EngineData.UrlBase + EngineData.UrlDevices);
                }    
            }
            return response;
        }
    }
}
