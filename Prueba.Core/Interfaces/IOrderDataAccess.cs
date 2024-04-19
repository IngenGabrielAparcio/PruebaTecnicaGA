using Prueba.Core.DTOs;
using System.Collections.Generic;

namespace Prueba.Core.Interfaces
{
    public interface IOrderDataAccess
    {                

        public OrderDto GetOrder(int id);

        public OrderDto CreateOrder(OrderDto request);

        public OrderDto UpdateOrder(OrderDto request);

        public OrderDto ActivateOrder(int id);        

    }
}
