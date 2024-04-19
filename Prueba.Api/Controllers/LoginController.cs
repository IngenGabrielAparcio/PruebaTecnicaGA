
using Microsoft.AspNetCore.Mvc;
using Prueba.Core.DTOs;
using Prueba.Core.Interfaces;
using Prueba.Core.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        readonly ILoginServices loginServices;

        public LoginController(ILoginServices _loginServices)
        {
            loginServices = _loginServices;
        }        

        /// <summary>
        /// Servicio para hacer login de usuario
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(LoginController.Login))]
        public async Task<IActionResult> Login(UserDto request)
        {
            return await Task.Run(() =>
            {
                ResponseQuery<UserDto> response = new ResponseQuery<UserDto>();
                loginServices.Login(request, response);
                return Ok(response);
            });
        }

        /// <summary>
        /// Servicio para crear usuarios
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(LoginController.CreateUser))]
        public async Task<IActionResult> CreateUser(UserDto request)
        {
            return await Task.Run(() =>
            {
                ResponseQuery<UserDto> response = new ResponseQuery<UserDto>();
                loginServices.CreateUser(request, response);
                return Ok(response);
            });
        }


    }
}
