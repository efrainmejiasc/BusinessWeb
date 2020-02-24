using BusinessDeskTop.Engine.Interfaces;
using BusinessDeskTop.Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDeskTop.Engine
{
    public class EngineHttp : IEngineHttp
    {
        public async Task<string> GetAccessToken(string jsonData)
        {
            TicketAcceso ticketAcceso = new TicketAcceso();
            string respuesta = string.Empty;
            using (HttpClient client = new HttpClient())
            {   
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsync("http://localhost:50045/api/UserApi/Login", new StringContent(jsonData, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                    ticketAcceso = JsonConvert.DeserializeObject<TicketAcceso>(respuesta);
                }
            }
            return ticketAcceso.access_token;
        }

        public async Task<bool> UploadPersonToApi(string strToken,string jsonData)
        {
            string respuesta = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
                HttpResponseMessage response = await client.PostAsync("http://localhost:50045/api/PersonApi/CreatePerson", new StringContent(jsonData, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> CreateCompany (string strToken, string jsonData)
        {
            string respuesta = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
                HttpResponseMessage response = await client.PostAsync("http://localhost:50045/api/CompanyApi/CreateCompany", new StringContent(jsonData, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                    return true;
                }
            }
            return false;
        }

    }
}
