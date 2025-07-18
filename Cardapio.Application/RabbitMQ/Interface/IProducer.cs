namespace Cardapio.Application.RabbitMQ.Interface
{
    public interface IProducer
    {
        public void SendMessage<T>(T message,string queueName);
    }
}
