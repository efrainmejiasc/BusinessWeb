using BusinessWebApi.Engine;
using BusinessWebApi.Engine.Interfaces;
using BusinessWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace BusinessWebApi.Controllers
{
    public class AsistenciaClaseApiController : ApiController
    {
        private readonly IEngineTool Tool;
        private readonly IEngineDb Metodo;
        public AsistenciaClaseApiController(IEngineTool _tool, IEngineDb _metodo)
        {
            Tool = _tool;
            Metodo = _metodo;
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("AsistenciaClase")]
        public HttpResponseMessage AsistenciaClase([FromBody] List<AsistenciaClase> model)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

            if (model.Count == 0)
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                response.Content = new StringContent(EngineData.modeloImcompleto, Encoding.Unicode);
                return response;
            }

            bool resultado = false;
            resultado = Metodo.NewAsistenciaClase(model);
            if (!resultado)
            {
                response.Content = new StringContent(EngineData.falloCrearUsuario, Encoding.Unicode);
            }
            else
            {
                response.Content = new StringContent(EngineData.transaccionExitosa, Encoding.Unicode);
                response.Headers.Location = new Uri(EngineData.UrlBase + EngineData.UrlLogin);
            }

            return response;
        }
    }
}
