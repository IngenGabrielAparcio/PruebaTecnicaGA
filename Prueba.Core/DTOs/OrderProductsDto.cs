
using Prueba.Core.Entities;
using System;
using System.Collections.Generic;

namespace Prueba.Core.DTOs
{
    public class OrderProductsDto
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public DateTime? Date { get; set; }
        public double? Total { get; set; }
        public bool? Active { get; set; }
        
        public virtual List<ProductsDto> Products { get; set; }
    }
}
