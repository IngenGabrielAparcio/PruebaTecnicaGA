using Microsoft.AspNetCore.Mvc;
using Prueba.Core.DTOs;
using Prueba.Core.Interfaces;
using Prueba.Core.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : Controller
    {
        readonly ICatalogServices catalogServices;

        public CatalogController(ICatalogServices _catalogServices)
        {
            catalogServices = _catalogServices;
        }
        /// <summary>
        /// Servicio que consulta el producto/alimento del catálogo por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(CatalogController.GetCatalog))]
        public async Task<ResponseQuery<CatalogDto>> GetCatalog(int id)
        {
            return await Task.Run(() =>
            {
                ResponseQuery<CatalogDto> response = new ResponseQuery<CatalogDto>();
                return catalogServices.GetCatalog(id, response);
            });

        }

        /// <summary>
        /// Servicio que obtiene el listado de productos en el catálogo
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(CatalogController.GetListCatalog))]
        public async Task<IActionResult> GetListCatalog()
        {
            return await Task.Run(() =>
            {
                ResponseQuery<List<CatalogDto>> response = new ResponseQuery<List<CatalogDto>>();
                catalogServices.GetListCatalog(response);
                return Ok(response);

            });
        }

        /// <summary>
        /// Servicio para crear productos
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(CatalogController.CreateCatalog))]
        public async Task<IActionResult> CreateCatalog(CatalogDto request)
        {
            return await Task.Run(() =>
            {
                ResponseQuery<CatalogDto> response = new ResponseQuery<CatalogDto>();
                catalogServices.CreateCatalog(request, response);
                return Ok(response);
            });
        }

        /// <summary>
        /// Servicio para actualizar productos
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route(nameof(CatalogController.UpdateCatalog))]
        public async Task<IActionResult> UpdateCatalog(CatalogDto request)
        {
            return await Task.Run(() =>
            {
                ResponseQuery<CatalogDto> response = new ResponseQuery<CatalogDto>();
                catalogServices.UpdateCatalog(request, response);
                return Ok(response);
            });
        }

        /// <summary>
        /// Servicio para borrar productos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route(nameof(CatalogController.DeleteCatalog))]
        public async Task<IActionResult> DeleteCatalog(int id)
        {
            return await Task.Run(() =>
            {
                ResponseQuery<CatalogDto> response = new ResponseQuery<CatalogDto>();
                catalogServices.DeleteCatalog(id, response);
                return Ok(response);
            });
        }

    }
}
