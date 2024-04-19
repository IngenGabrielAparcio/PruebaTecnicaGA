using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Prueba.Core.DTOs;
using Prueba.Core.Entities;
using Prueba.Core.Interfaces;
using Prueba.Core.Utilities;
using Prueba.infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.infraestructure.Access
{
    public class LoginDataAccess : ILoginDataAccess
    {
        protected DBStoreTestContext context;
        private readonly IMapper mapper;

        public LoginDataAccess(DBStoreTestContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }

        public UserDto Login(string userStore, string pass)
        {
            Users login = new Users();
            login = context.Users.FirstOrDefault(x => x.UserStore == userStore && x.Pass == pass);            
            return mapper.Map<UserDto>(login);

        }

        public UserDto CreateUser(UserDto request)
        {
            var entity = mapper.Map<Users>(request);
            context.Users.Add(entity);
            context.SaveChanges();
            var Result = mapper.Map<UserDto>(entity);
            return Result;
        }

        public UserDto GetUser(int id)
        {
            Users login = new Users();
            login = context.Users.FirstOrDefault(x => x.Id == id);
            return mapper.Map<UserDto>(login);

        }


    }
}
