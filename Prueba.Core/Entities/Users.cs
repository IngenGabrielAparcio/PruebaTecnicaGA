using System;
using System.Collections.Generic;

#nullable disable

namespace Prueba.Core.Entities
{
    public partial class Users
    {
        public int Id { get; set; }
        public string UserStore { get; set; }
        public string Pass { get; set; }
        public string Email { get; set; }        
        public string Role { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
