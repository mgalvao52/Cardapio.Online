using Cardapio.Application.RabbitMQ.Interface;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Cardapio.Application.RabbitMQ
{
    public class Producer : IProducer
    {
        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            // cria a conexão com rabbitMQ
            var connection = factory.CreateConnection();

            // cria o canal da conexão
            using var channel = connection.CreateModel();

            // declara a fila duravel e aguarda confirmação do consumidor para deletar.
            channel.QueueDeclare(queue:"order",durable:true, exclusive: false,autoDelete:false,arguments:null);

            var json = JsonSerializer.Serialize(message);

            var body = Encoding.UTF8.GetBytes(json);    

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            // determina que deve enviar 1 mesnagem por vez para cada consumidor
            // e espera-lo terminar o processo antes de mandar outra mensage
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            channel.BasicPublish(exchange:string.Empty,routingKey:"order",basicProperties:properties,body:body);
        }
    }
}
