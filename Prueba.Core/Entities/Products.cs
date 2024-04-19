using System;
using System.Collections.Generic;

#nullable disable

namespace Prueba.Core.Entities
{
    public partial class Products
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }

        public virtual Order Order { get; set; }
        public virtual Catalog Product { get; set; }
    }
}
