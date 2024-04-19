using AutoMapper;
using Prueba.Core.DTOs;
using Prueba.Core.Entities;
using Prueba.Core.Interfaces;
using Prueba.Core.Utilities;
using Prueba.infraestructure.Data;
using System.Linq;

namespace Prueba.infraestructure.Access
{
    public class OrderDataAccess : IOrderDataAccess
    {
        protected DBStoreTestContext context;
        private readonly IMapper mapper;

        public OrderDataAccess(DBStoreTestContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        
        public OrderDto GetOrder(int id)
        {
            var entidad = context.Order.FirstOrDefault(x => x.Id == id);
            return mapper.Map<OrderDto>(entidad);

        }

        public OrderDto CreateOrder(OrderDto request)
        {
            var entity = mapper.Map<Order>(request);
            context.Order.Add(entity);
            context.SaveChanges();
            var Result = mapper.Map<OrderDto>(entity);
            return Result;
        }

        public OrderDto ActivateOrder(int id)
        {
            var order = context.Order.FirstOrDefault(x => x.Id == id);
            Order newOrder = new Order();
            newOrder = order;
            if (order != null)
            {
                newOrder.Active = !order.Active;
                // Campos a actualizar
                FrameworkTypeUtility.SetProperties(newOrder, order);

                // Guardar cambios
                context.SaveChanges();
                var HotelResult = mapper.Map<OrderDto>(newOrder);
                return HotelResult;
            }
            else
            {
                return new OrderDto();
            }
        }

        public OrderDto UpdateOrder(OrderDto request)
        {
            var order = context.Order.FirstOrDefault(x => x.Id == request.Id);
            
            if (order != null)
            {                
                // Campos a actualizar
                FrameworkTypeUtility.SetProperties(request, order);

                // Guardar cambios
                context.SaveChanges();
                var HotelResult = mapper.Map<OrderDto>(request);
                return HotelResult;
            }
            else
            {
                return new OrderDto();
            }
        }

    }
}
