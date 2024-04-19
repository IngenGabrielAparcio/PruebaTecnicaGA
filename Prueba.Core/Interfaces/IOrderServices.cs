
using Prueba.Core.DTOs;
using Prueba.Core.Responses;
using System.Collections.Generic;

namespace Prueba.Core.Interfaces
{
    public interface IOrderServices
    {                

        public ResponseQuery<OrderDto> GetOrder(int id, ResponseQuery<OrderDto> response);

        public ResponseQuery<OrderDto> CreateOrder(OrderDto request, ResponseQuery<OrderDto> response);

        public ResponseQuery<OrderDto> SendMail(OrderDto request, ResponseQuery<OrderDto> response);

        public ResponseQuery<OrderDto> SendMailById(int id, ResponseQuery<OrderDto> response);

        public ResponseQuery<OrderDto> ActivateOrder(int id, ResponseQuery<OrderDto> response);

    }
}
