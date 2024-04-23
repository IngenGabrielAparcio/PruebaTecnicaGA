
using System;

namespace Prueba.Core.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserStore { get; set; }
        public string Pass { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        // extended
        public string token { get; set; }

    }
}
