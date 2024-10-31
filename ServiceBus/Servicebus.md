# Service Bus
Service Bus, usually shortened to Bus, is the term given to the type of application that handles the movement of `messages or event`.

# MassTransit
MassTransit is a free, open-source, distributed application framework for .NET applications. It abstracts away the underlying logic required to work with `message brokers`, such as `RabbitMQ`,`Azure Service Bus`, `Amazon SQS`, making it easier to create message-based, loosely coupled applications.


   ## Why do we use MassTransit?
   1. Firstly, by abstracting the underlying `message broker logic`, we can work with multiple message brokers, without having to completely rewrite our code. This allows us to work with something such as the InMemory transport when working locally, then when deploying our code, use another transport such as Azure Service Bus or Amazon Simple Queue Service.
   2.  There are a lot of specific patterns we need to be aware of and implement, such as `retry, circuit breaker, outbox `to name a few. MassTransit handles all of this for us, along with many other features such as `exception handling`, `distributed transactions`, and `monitoring`.

---
   ## MassTransit Common Terminology:
   1. ***`Transports`***: Transports are the different types of message brokers MassTransit works with, including `RabbitMQ`, `InMemory`, `Azure Service Bus`, `Amazon SQS` and more.

   2. ***`Message:`*** Message is a contract, defined code first by creating a .NET `class or interface or record` without any `method`.

   3. ***`Command:`*** Command is a type of message, specifically used to tell a service to do something. These message types are `sent to an endpoint (queue)` and will be expressed using a verb-noun sequence.
      - UpdateCustomerAddress
      - CheckUserOrderstatus 
      
   4. ***`Event:`:*** Events are another message type, signifying that something has happened. Events are `published to one or multiple consumers` and will be expressed using noun-verb (past tense) sequence.
      - CustomerAddressUpdated
      - UserOrderSubmitted

---
   ## How do we consume message:
   1. ***`Consumer:`***
      ```bash
      services.AddMassTransit(x =>
      {
         x.AddConsumer<SubmitOrderConsumer>();

         x.UsingInMemory((context, cfg) =>
         {
               cfg.ConfigureEndpoints(context); #will automatically configure the received endpoint. 
         });
      });
            
      ```
   ---

   ## How can we publish message:
   1. **IPublishEndpoint:** Publish events to `multiple consumers`. Use IPublishEndpoint when you want to publish events to multiple consumers `(publish-subscribe pattern)`.
      ```c#
      await _publishEndpoint.Publish(new OrderCreated
      {
         OrderId = Guid.NewGuid(),
         Timestamp = DateTime.UtcNow
      });
      ```
   2. **ISendEndpoint:** Send commands to specific endpoints. Use `ISendEndpoint` for `point-to-point communication` when only `one service` should process the command.
      ```c#
      var sendEndpoint = await _bus.GetSendEndpoint(new Uri("queue:create-order"));
      await sendEndpoint.Send(new CreateOrder
      {
         OrderId = Guid.NewGuid(),
         ProductName = "Laptop",
         Quantity = 1
      });
      ```
   3. **IRequestClient:** Request-response communication. Use IRequestClient for synchronous communication when you need a response from the consumer.
      ```c#
      //publish
      var response = await _requestClient.GetResponse<OrderStatusResponse>(new CheckOrderStatus { OrderId = orderId });

      // Consumer code:
      public class CheckOrderStatusConsumer : IConsumer<CheckOrderStatus>
      {
         public async Task Consume(ConsumeContext<CheckOrderStatus> context)
         {
            await context.RespondAsync(new OrderStatusResponse
            {
               OrderId = context.Message.OrderId,
               Status = "Processing"
            });
         }
      }
      ```
   4. **IMessageScheduler:** Schedule future events.
      ```c#
      public class ReminderService
      {
         private readonly IMessageScheduler _scheduler;

         public ReminderService(IMessageScheduler scheduler)
         {
            _scheduler = scheduler;
         }

         public async Task ScheduleOrderReminderAsync(Guid orderId)
         {
            await _scheduler.SchedulePublish(DateTime.UtcNow.AddMinutes(5), new OrderReminder
            {
                  OrderId = orderId
            });
         }
      }
      ```
   5. **Routing Slips:** Orchestrate multi-step workflows. Use routing slips for orchestrating complex workflows across multiple services.
      ```c#
      public async Task ExecuteOrderWorkflowAsync()
      {
         var builder = new RoutingSlipBuilder(Guid.NewGuid());

         builder.AddActivity("ProcessPayment", new Uri("queue:process-payment"), new { Amount = 100.0M });
         builder.AddActivity("ShipOrder", new Uri("queue:ship-order"), new { OrderId = 123 });

         var routingSlip = builder.Build();
         await _bus.Execute(routingSlip);
      }
      //consumer section
      public class ProcessPaymentConsumer : IConsumer<dynamic>
      {
         public async Task Consume(ConsumeContext<dynamic> context)
         {
            Console.WriteLine($"Processing Payment of {context.Message.Amount}");
         }
      }

      public class ShipOrderConsumer : IConsumer<dynamic>
      {
         public async Task Consume(ConsumeContext<dynamic> context)
         {
            Console.WriteLine($"Shipping Order: {context.Message.OrderId}");
         }
      }
      ```
1. ***`Publisher:`*** Publish message
   - `IPublishEndpoint:` This works like Rabbitmq Fanout exchange. `_publisher.Publish(new OrderPlaced(101, "Limon"))`
   - `IBus`:
   - `ConsumeContext`
