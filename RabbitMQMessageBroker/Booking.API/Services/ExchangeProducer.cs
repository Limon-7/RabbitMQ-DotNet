using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace Booking.API.Services
{
    public class ExchangeProducer
    {
        private readonly IRabbitMqConnectionService service;
        private readonly IModel channel;

        public ExchangeProducer(IRabbitMqConnectionService service)
        { 
            this.service = service;
            channel = this.service.CreateChannel();
        }
        public void PublishFanOutExchangeMessage<T>(T message)
        {
            channel.ExchangeDeclare("fanout.exchange",type:"fanout", durable:true, autoDelete:false,null);
            channel.QueueDeclare("fanout.booking", durable: true, exclusive: false, false, null);
            var jsonObject = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonObject);
            channel.BasicPublish(exchange:"fanout.exchange", "fanout.booking.key", null, body:body);
        }
        
        public void PublishTopicExchangeMessage<T>(T message)
        {
            channel.ExchangeDeclare("topic.exchange",type:"topic", durable:true, autoDelete:false,null);
            channel.QueueDeclare("topic.booking", durable: true, exclusive: false, false, null);
            var jsonObject = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonObject);
            channel.BasicPublish(exchange:"topic.exchange", "topic.booking.key", null, body:body);
        }
        
        public void PublishRoutingExchangeMessage<T>(T message)
        {
            channel.ExchangeDeclare("topic.exchange",type:"topic", durable:true, autoDelete:false,null);
            channel.QueueDeclare("topic.booking", durable: true, exclusive: false, false, null);
            var jsonObject = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonObject);
            channel.BasicPublish(exchange:"topic.exchange", "topic.booking.key", null, body:body);
        }
        
        public void PublishDirectExchangeMessage<T>(T message)
        {
            channel.ExchangeDeclare("direct.exchange",type:"direct", durable:true, autoDelete:false,null);
            channel.QueueDeclare("direct.booking", durable: true, exclusive: false, false, null);
            var jsonObject = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonObject);
            channel.BasicPublish(exchange:"direct.exchange", "direct.booking.key", null, body:body);
        }
    }
}