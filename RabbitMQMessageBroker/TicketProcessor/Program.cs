using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace TicketProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/",
            };
            var conn = factory.CreateConnection();
            using var channel = conn.CreateModel();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, eventArg) =>
            {
                //byte array[]
                var body = eventArg.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Messages have received:{message}");
            };

            channel.BasicConsume("booking", true, consumer);
            Console.ReadKey();
        }
    }
}
