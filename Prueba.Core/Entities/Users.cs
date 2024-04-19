using System;
using System.Collections.Generic;

#nullable disable

namespace Prueba.Core.Entities
{
    public partial class Users
    {
        public Users()
        {
            Order = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string UserStore { get; set; }
        public string Pass { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
