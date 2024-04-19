using Prueba.Core.DTOs;
using Prueba.Core.Interfaces;
using Prueba.Core.Responses;
using Prueba.infraestructure.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.BL.Services
{
    public class LoginServices : ILoginServices
    {
        private readonly ILoginDataAccess loginDataAccess;
        public LoginServices(ILoginDataAccess _loginDataAccess)
        {
            loginDataAccess = _loginDataAccess;
        }

        public ResponseQuery<UserDto> Login(UserDto request, ResponseQuery<UserDto> response)
        {
            try
            {
                response.Result = loginDataAccess.Login(request.UserStore, request.Pass);
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

    }
}
