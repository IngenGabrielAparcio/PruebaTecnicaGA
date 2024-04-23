using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Prueba.Core.DTOs;
using Prueba.Core.Interfaces;
using Prueba.Core.Responses;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Prueba.BL.Services
{
    public class LoginServices : ILoginServices
    {
        private readonly ILoginDataAccess loginDataAccess;
        private readonly IConfiguration configuration;
        public LoginServices(ILoginDataAccess _loginDataAccess, IConfiguration _configuration)
        {
            loginDataAccess = _loginDataAccess;
            configuration = _configuration;
        }

        public ResponseQuery<UserDto> Login(UserDto request, ResponseQuery<UserDto> response)
        {
            try
            {
                response.Result = loginDataAccess.Login(request.Email, request.Pass);
                response.Result.token = GenerateToken(response.Result);
                if (response.Result != null)
                {
                    response.Mensaje = "User Logged Correctly";
                    response.Exitosos = true;
                }
                else
                {
                    response.Mensaje = "User not found";
                    response.Exitosos = false;
                }
            }
            catch (Exception ex)
            {
                response.Result = new UserDto();
                response.Mensaje = ex.Message;
                response.Exitosos = false;
            }
            return response;
        }

        public ResponseQuery<UserDto> CreateUser(UserDto request, ResponseQuery<UserDto> response)
        {
            try
            {
                response.Result = loginDataAccess.CreateUser(request);
                response.Exitosos = true;
                response.Mensaje = "User successfully Created";
            }
            catch (Exception ex)
            {
                response.Result = new UserDto();
                response.Mensaje = ex.Message;
                response.Exitosos = false;
            }
            return response;
        }

        private string GenerateToken(UserDto user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWT:Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: creds );

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }

    }
}
