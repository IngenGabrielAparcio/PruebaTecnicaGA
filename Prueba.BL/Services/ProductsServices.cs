using Prueba.Core.DTOs;
using Prueba.Core.Interfaces;
using Prueba.Core.Responses;
using System;

namespace Prueba.BL.Services
{
    public class ProductsServices : IProductsServices
    {
        private readonly IProductsDataAccess productsDataAccess;
        private readonly ICatalogDataAccess catalogDataAccess;
        private readonly IOrderDataAccess orderDataAccess;
        public ProductsServices(IProductsDataAccess _productsDataAccess, ICatalogDataAccess _catalogDataAccess, IOrderDataAccess _orderDataAccess)
        {
            productsDataAccess = _productsDataAccess;
            catalogDataAccess = _catalogDataAccess;
            orderDataAccess = _orderDataAccess;
        }

        public ResponseQuery<ProductsDto> CreateProducts(ProductsDto request, ResponseQuery<ProductsDto> response)
        {
            try
            {
                var orderActive = getOrderActive((int)request.OrderId);
                if(orderActive == true)
                {
                    var inStorage = GetProductById(request);
                    if (inStorage)
                    {
                        response.Result = productsDataAccess.CreateProducts(request);
                        response.Exitosos = true;
                        response.Mensaje = "Product successfully Created";
                        var price = GetProductsByOrder((int)request.OrderId);
                        UpdateOrder(price, (int)request.OrderId);
                        UpdateCatalog(request);
                    }
                    else
                    {
                        response.Result = new ProductsDto();
                        response.Mensaje = "Not enough products in storage";
                        response.Exitosos = false;
                    }
                }
                else {
                    response.Result = new ProductsDto();
                    response.Mensaje = "The order is disabled, not possible to create register";
                    response.Exitosos = false;
                }
                              
            }
            catch (Exception ex)
            {
                response.Result = new ProductsDto();
                response.Mensaje = ex.Message;
                response.Exitosos = false;
            }
            return response;
        }

        public ResponseQuery<ProductsDto> EditProducts(ProductsDto request, ResponseQuery<ProductsDto> response)
        {
            try
            {
                response.Result = productsDataAccess.EditProducts(request);
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
                response.Result = new ProductsDto();
                response.Mensaje = ex.Message;
                response.Exitosos = false;
            }
            return response;
        }

        public ResponseQuery<ProductsDto> DeleteProducts(int id, ResponseQuery<ProductsDto> response)
        {
            try
            {                
                response.Result = productsDataAccess.DeleteProducts(id);                
                if (response.Result != null)
                {
                    response.Mensaje = "Record Deleted updated";
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
                response.Result = new ProductsDto();
                response.Mensaje = ex.Message;
                response.Exitosos = false;
            }
            return response;
        }

        public double? GetProductsByOrder(int orderid)
        {          
                double? total = 0;
                var products = productsDataAccess.GetProductsByOrder(orderid);
                foreach (var item in products)
                {
                    total = total + item.Price;
                }     
                return total;            
        }

        private void UpdateOrder(double? price, int orderId)
        {
            var order = new OrderDto();
            order.Total = price;
            order.Id = orderId;
            orderDataAccess.UpdateOrder(order);
        }

        private void UpdateCatalog(ProductsDto request)
        {
            var catalogExist = catalogDataAccess.GetCatalog((int)request.ProductId);
            var catalogtemp = new CatalogDto();
            catalogtemp.Id = (int)request.ProductId;
            catalogtemp.Quantity = catalogExist.Quantity - request.Quantity;
            catalogDataAccess.UpdateCatalog(catalogtemp);
        }

        private bool GetProductById(ProductsDto request)
        {
            var flag = false;
            var storage = productsDataAccess.GetProductsById((int)request.ProductId);

            if (storage.Quantity - request.Quantity >= 0)
            {
                flag = true;
            }
            return flag;
        }

        private bool? getOrderActive(int orderid)
        {            
            bool? flag = orderDataAccess.GetOrder(orderid).Active;
            return flag;
        }
    }
}
