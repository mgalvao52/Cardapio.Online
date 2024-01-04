using Cardapio.Application.DTOs;
using Cardapio.Application.Helpers;
using Cardapio.DB.Enums;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Cardapio.Service
{
    public class Simulator : BackgroundService
    {
        private readonly int totalThreads;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceProvider _serviceProvider;
        private readonly DefaultConfiguration _configuration;

        public Simulator(IOptions<DefaultConfiguration> options,IServiceProvider serviceProvider)
        {
            _configuration = options.Value;
            this.totalThreads = _configuration.TotalThreads;
            _serviceProvider = serviceProvider;

            var factory = new ConnectionFactory
            {
                HostName = _configuration.Host,
                Password = _configuration.Password,
                UserName = _configuration.UserName,
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue:_configuration.QueueName,durable:true,exclusive:false,autoDelete:false,arguments:null);
            _channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await InicializeConsumer();
        }

        private async Task InicializeConsumer()
        {
            Console.WriteLine(" waiting for messages");
            bool completed = false;

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (model, ev) =>
            {
                byte[] bytes = ev.Body.ToArray();

                var json = Encoding.UTF8.GetString(bytes);
                Console.WriteLine("received message");

                var order = JsonSerializer.Deserialize<ReadOrderItemDTO>(json);

                if (order != null)
                {
                    await UpdateStatus(order.Id.Value, OrderItemStatus.Preparing);
                    Console.WriteLine($"preparing...{order.MenuName} -  ({order.Time})s");
                    await Task.Delay(TimeSpan.FromSeconds(order.Time));

                    Console.WriteLine("ready.");
                    completed = await UpdateStatus(order.Id.Value,OrderItemStatus.Ready);
                    _channel.BasicAck(deliveryTag: ev.DeliveryTag, multiple: false);
                }

            };

            _channel.BasicConsume(queue:_configuration.QueueName,autoAck:false,consumer:consumer);

        }

        private async Task<bool> UpdateStatus(int id, OrderItemStatus status)
        {
            bool result = false;
            try
            {
                using (var client = new HttpClient()) 
                {
                    client.BaseAddress = new Uri("https://localhost:7254/");

                    var response = await client.PutAsync($"api/orderitem/{id}/{status}",null);
                    result = response.IsSuccessStatusCode;
                }
            }
            catch (Exception)
            {
            }
            return result;
        }
    }
}
