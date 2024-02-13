using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace Booking.API.Services
{
    public class MessageProducer: IMessageProducer
    {
        private readonly IRabbitMqConnectionService service;
        private readonly IModel channel;

        public MessageProducer(IRabbitMqConnectionService service)
        { 
            this.service = service;
            channel = this.service.CreateChannel();
        }
        public void PublishMessage<T>(T message)
        {
            channel.QueueDeclare("booking", durable: true, exclusive: false, false, null);
            var jsonObject = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonObject);
            channel.BasicPublish("", "booking", null, body:body);
        }
    }
}