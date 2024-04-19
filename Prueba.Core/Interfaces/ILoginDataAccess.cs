using Prueba.Core.DTOs;
using Prueba.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Core.Interfaces
{
    public interface ILoginDataAccess
    {

        public UserDto Login(string userStore, string pass);

        public UserDto CreateUser(UserDto request);

        public UserDto GetUser(int id);

    }
}
