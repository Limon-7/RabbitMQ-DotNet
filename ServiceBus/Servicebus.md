## Service Bus
Service Bus, usually shortened to Bus, is the term given to the type of application that handles the movement of `messages or event`.

### MassTransit
MassTransit is a free, open-source, distributed application framework for .NET applications. It abstracts away the underlying logic required to work with `message brokers`, such as `RabbitMQ`,`Azure Service Bus`, `Amazon SQS`, making it easier to create message-based, loosely coupled applications.


### Why do we use MassTransit?
1. Firstly, by abstracting the underlying message broker logic, we can work with multiple message brokers, without having to completely rewrite our code. This allows us to work with something such as the InMemory transport when working locally, then when deploying our code, use another transport such as Azure Service Bus or Amazon Simple Queue Service.
2.  There are a lot of specific patterns we need to be aware of and implement, such as `retry, circuit breaker, outbox `to name a few. MassTransit handles all of this for us, along with many other features such as `exception handling`, `distributed transactions`, and `monitoring`.

### MassTransit Common Terminology:

1. ***`Transports`***: Transports are the different types of message brokers MassTransit works with, including `RabbitMQ`, `InMemory`, `Azure Service Bus`, `Amazon SQS` and more.

2. ***`Message:`*** Message is a contract, defined code first by creating a .NET `class or interface or record` without any `method`.

3. ***`Command:`*** Command is a type of message, specifically used to tell a service to do something. These message types are `sent to an endpoint (queue)` and will be expressed using a verb-noun sequence.
   - UpdateCustomerAddress
   - CheckUserOrderstatus 
   
4. ***`Event:`:*** Events are another message type, signifying that something has happened. Events are `published to one or multiple consumers` and will be expressed using noun-verb (past tense) sequence.
   - CustomerAddressUpdated
   - UserOrderSubmitted
5. ***`Consumer:`***
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
6. ***`Publisher:`*** Publish message
   - `IPublishEndpoint`
   - `IBus`
   - `ConsumeContext`