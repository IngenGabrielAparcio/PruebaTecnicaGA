using System;
using System.Collections.Generic;

#nullable disable

namespace Prueba.Core.Entities
{
    public partial class Order
    {
        public Order()
        {
            Products = new HashSet<Products>();
        }

        public int Id { get; set; }
        public int? UserId { get; set; }
        public DateTime? Date { get; set; }
        public double? Total { get; set; }
        public bool? Active { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<Products> Products { get; set; }
    }
}
