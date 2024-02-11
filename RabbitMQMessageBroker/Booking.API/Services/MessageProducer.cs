using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Booking.API.Services
{
    public class MessageProducer: IMessageProducer
    {
        private readonly IConfiguration _configuration;
        private readonly IRabbitMqConnectionService service;
        private readonly IModel channel;

        public MessageProducer(IRabbitMqConnectionService service)
        {
            // _configuration = configuration;
            this.service = service;
            channel = this.service.CreateChannel();
        }
        public void PublishMessage<T>(T message)
        {
            // var factory = new ConnectionFactory()
            // {
            //     HostName = _configuration.GetValue<string>("RabbitMQConnectionString:Hostname"),
            //     UserName = _configuration.GetValue<string>("RabbitMQConnectionString:UserName"),
            //     Password = _configuration.GetValue<string>("RabbitMQConnectionString:Password"),
            //     VirtualHost = _configuration.GetValue<string>("RabbitMQConnectionString:VirtualHost"),
            // };
            // var conn = factory.CreateConnection();
            // using var channel = conn.CreateModel();
            channel.QueueDeclare("booking", durable: true, exclusive: false, false, null);
            var jsonObject = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonObject);
            channel.BasicPublish("", "booking", null, body:body);
        }
    }
}