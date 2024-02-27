using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Booking.API.Services
{
    public class RabbitMqConnectionService: IRabbitMqConnectionService
    {
        private readonly IConfiguration _configuration;

        private readonly IConnection connection;

        public RabbitMqConnectionService(IConfiguration configuration)
        {
            _configuration = configuration;
             var factory = new ConnectionFactory()
            {
                HostName = _configuration.GetValue<string>("RabbitMQConnectionString:Hostname"),
                UserName = _configuration.GetValue<string>("RabbitMQConnectionString:UserName"),
                Password = _configuration.GetValue<string>("RabbitMQConnectionString:Password"),
                VirtualHost = _configuration.GetValue<string>("RabbitMQConnectionString:VirtualHost"),
            };
            connection = factory.CreateConnection();
        }

        public IModel CreateChannel()=> connection.CreateModel();

        public void Dispose()
        {
            connection?.Dispose();
        }
    }
}