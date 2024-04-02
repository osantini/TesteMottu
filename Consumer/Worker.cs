using Mottu.Service.Data;
using Mottu.Service.Interfaces;
using Mottu.Service.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json.Serialization;

namespace Consumer
{
    public class Worker : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
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
                    channel.QueueDeclare(queue: "Notifications", durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += async (sender, eventArgs) =>
                    {
                        var body = eventArgs.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        var pedidoMessage = JsonConvert.DeserializeObject<List<Pedidos>>(message);

                        foreach(var pedidos in pedidoMessage)
                        {
                            Console.WriteLine(JsonConvert.SerializeObject(pedidos));
                        }
                    };

                    channel.BasicConsume(queue: "Notifications", autoAck: true, consumer: consumer);
                }
            }
            await Task.Delay(2000, stoppingToken);
        }
    }
}

