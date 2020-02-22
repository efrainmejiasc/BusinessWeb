using BusinessWebApi.Engine;
using BusinessWebApi.Engine.Interfaces;
using BusinessWebApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using System.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Claim = System.Security.Claims.Claim;
using SecurityToken = Microsoft.IdentityModel.Tokens.SecurityToken;
using SecurityTokenValidationException = Microsoft.IdentityModel.Tokens.SecurityTokenValidationException;
using SymmetricSecurityKey = Microsoft.IdentityModel.Tokens.SymmetricSecurityKey;
using ClaimTypes = System.IdentityModel.Claims.ClaimTypes;
using Newtonsoft.Json;

namespace BusinessWebApi.Controllers
{
    public class UserApiController : ApiController
    {
        private readonly IEngineTool Tool;
        private readonly IEngineDb Metodo;
        public UserApiController(IEngineTool _tool, IEngineDb _metodo)
        {
            Tool = _tool;
            Metodo = _metodo;
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("CreateUser")]
        public HttpResponseMessage CreateUser([FromBody] UserApi user)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.User))
            {
                response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                response.Content = new StringContent(EngineData.modeloImcompleto, Encoding.Unicode);
                return response;
            }

            bool resultado = false;
            resultado = Tool.EmailEsValido(user.Email);
             if (!resultado)
             {
                 response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                 response.Content = new StringContent(EngineData.emailNoValido, Encoding.Unicode);
                 return response;
             }
            resultado = Metodo.CreateUser(user);
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

        [AllowAnonymous]
        [HttpPost]
        [ActionName("Login")]
        public IHttpActionResult Login([FromBody] UserApi login)
        {
            IHttpActionResult response = Unauthorized();
            string password1_64 = Tool.ConvertirBase64(login.Email + login.Password);
            string password2_64 = Tool.ConvertirBase64(login.User + login.Password);
            UserApi user = Metodo.GetUser(password1_64, password2_64);
            if (user == null)
                return response;

            string unicoIdentificador = Guid.NewGuid().ToString();
            int expire = EngineData.ExpireToken;
            DateTime time = DateTime.UtcNow;
            DateTime expireTime = time.AddMinutes(Convert.ToInt32(expire));
            var tokenString = GenerateTokenJwt(user, unicoIdentificador, time, expireTime);
            response = Ok(new
            {
                access_token = tokenString,
                expire_token = "20000",
                type_token = "Bearer",
                refresh_token = unicoIdentificador,
            });

            return response;
        }

        private string GenerateTokenJwt(UserApi user, string unicoIdentificador, DateTime time, DateTime expireTime)
        {
            // appsetting for Token JWT
            var secretKey = EngineData.SecretKey;
            var audienceToken = EngineData.Audience;
            var issuerToken = EngineData.Issuer;

            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(secretKey));
            var signingCredentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, System.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256Signature);

            // create a claimsIdentity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim (ClaimTypes.DateOfBirth, user.CreateDate.ToString()),
                new Claim ("HoraToken", DateTime.UtcNow.ToString()),
                new Claim (ClaimTypes.Anonymous, unicoIdentificador)
            });

            // create token to the user
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: audienceToken,
                issuer: issuerToken,
                subject: claimsIdentity,
                notBefore: time,
                expires: expireTime,
                signingCredentials: signingCredentials);

            string token = tokenHandler.WriteToken(jwtSecurityToken);
            return token;
        }
    }
}
