
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
using System.Web.Mvc;
using System.Web.Routing;

namespace BusinessWebSite.Engine
{
    public class EngineHttp :IEngineHttp
    {
        public async Task<TicketAcceso> GetAccessToken(string jsonData)
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
                }
                else
                {
                    if (response.StatusCode.ToString() == "404")
                    {
                       /* if (HttpContext.Current.Session["Password"] != null   && HttpContext.Current.Session["User"] != null && HttpContext.Current.Session["Email"] != null) 
                        {
                            var Tool = new EngineTool();
                            var Funcion = new EngineProject();
                            string jsonUserApiStr = Funcion.BuildUserApiStr(HttpContext.Current.Session["User"].ToString(), HttpContext.Current.Session["Password"].ToString(),Tool);
                            ticketAcceso = await GetAccessToken(jsonUserApiStr);
                        }
                        else
                        {
                            var context = new RequestContext(new HttpContextWrapper(HttpContext.Current), new RouteData());
                            var urlHelper = new UrlHelper(context);
                            var url = urlHelper.Action("Index", new { OtherParm = "Home" });
                            HttpContext.Current.Response.Redirect(url);
                        }*/
                    }
                  
                }
                return ticketAcceso;
            }
        }

        public async Task<string> GetRefreshToken(string jsonData)
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
                }
                return ticketAcceso.access_token;
            }
        }

        public async Task<bool> CreateUserApi(string jsonData)
        {
            string respuesta = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PostAsync(EngineData.UrlBase + "UserApi/CreateUser", new StringContent(jsonData, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                    if (respuesta == "transaccion exitosa") 
                        return true;
                }
            }
            return false;
        }

        public async Task<bool> RegisterDevice(string jsonData,string strToken)
        {
            string respuesta = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
                HttpResponseMessage response = await client.PostAsync(EngineData.UrlBase + "DeviceApi/RegisterDevice", new StringContent(jsonData, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                    if (respuesta == "transaccion exitosa")
                        return true;
                }
            }
            return false;
        }

        public async Task<string> GetPerson (string dni,string strToken)
        {
            string respuesta = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
                HttpResponseMessage response = await client.GetAsync(EngineData.UrlBase + "PersonApi/GetPerson?dni=" + dni);
                if (response.IsSuccessStatusCode)
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                }else
                {
                    respuesta = "NO AUTORIZADO";
                }

            }
            return respuesta;
        }

        public async Task<string> GetPerson(string grado , string grupo , int idCompany, string strToken, int turno = 1 )
        {
            string respuesta = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
                HttpResponseMessage response = await client.GetAsync(EngineData.UrlBase + "PersonApi/GetPersonFull?idCompany=" + idCompany + "&grado=" + grado + "&grupo=" + grupo + "&turno=" + turno);
                if (response.IsSuccessStatusCode)
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    respuesta = "NO AUTORIZADO";
                }

            }
            return respuesta;
        }

        public async Task<string> GetGrados(string strToken)
        {
            string respuesta = string.Empty;
            List<Grado> grados = new List<Grado>();
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
                HttpResponseMessage response = await client.GetAsync(EngineData.UrlBase + "PersonApi/GetGrados");
                if (response.IsSuccessStatusCode)
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                }
            }
            return respuesta;
        }

        public async Task<string> GetGrupos(string strToken)
        {
            string respuesta = string.Empty;
            List<Grado> grados = new List<Grado>();
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
                HttpResponseMessage response = await client.GetAsync(EngineData.UrlBase + "PersonApi/GetGrupos");
                if (response.IsSuccessStatusCode)
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                }
            }
            return respuesta;
        }

        public async Task<string> GetAsistencia (string strToken,string fecha,string grado,string grupo,int idCompany)
        {
            string respuesta = string.Empty;
            List<Grado> grados = new List<Grado>();
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
                HttpResponseMessage response = await client.GetAsync(EngineData.UrlBase + "AsistenciaClaseApi/GetAsistenciaClase?fecha=" + fecha + "&grado=" + grado + "&grupo=" + grupo + "&idCompany=" + idCompany.ToString());
                if (response.IsSuccessStatusCode)
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                }
            }
            return respuesta;
        }

        public async Task<bool> UpdateObservacionAsistencia(string jsonData, string strToken)
        {
            string respuesta = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
                HttpResponseMessage response = await client.PostAsync(EngineData.UrlBase + "AsistenciaClaseApi/ObservacionClase", new StringContent(jsonData, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                    if (respuesta == "transaccion exitosa")
                        return true;
                }
            }
            return false;
        }

        public async Task<string> GetHistoriaAsistenciaPerson(string dni, string strToken)
        {
            string respuesta = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
                HttpResponseMessage response = await client.GetAsync(EngineData.UrlBase + "AsistenciaClaseApi/GetHistoriaAsistenciaPerson?dni=" + dni);
                if (response.IsSuccessStatusCode)
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    respuesta = "NO AUTORIZADO";
                }

            }
            return respuesta;
        }

    }
}