using Prueba.Core.DTOs;
using Prueba.Core.Interfaces;
using Prueba.Core.Responses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace Prueba.BL.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IOrderDataAccess orderDataAccess;
        private readonly ILoginDataAccess loginDataAccess;
        private readonly IProductsDataAccess productsDataAccess;
        private readonly ICatalogDataAccess catalogDataAccess;
        public OrderServices(IOrderDataAccess _orderDataAccess,
            ILoginDataAccess _loginDataAccess,
            IProductsDataAccess _productsDataAccess,
            ICatalogDataAccess _catalogDataAccess)
        {
            orderDataAccess = _orderDataAccess;
            loginDataAccess = _loginDataAccess;
            productsDataAccess = _productsDataAccess;
            catalogDataAccess = _catalogDataAccess;
        }            

        public ResponseQuery<OrderDto> GetOrder(int id, ResponseQuery<OrderDto> response)
        {
            try
            {
                response.Result = orderDataAccess.GetOrder(id);
                if (response.Result != null)
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
                response.Result = new OrderDto();
                response.Mensaje = ex.Message;
                response.Exitosos = false;
            }
            return response;
        }

        public ResponseQuery<OrderDto> CreateOrder(OrderDto request, ResponseQuery<OrderDto> response)
        {
            try
            {
                response.Result = orderDataAccess.CreateOrder(request);                    
                response.Exitosos = true;
                response.Mensaje = "Order successfully Created";
            }
            catch (Exception ex)
            {
                response.Result = new OrderDto();
                response.Mensaje = ex.Message;
                response.Exitosos = false;
            }
            return response;
        }

        public ResponseQuery<OrderDto> CreateOrderProducts(OrderProductsDto request, ResponseQuery<OrderDto> response)
        {
            try
            {
                var productExist = searchProducts(request.Products);
                if (productExist)
                {
                    response.Result = new OrderDto();
                    response.Mensaje = "Not Enough products in the catalog";
                    response.Exitosos = false;
                    return response;
                }
                var response2 = new ResponseQuery<OrderDto>();
                var totalValue = CalculateTotal(request);
                var newOrder = new OrderDto();
                newOrder.UserId = request.UserId;
                newOrder.Total = totalValue;
                newOrder.Active = request.Active;
                newOrder.Date = request.Date;
                response.Result = orderDataAccess.CreateOrder(newOrder);
                request.Id = response.Result.Id;
                createProductsRange(request);
                UpdateCatalog(request.Products);
                SendMail(newOrder, response2);
                response.Exitosos = true;
                response.Mensaje = "Order successfully Created";
            }
            catch (Exception ex)
            {
                response.Result = new OrderDto();
                response.Mensaje = ex.Message;
                response.Exitosos = false;
            }
            return response;
        }

        public ResponseQuery<OrderDto> ActivateOrder(int id, ResponseQuery<OrderDto> response)
        {
            try
            {
                response.Result = orderDataAccess.ActivateOrder(id);                    
                response.Exitosos = true;
                if ((bool)response.Result.Active)
                {
                    response.Mensaje = "Order Active";
                }
                else
                {
                    response.Mensaje = "Order Disabled";
                }
                    
            }
            catch (Exception ex)
            {
                response.Result = new OrderDto();
                response.Mensaje = ex.Message;
                response.Exitosos = false;
            }
            return response;
        }

        public ResponseQuery<OrderDto> SendMailById(int id, ResponseQuery<OrderDto> response)
        {
            try {
            var orderData = orderDataAccess.GetOrder(id);
            if (orderData != null)
            {
                SendMailByIdComplement(orderData);
                response.Result = new OrderDto();
                response.Mensaje = "Email Sended Correctly";
                response.Exitosos = true;
            }
            else
            {
                response.Mensaje = "Email Error";
                response.Exitosos = false;
            }
        }
            catch (Exception ex)
            {
                response.Result = new OrderDto();
                response.Mensaje = ex.Message;
                response.Exitosos = false;
            }
            return response;
    }

        public ResponseQuery<OrderDto> SendMail(OrderDto request, ResponseQuery<OrderDto> response)
            {
                
            try
            {
                var userResult = SearchUser(request);                                
                MailMessage message = new MailMessage();
                string from = "PruebaTecnicaGA@outlook.es";
                string smtpCliente = "smtp-mail.outlook.com";
                string puerto = "587";
                string usuario = "PruebaTecnicaGA@outlook.es";
                string clave = "Brinidas1";
                string correoReporte = userResult.Email;

                message.To.Add(new MailAddress(correoReporte));
                //message.To.Add(new MailAddress("ingen.aparicio@gmail.com"));
                message.From = new MailAddress(from);
                message.Subject = "Confirmación de Pedido";
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.Body = "Señor/a " + userResult.UserStore + ", usted ha realizado un pedido por valor de: " + request.Total + " en la fecha: " + request.Date;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                new SmtpClient(smtpCliente)
                {
                    EnableSsl = true,
                    Port = Convert.ToInt32(puerto),
                    Credentials = new NetworkCredential(usuario, clave),
                    UseDefaultCredentials = false,
                }.Send(message);
                response.Result = new OrderDto();
                response.Mensaje = "Email succesfully sended";
                response.Exitosos = true;                
            }
            catch (Exception ex)
            {
                response.Result = new OrderDto();
                response.Mensaje = ex.Message;
                response.Exitosos = false;
            }
            return response;
    }

        public void SendMailByIdComplement(OrderDto request)
        {
            var userResult = SearchUser(request);
            MailMessage message = new MailMessage();
            string from = "PruebaTecnicaGA@outlook.es";
            string smtpCliente = "smtp-mail.outlook.com";
            string puerto = "587";
            string usuario = "PruebaTecnicaGA@outlook.es";
            string clave = "Brinidas1";
            string correoReporte = userResult.Email;

            message.To.Add(new MailAddress(correoReporte));
            //message.To.Add(new MailAddress("ingen.aparicio@gmail.com"));
            message.From = new MailAddress(from);
            message.Subject = "Confirmación de Pedido";
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.Body = "Señor/a " + userResult.UserStore + ", usted ha realizado un pedido por valor de: " + request.Total + " en la fecha: " + request.Date;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            new SmtpClient(smtpCliente)
            {
                EnableSsl = true,
                Port = Convert.ToInt32(puerto),
                Credentials = new NetworkCredential(usuario, clave),
                UseDefaultCredentials = false,
            }.Send(message);                
        }

        private UserDto SearchUser(OrderDto request)
            {
                var userResult = loginDataAccess.GetUser((int)request.UserId);
                return userResult;
            }

        private string createProductsRange(OrderProductsDto request)
        {
            try
            {
                foreach (var item in request.Products)
                {
                    item.OrderId = request.Id;
                }
                var products = productsDataAccess.CreateRange(request.Products);
                return products;

            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        private Double CalculateTotal(OrderProductsDto request)
        {
            Double total = 0;
            foreach (var item in request.Products)
            {
                total = total + (double)item.Price;
            }
            return total;
        }

        private void UpdateCatalog(List<ProductsDto> request)
        {
        foreach (var item in request)
            {
                var catalogExist = catalogDataAccess.GetCatalog((int)item.ProductId);
                var catalogtemp = new CatalogDto();
                catalogtemp.Id = (int)item.ProductId;
                catalogtemp.Quantity = catalogExist.Quantity - item.Quantity;
                catalogDataAccess.UpdateCatalog(catalogtemp);
            }
            
        }

        private bool searchProducts(List<ProductsDto> request)
        {
            var flag = false;

            foreach (var item in request) {
                var catalogExist = catalogDataAccess.GetCatalog((int)item.ProductId);
                if (catalogExist.Quantity < item.Quantity) {
                    flag = true;
                }
            }
            return flag;
        }

    }
    
}
