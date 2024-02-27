using System;
using System.Linq;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ExchangeTicketProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            // fanout-exchange
            ExchangeConsumer(exchangeName: "fanout.exchange", queueName: "fanout.booking", string.Empty,
                type: ExchangeType.Fanout);
            
            // direct-exchange
            ExchangeConsumer(exchangeName: "direct.exchange", queueName: "direct.booking", "direct.booking.key",
                type: ExchangeType.Direct);
            
            Console.ReadKey();      
        }
        private static void ExchangeConsumer(string exchangeName, string queueName, string routingKey,string type)
        {
            var channel = RabbitMQConsumerConnection.RabbitMQConsumerConnection.CreateChannel();
            channel.ExchangeDeclare(exchange: exchangeName, type: type, durable: true, autoDelete: false, null);
            channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, false, null);

            // bind the queue with proper exchange and routing key
            channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routingKey, arguments: null);
            var consumer = new EventingBasicConsumer(channel);


            consumer.Received += (model, eventArg) =>
            {
                //byte array[]
                var body = eventArg.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Messages have received:{message}");
            };

            channel.BasicConsume(queue: queueName, true, consumer);
        }
    }
}
