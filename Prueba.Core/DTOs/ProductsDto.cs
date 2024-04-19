
using System;

namespace Prueba.Core.DTOs
{
    public class ProductsDto
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
    }
}
