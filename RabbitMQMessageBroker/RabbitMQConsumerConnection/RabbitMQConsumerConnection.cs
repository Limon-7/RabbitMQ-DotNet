using RabbitMQ.Client;

namespace RabbitMQConsumerConnection
{
    public static class RabbitMQConsumerConnection
    {
        public static IModel CreateChannel()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/",
            };
            var conn = factory.CreateConnection();
            return conn.CreateModel();
        }
    }
}