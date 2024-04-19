
using Microsoft.AspNetCore.Mvc;
using Prueba.Core.DTOs;
using Prueba.Core.Interfaces;
using Prueba.Core.Responses;
using System.Threading.Tasks;

namespace PruebaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        readonly IProductsServices productsServices;

        public ProductsController(IProductsServices _productsServices)
        {
            productsServices = _productsServices;
        }        

        /// <summary>
        /// Servicio para agregar productos al pedido
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(ProductsController.CreateProducts))]
        public async Task<IActionResult> CreateProducts(ProductsDto request)
        {
            return await Task.Run(() =>
            {
                ResponseQuery<ProductsDto> response = new ResponseQuery<ProductsDto>();
                productsServices.CreateProducts(request, response);
                return Ok(response);
            });
        }

        /// <summary>
        /// Servicios para actualizar los productos asociados a un pedido
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route(nameof(ProductsController.UpdateProducts))]
        public async Task<IActionResult> UpdateProducts(ProductsDto request)
        {
            return await Task.Run(() =>
            {
                ResponseQuery<ProductsDto> response = new ResponseQuery<ProductsDto>();
                productsServices.EditProducts(request, response);
                return Ok(response);
            });
        }

        /// <summary>
        /// Servicio para borrar productos de un pedido
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route(nameof(ProductsController.DeleteProducts))]
        public async Task<IActionResult> DeleteProducts(int id)
        {
            return await Task.Run(() =>
            {
                ResponseQuery<ProductsDto> response = new ResponseQuery<ProductsDto>();
                productsServices.DeleteProducts(id, response);
                return Ok(response);
            });
        }

    }
}
