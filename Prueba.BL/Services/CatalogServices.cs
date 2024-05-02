using Prueba.Core.DTOs;
using Prueba.Core.Interfaces;
using Prueba.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.BL.Services
{
    public class CatalogServices : ICatalogServices
    {
        private readonly ICatalogDataAccess catalogDataAccess;
        public CatalogServices(ICatalogDataAccess _catalogDataAccess)
        {
            catalogDataAccess = _catalogDataAccess;
        }

        public ResponseQuery<CatalogDto> GetCatalog(int id, ResponseQuery<CatalogDto> response)
        {
            try
            {
                response.Result = catalogDataAccess.GetCatalog(id);
                if(response.Result != null)
                {
                    response.Mensaje = "record Consulted correctly";
                    response.Exitosos = true;
                }
                else
                {
                    response.Mensaje = "Element not found";
                    response.Exitosos = false;
                }
                
            }
            catch (Exception ex)
            {                
                response.Result = new CatalogDto();
                response.Mensaje = ex.Message;
                response.Exitosos = false;
            }
            return response;
        }

        public ResponseQuery<List<CatalogDto>> GetListCatalog(ResponseQuery<List<CatalogDto>> response)
        {
            try
            {
                response.Result = catalogDataAccess.GetListCatalog();
                if (response.Result.Count != 0)
                {
                    response.Mensaje = "Records Consulted Correctly";
                    response.Exitosos = true;
                }
                else
                {
                    response.Mensaje = "Elements not found";
                    response.Exitosos = false;
                }
            }
            catch (Exception ex)
            {
                response.Result = new List<CatalogDto>();
                response.Mensaje = ex.Message;
                response.Exitosos = false;
            }
            return response;
        }

        public ResponseQuery<CatalogDto> CreateCatalog(CatalogDto request, ResponseQuery<CatalogDto> response)
        {
            try
            {
                if (request.Quantity <= 0)
                {
                    response.Result = new CatalogDto();
                    response.Mensaje = "Cannot create a product with a quantity of 0 or negative";
                    response.Exitosos = false;
                    return response;
                }
                var catalogtemp = getCatalogByName(request.Name);
                
                if (catalogtemp != null)
                {
                    request.Quantity = request.Quantity + catalogtemp.Quantity;
                    request.Id = catalogtemp.Id;                    
                    response.Result = catalogDataAccess.UpdateCatalog(request);
                    response.Exitosos = true;
                    response.Mensaje = "Product successfully Updated";
                }
                else
                {
                    response.Result = catalogDataAccess.CreateCatalog(request);
                    response.Exitosos = true;
                    response.Mensaje = "Product successfully Created";
                }
            }
            catch (Exception ex)
            {
                response.Result = new CatalogDto();
                response.Mensaje = ex.Message;
                response.Exitosos = false;
            }
            return response;
        }

        public ResponseQuery<CatalogDto> UpdateCatalog(CatalogDto request, ResponseQuery<CatalogDto> response)
        {
            try
            {
                if (request.Quantity <= 0)
                {
                    response.Result = new CatalogDto();
                    response.Mensaje = "Cannot update a product with a quantity of 0 or negative";
                    response.Exitosos = false;
                    return response;
                }

                response.Result = catalogDataAccess.UpdateCatalog(request);
                if (response.Result != null)
                {
                    response.Mensaje = "Record successfully updated";
                    response.Exitosos = true;
                }
                else
                {
                    response.Mensaje = "The record doesn't exist";
                    response.Exitosos = false;
                }
            }
            catch (Exception ex)
            {
                response.Result = new CatalogDto();
                response.Mensaje = ex.Message;
                response.Exitosos = false;
            }
            return response;
        }

        public ResponseQuery<CatalogDto> DeleteCatalog(int id, ResponseQuery<CatalogDto> response)
        {
            try
            {                
                response.Result = catalogDataAccess.DeleteCatalog(id);
                if (response.Result != null)
                {
                    response.Mensaje = "Record Deleted Successfully";
                    response.Exitosos = true;
                }
                else
                {
                    response.Mensaje = "The record doesn't exist";
                    response.Exitosos = false;
                }
            }
            catch (Exception ex)
            {
                response.Result = new CatalogDto();
                response.Mensaje = ex.Message;
                response.Exitosos = false;
            }
            return response;
        }

        private CatalogDto getCatalogByName(string name)
        {            
            var catalogtemp = catalogDataAccess.GetCatalogByName(name);            
            return catalogtemp;
        }

    }
}
