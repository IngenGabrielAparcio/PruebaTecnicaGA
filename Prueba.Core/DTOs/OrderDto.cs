
using System;

namespace Prueba.Core.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public DateTime? Date { get; set; }
        public double? Total { get; set; }
        public bool? Active { get; set; }
    }
}
