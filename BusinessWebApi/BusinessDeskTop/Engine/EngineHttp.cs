using BusinessDeskTop.Engine.Interfaces;

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
        private string cadenaConexion = System.Configuration.ConfigurationManager.ConnectionStrings["CNX_DB"].ToString();
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

        public bool  UploadPersonToApiUpdate(List<Person> persons)
        {
            bool resultado = false;
            SqlConnection Conexion = new SqlConnection(cadenaConexion);
            Conexion.Open();
            foreach (Person p in persons)
            {
                try {
                    SqlCommand command = new SqlCommand("Sp_InsertUpdatePerson", Conexion);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Nombre", p.Nombre);
                    command.Parameters.AddWithValue("@Apellido", p.Apellido);
                    command.Parameters.AddWithValue("@Dni", p.Dni);
                    command.Parameters.AddWithValue("@Matricula", p.Matricula);
                    command.Parameters.AddWithValue("@Rh", p.Rh);
                    command.Parameters.AddWithValue("@Grado", p.Grado);
                    command.Parameters.AddWithValue("@Grupo", p.Grupo);
                    command.Parameters.AddWithValue("@Email", p.Email);
                    command.Parameters.AddWithValue("@IdCompany", p.IdCompany);
                    command.Parameters.AddWithValue("@Company", p.Company);
                    command.Parameters.AddWithValue("@Date", DateTime.UtcNow);
                    command.Parameters.AddWithValue("@Status", p.Status);
                    command.Parameters.AddWithValue("@Foto", p.Foto);
                    command.Parameters.AddWithValue("@Qr", p.Qr);
                    command.Parameters.AddWithValue("@Turno", p.Turno);
                    command.ExecuteNonQuery();
                    resultado = true;
                }
                catch (Exception ex)
                {
                    string errr = ex.ToString();
                }     
            }
            Conexion.Close();
            return resultado;
        }


        public bool UploadPersonToApi(List<Person> persons,IEngineProject project)
        {
            DataTable dt = project.BuildDtPerson();
            int turn = 1;
            foreach(Person p in persons)
            {
                turn = p.Turno;
                dt.Rows.Add(p.Nombre,p.Apellido,p.Dni,p.Matricula,p.Rh,p.Grado,p.Grupo,p.Email,0,p.Company,DateTime.UtcNow,true,p.Foto,p.Qr,p.Turno);
            }
            bool resultado = false;
            SqlConnection Conexion = new SqlConnection(cadenaConexion); 
            Conexion.Open();
            using (var bcp = new SqlBulkCopy(Conexion))
            {
                bcp.BulkCopyTimeout = 100;
                bcp.BatchSize = 10000;
                bcp.DestinationTableName = "Person";
                SqlBulkCopyColumnMapping nombre  = new SqlBulkCopyColumnMapping("Nombre", "Nombre");
                bcp.ColumnMappings.Add(0, "Nombre");
                SqlBulkCopyColumnMapping apellido= new SqlBulkCopyColumnMapping("Apellido","Apellido");
                bcp.ColumnMappings.Add(1, "Apellido");
                SqlBulkCopyColumnMapping dni = new SqlBulkCopyColumnMapping("Dni", "Dni");
                bcp.ColumnMappings.Add(2, "Dni");
                SqlBulkCopyColumnMapping matricula = new SqlBulkCopyColumnMapping("Matricula", "Matricula");
                bcp.ColumnMappings.Add(3, "Matricula");
                SqlBulkCopyColumnMapping rh = new SqlBulkCopyColumnMapping("Rh","Rh");
                bcp.ColumnMappings.Add(4, "Rh");
                SqlBulkCopyColumnMapping grado = new SqlBulkCopyColumnMapping("Grado", "Grado");
                bcp.ColumnMappings.Add(05, "Grado");
                SqlBulkCopyColumnMapping grupo = new SqlBulkCopyColumnMapping("Grupo", "Grupo");
                bcp.ColumnMappings.Add(6, "Grupo");
                SqlBulkCopyColumnMapping email = new SqlBulkCopyColumnMapping("Email", "Email");
                bcp.ColumnMappings.Add(7, "Email");
                SqlBulkCopyColumnMapping idCompany = new SqlBulkCopyColumnMapping("IdCompany", "IdCompany");
                bcp.ColumnMappings.Add(8, "IdCompany");
                SqlBulkCopyColumnMapping company= new SqlBulkCopyColumnMapping("Company", "Company");
                bcp.ColumnMappings.Add(9, "Company");
                SqlBulkCopyColumnMapping date = new SqlBulkCopyColumnMapping("Date", "Date");
                bcp.ColumnMappings.Add(10, "Date");
                SqlBulkCopyColumnMapping status = new SqlBulkCopyColumnMapping("Status","Status");
                bcp.ColumnMappings.Add(11, "Status");
                SqlBulkCopyColumnMapping foto = new SqlBulkCopyColumnMapping("Foto", "Foto");
                bcp.ColumnMappings.Add(12, "Foto");
                SqlBulkCopyColumnMapping qr = new SqlBulkCopyColumnMapping("Qr", "Qr");
                bcp.ColumnMappings.Add(13, "Qr");
                SqlBulkCopyColumnMapping turno = new SqlBulkCopyColumnMapping("Turno", "Turno");
                bcp.ColumnMappings.Add(14, "Turno");
                bcp.WriteToServer(dt);
                resultado = true;
            }
            Conexion.Close();
            return resultado;
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

    }
}
