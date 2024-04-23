using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prueba.Core.DTOs;
using Prueba.Core.Interfaces;
using Prueba.Core.Responses;
using System.Threading.Tasks;

namespace PruebaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : Controller
    {
        readonly IOrderServices orderServices;

        public OrderController(IOrderServices _orderServices)
        {
            orderServices = _orderServices;
        }

        /// <summary>
        /// Servicio para obtener un pedido por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(OrderController.GetOrder))]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> GetOrder(int id)
        {
            return await Task.Run(() =>
            {
                ResponseQuery<OrderDto> response = new ResponseQuery<OrderDto>();
                orderServices.GetOrder(id, response);
                return Ok(response);
            });
        }

        /// <summary>
        /// Servicio para crear un pedido
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(OrderController.CreateOrder))]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> CreateOrder(OrderDto request)
        {
            return await Task.Run(() =>
            {
                ResponseQuery<OrderDto> response = new ResponseQuery<OrderDto>();
                orderServices.CreateOrder(request, response);
                return Ok(response);
            });
        }

        /// <summary>
        /// Servicio para crear un pedido junto con los productos asociados
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(OrderController.CreateOrderProducts))]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> CreateOrderProducts(OrderProductsDto request)
        {
            return await Task.Run(() =>
            {
                ResponseQuery<OrderDto> response = new ResponseQuery<OrderDto>();
                orderServices.CreateOrderProducts(request, response);
                return Ok(response);
            });
        }

        /// <summary>
        /// Servicio para enviar Email de confirmación mediante el Pedido Completo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(OrderController.SendMail))]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> SendMail(OrderDto request)
        {
            return await Task.Run(() =>
            {
                ResponseQuery<OrderDto> response = new ResponseQuery<OrderDto>();
                orderServices.SendMail(request, response);
                return Ok(response);
            });
        }

        /// <summary>
        /// Servicio para enviar Email de confirmación mediante el ID del Pedido
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(OrderController.SendMailById))]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> SendMailById(int id)
        {
            return await Task.Run(() =>
            {
                ResponseQuery<OrderDto> response = new ResponseQuery<OrderDto>();
                orderServices.SendMailById(id, response);
                return Ok(response);
            });
        }

        /// <summary>
        /// Servicio para Activar o desactivar un Pedido
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route(nameof(OrderController.ActivateOrder))]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ActivateOrder(int id)
        {
            return await Task.Run(() =>
            {
                ResponseQuery<OrderDto> response = new ResponseQuery<OrderDto>();
                orderServices.ActivateOrder(id, response);
                return Ok(response);
            });
        }

    }
}
