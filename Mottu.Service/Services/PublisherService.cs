using Mottu.Service.Interfaces;
using Mottu.Service.Request;
using Mottu.Service.Responses;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Mottu.Service.Services
{
    public class PublisherService
    {
        private readonly IApplicationContext _context;

        public PublisherService(IApplicationContext context)
        {
            _context = context;
        }
        public async Task<BaseResponse> PublisherNotification(NotificationRequest notificationRequest)
        {
            BaseResponse ret = new BaseResponse();
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = "localhost",
                    UserName = "guest",
                    Password = "guest"
                };

                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    List<int> entregadorId = _context.AluguelMotos.Where(e => e.Status == "Ativo").Select(e => e.EntregadorId).ToList();
                    foreach(var entregadores in entregadorId)
                    {
                        notificationRequest.EntregadorId = entregadores;
                    }

                    channel.QueueDeclare(queue: "Notifications", durable: false, exclusive: false, autoDelete: false, arguments: null);
                    var message = JsonConvert.SerializeObject(notificationRequest);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "", routingKey: "Notifications", basicProperties: null, body: body);
                }

                ret.StatusCode = System.Net.HttpStatusCode.OK;
                ret.IsSucess = true;
                return ret;
            }
            catch(Exception ex)
            {
                ret.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                ret.IsSucess = false;
                ret.Message = ex.Message;
                return ret;
            }
        }
    }
}
