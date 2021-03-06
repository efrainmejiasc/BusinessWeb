﻿using BusinessDeskTop.Engine.Interfaces;

using BusinessDeskTop.Models;
using FastMember;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
                HttpResponseMessage response = await client.PostAsync(EngineData.UrlBase +  "UserApi/Login", new StringContent(jsonData, Encoding.UTF8, "application/json"));
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
                HttpResponseMessage response = await client.PostAsync(EngineData.UrlBase + "PersonApi/CreatePerson", new StringContent(jsonData, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                    if (respuesta == "transaccion exitosa")
                        return true;
                }else
                {
                    string s = response.StatusCode.ToString();
                }
            }
            return false;
        }


        public async Task<bool> UploadPersonToApiUpdate(string strToken, string jsonData)
        {
            string respuesta = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
                HttpResponseMessage response = await client.PutAsync(EngineData.UrlBase + "PersonApi/UpdatePerson", new StringContent(jsonData, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                    if (respuesta == "transaccion exitosa")
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
                HttpResponseMessage response = await client.PostAsync(EngineData.UrlBase + "CompanyApi/CreateCompany", new StringContent(jsonData, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                    //string urlValidacion = response.Headers.Location.ToString();
                    if (respuesta == "transaccion exitosa")
                        return true;
                }
            }
            return false;
        }

        public async Task<List<Company>> GetAllCompany(string strToken)
        {
            string respuesta = string.Empty;
            List<Company> companys = new List<Company>();
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
                HttpResponseMessage response = await client.GetAsync(EngineData.UrlBase + "CompanyApi/GetAllCompany");
                if (response.IsSuccessStatusCode)
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                    companys = JsonConvert.DeserializeObject<List<Company>>(respuesta);
                }
            }
            return companys;
        }

        public async Task<bool> UpdateCompany(string strToken, string jsonData)
        {
            string respuesta = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
                HttpResponseMessage response = await client.PutAsync(EngineData.UrlBase + "CompanyApi/UpdateCompany", new StringContent(jsonData, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    respuesta = await response.Content.ReadAsStringAsync();
                    if (respuesta == "transaccion exitosa")
                        return true;
                }
            }
            return false;
        }

        public bool UpdatePersonFoto(string dni, string foto)
        {
            bool resultado = false;
            string cnx = "Data Source=SQL5031.site4now.net;Initial Catalog=DB_A3EF3F_BusinessWeb;User Id=DB_A3EF3F_BusinessWeb_admin;Password=1234santiago";
            SqlConnection conexion = new SqlConnection(cnx);
            try
            {
                using (conexion)
                {
                    conexion.Open();
                    SqlCommand command = new SqlCommand("Sp_UpdatePersonFoto", conexion);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Dni", dni);
                    command.Parameters.AddWithValue("@Foto", foto);
                    command.ExecuteNonQuery();
                    conexion.Close();
                    resultado = true;
                }
            } catch {

                string error = "";
            }
          
            return resultado;
        }

    }
}
