using System;
using System.Collections.Generic;

#nullable disable

namespace Prueba.Core.Entities
{
    public partial class Catalog
    {
        public Catalog()
        {
            Products = new HashSet<Products>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}
