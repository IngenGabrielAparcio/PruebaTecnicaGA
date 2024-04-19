
using Prueba.Core.DTOs;
using Prueba.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Core.Interfaces
{
    public interface ILoginServices
    {        

        public ResponseQuery<UserDto> Login(UserDto request, ResponseQuery<UserDto> response);

        public ResponseQuery<UserDto> CreateUser(UserDto request, ResponseQuery<UserDto> response);

    }
}
