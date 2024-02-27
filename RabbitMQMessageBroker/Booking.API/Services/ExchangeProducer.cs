using System.Collections.Generic;
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
            channel.ExchangeDeclare("fanout.exchange",type:ExchangeType.Fanout, durable:true, autoDelete:false,null);
            var jsonObject = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonObject);
            channel.BasicPublish(exchange:"fanout.exchange", routingKey:string.Empty, null, body:body);
        }
        
        public void PublishTopicExchangeMessage<T>(T message)
        {
            var queue = "topic.booking";
            var exchange = "topic.exchange";
            var routingKey = "topic.booking.*";// will match exactly one word
            //var routingKey = "topic.booking.*";// Matches zero or more words. image.# will match image.jpg and image.bitmap.32bit but not convert.image
            
            channel.ExchangeDeclare(exchange: exchange,type: ExchangeType.Topic, durable:true, autoDelete:false,null);
            channel.QueueDeclare(queue: queue, durable: true, exclusive: false, false, null);
            channel.QueueBind(queue:queue, exchange:exchange, routingKey: routingKey,arguments:null);
            var jsonObject = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonObject);
            channel.BasicPublish(exchange:exchange, routingKey:routingKey, null, body:body);
        }
        
        public void PublishHeadersExchangeMessage<T>(T message)
        {
            var queue = "headers.booking";
            var exchange = "headers.exchange";
            var routingKey = "headers.booking.key";
            var headers =  new Dictionary<string, object>
            {
                {"x-match", "all"},
                {"job", "format"},
                {"type", "mp4"},
            };

            channel.ExchangeDeclare(exchange: exchange,type: ExchangeType.Headers, durable:true, autoDelete:false,null);
            channel.QueueDeclare(queue:queue, durable: true, exclusive: false, false, null);
            channel.QueueBind(queue:queue, exchange: exchange, routingKey:string.Empty, arguments: headers);
            
            // This will add headers with the message body
            IBasicProperties props = channel.CreateBasicProperties();
            props.Headers = new Dictionary<string, object>();
            props.Headers.Add("x-match","all");
            props.Headers.Add("job","format");
            props.Headers.Add("type","mp4");
            
            var jsonObject = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonObject);
            channel.BasicPublish(exchange:exchange, routingKey:routingKey, basicProperties: props, body:body);
        }
        
        public void PublishDirectExchangeMessage<T>(T message)
        {
            var queue = "direct.booking";
            var exchange = "direct.exchange";
            var routingKey = "direct.booking.key";

            channel.ExchangeDeclare(exchange:exchange,type:ExchangeType.Direct, durable:true, autoDelete:false,null);
            channel.QueueDeclare(queue: queue, durable: true, exclusive: false, false, null);
            channel.QueueBind(queue: queue, exchange:exchange, routingKey: routingKey, arguments: null);
            var jsonObject = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonObject);
            channel.BasicPublish(exchange:exchange, routingKey: routingKey, null, body:body);
        }
    }
}