using Prueba.Core.DTOs;
using Prueba.Core.Interfaces;
using Prueba.Core.Responses;
using System;
using System.Net;
using System.Net.Mail;

namespace Prueba.BL.Services
{
    public class OrderServices : IOrderServices
        {
            private readonly IOrderDataAccess orderDataAccess;
            private readonly ILoginDataAccess loginDataAccess;
            public OrderServices(IOrderDataAccess _orderDataAccess,
                ILoginDataAccess _loginDataAccess)
            {
                orderDataAccess = _orderDataAccess;
                loginDataAccess = _loginDataAccess;
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
                    var email = SearchUser(request);
                    MailMessage message = new MailMessage();
                    string from = "PruebaTecnicaGA@outlook.es";
                    string smtpCliente = "smtp-mail.outlook.com";
                    string puerto = "587";
                    string usuario = "PruebaTecnicaGA@outlook.es";
                    string clave = "Brinidas1";
                    string correoReporte = email;

                    message.To.Add(new MailAddress(correoReporte));
                    message.To.Add(new MailAddress("ingen.aparicio@gmail.com"));
                    message.From = new MailAddress(from);
                    message.Subject = "Confirmación de Pedido";
                    message.SubjectEncoding = System.Text.Encoding.UTF8;
                    message.Body = "Señor/a usuario, usted ha realizado un pedido por valor de: " + request.Total + " en la fecha: " + request.Date;
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
                var email = SearchUser(request);
                MailMessage message = new MailMessage();
                string from = "PruebaTecnicaGA@outlook.es";
                string smtpCliente = "smtp-mail.outlook.com";
                string puerto = "587";
                string usuario = "PruebaTecnicaGA@outlook.es";
                string clave = "Brinidas1";
                string correoReporte = email;

                message.To.Add(new MailAddress(correoReporte));
                message.To.Add(new MailAddress("ingen.aparicio@gmail.com"));
                message.From = new MailAddress(from);
                message.Subject = "Confirmación de Pedido";
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.Body = "Señor/a usuario, usted ha realizado un pedido por valor de: " + request.Total + " en la fecha: " + request.Date;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                new SmtpClient(smtpCliente)
                {
                    EnableSsl = true,
                    Port = Convert.ToInt32(puerto),
                    Credentials = new NetworkCredential(usuario, clave),
                    UseDefaultCredentials = false,
                }.Send(message);                
            }

            private string SearchUser(OrderDto request)
                {
                    var userResult = loginDataAccess.GetUser((int)request.UserId);
                    return userResult.UserStore;
                }

        }
    
}
