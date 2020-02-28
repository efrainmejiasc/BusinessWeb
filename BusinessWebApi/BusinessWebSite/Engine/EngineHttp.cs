using BusinessWebApi.Engine;
using BusinessWebApi.Models;
using BusinessWebSite.Engine.Interfaces;
using BusinessWebSite.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BusinessWebSite.Engine
{
    public class EngineHttp :IEngineHttp
    {
        public async Task<string> GetAccessToken(string jsonData)
        {
            TicketAcceso ticketAcceso = new TicketAcceso();
            string respuesta = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsync(EngineData.UrlBase + "UserApi/Login", new StringContent(jsonData, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                    ticketAcceso = JsonConvert.DeserializeObject<TicketAcceso>(respuesta);
                }else
                {
                    respuesta = response.StatusCode.ToString();
                }
            }
            return ticketAcceso.access_token;
        }
    }
}