## 1#Integration pattern:
Integration patterns are design solution that address common challenges and problems encountered when connecting different applications or services. Integration patterns help in achieving consistency, reliability, and maintainability in the development of integrated solutions.
1. File Transfer
2. Shared Database
3. Direct Connection
4. Remote Procedure Invocation(Sync)
5. Messaging(Async)

    #### 1.  File-based integration:
    File-based integration involves the exchange and processing of data through files. This approach is commonly used when systems need to share information in the form of files, which can be text, binary, or other formats.
    - One application is responsible for creation a file.
    - Other application may keep watching to any change in the file directory; if any change happens, this applications will automatically grab the changes.

    #### 2. Shared Database:
    A shared database integration pattern involves different systems or applications sharing a common database to exchange and synchronize data. This integration approach allows multiple systems to interact and collaborate by reading from and writing to a shared data store.
    #### 3. Direct Connection:
    Applications connect and send message each other dirrectly without any intermediate middleware.
    - Use HTTP, TCP/IP or custom protocols to communicate among applications.
    - Message format can be text-based(XML or json), or binary, or anything.
    #### 4. Remote Procedure Invocation:
    Remote Procedure Invocation (RPI) is a communication mechanism that allows a program to invoke procedures or methods that are located on a remote system. This enables distributed systems to interact with each other by calling functions or methods across network boundaries.
    - RPI abstracts the complexities of networking and communication protocols.
    - RPI technologies often come with built-in support for interoperability between different programming languages and platforms.
    #### 5. Messaging(Async):
    Messaging integration is a communication paradigm that involves the exchange of messages between different systems, applications, or components. This is called Message Broker or Message Bus. Messaging provides a way for disparate systems to communicate asynchronously, enabling more decoupled and scalable architectures.
    - Use intermediary middleware, ex: RabbitMQ
    - Decouple publisher and consumer.


## 2# Messaging Protocol:
Messaging protocols define the rules and conventions for exchanging messages between systems or components within a distributed environment. These protocols facilitate communication, ensuring that messages are formatted, transmitted, and interpreted consistently.
1. AMQP (Advanced Message Queuing Protocol)(Processing)
2. MQTT (Message Queuing Telemetry Transport)
3. HTTP/HTTPS (Hypertext Transfer Protocol/Secure)
4. WebSockets
5. SAOP
6. gRPC (gRPC Remote Procedure Call)(Processing)

### 2.1# AMQP (Advanced Message Queuing Protocol)(Processing):
AMQP (Advanced Message Queuing Protocol) is an open standard application layer protocol for message-oriented middleware. It facilitates the reliable exchange of messages between different systems, applications, or components. AMQP defines a set of rules and conventions for message format, routing, and behavior, allowing interoperability between different messaging systems.

<p align="center"><img width="70%" src="./images/AMQP.png" /></p>

- **Use-Case:** AMQP is primarily used for message queuing and communication between distributed systems.
- **Message Broker:** A central component in AMQP is the message broker, which acts as an intermediary responsible for receiving, routing, and delivering messages between producers and consumers.
- **Message Queue:** Messages in AMQP are often placed in message queues within the broker. Queues store and manage messages, ensuring their orderly delivery to consumers.
- **Exchanges:** Exchanges in AMQP define the routing logic for messages. Producers send messages to exchanges, which then route them to the appropriate queues based on routing keys and rules.
- **Channels:** AMQP communication occurs over channels, which are lightweight, virtual connections within a physical connection. Multiple channels can be created over a single connection.
- **Pub/Sub Pattern:** AMQP supports the publish-subscribe messaging pattern, where messages are sent to an exchange and then delivered to multiple queues (subscribers) based on routing rules.
- **Reliability:** AMQP introduces the concept of message queues, where messages are stored until they are consumed by the intended recipient. This ensures reliable message delivery and supports asynchronous communication.
- **Message Persistence:** Messages in AMQP can be marked as persistent, meaning they are stored on disk, ensuring message durability even if the message broker restarts.

#### 2.1.1# Elements of Messaging System:
1. **Message:** The data to be exchanged between the systems. Every message has two parts, `routing information` and `actual data(payload)`.
2. **Producer/Publisher:** Creates the message and sends them to Message Broker.
3. **Consumer/Receiver:** Get or receive the message from the message-broker and process them.
4. **Message Queue:** Contains a list of messages and every queue has a unique name.
5. **Message Broker:** It is the indermidiary middleware between publisher and consumer.
6. **Router/Exchange:** It is a routing mechanism that determines how message are distributed to queues and consumers.
7. **Connection:** Real TCP(Transmission Control Protocol enables application programs and computing devices to exchange messages over a network) connection with either Publisher or Consumer with message broker.
8. **Channel:** It is a virtual conections in real `TCP connections`. Message are transmited over the channels. So, there is atleast one channel either `Producer or Consumer` with `message broker`.
9. **Binding:** Define the relationship between exchange and queue and may contain arguments - `routing-key or header` to filter the messages to be sent to the bounded queue.

#### 2.1.2# Attributes of a Message:
1. **Routing Key:** Information used by RabbitMQ to route the message to the appropriate destination.`routing_key: "important.events"`
2. **Headers:** The headers property contains additional metadata or key-value pairs that provide contextual information about the message.`headers: { "content-type": "application/json", "version": "1.0" }`
3. **Payloads:** Actual message data.
4. **Publishing Timestapm:** The `timestamp` property indicates the time when the message was created or sent.
5. **Expiration:** The `expiration` property specifies the time until which the message is considered valid or relevant.`expiration: 60000 # valid for 60s`.
6. **Delivery Mode:** The `delivery_mode` property indicates whether the message should be `persisted (2)` or `not (1)`. `delivery_mode: 2  # Message is persistent`.
7. **Priority:** The `priority` property indicates the importance or `priority` level of the message within `0-255`.`priority: 5`.
8. **Message Id:** The `message_id` property represents a unique identifier assigned to each message. It helps in tracking and correlating messages.
8. **Correlation Id:** The `correlation_id` property is used to associate related messages. It is often employed in scenarios where multiple messages are part of the same transaction or process. Generally used in `gRPC` for matching `request and response`.
9. **Reply To:** The `reply_to property` specifies the destination or queue to which a reply or response to the message should be sent.`reply_to: "response_queue"`.
9. **Ackmowledgement:** The acknowledgment `(ack)` property is used to determine whether the message requires acknowledgment from the consumer upon successful processing.`ack: true`.

#### 2.1.3# Attributes of a Queue:
1. **Name:** A unique identifier for the queue within the RabbitMQ broker, max 256 charecters.
2. **Durable:** Specifies whether the queue and its messages should survive broker restarts.`durable: true`
3. **Auto Delete:** Specifies whether the queue should be deleted when there are no consumers connected.`autoDelete: false`
4. **Exclusive:** Indicates whether the queue is exclusive to one connection and will be deleted when that connection closes.`exclusive: false`
5. **Max Length:** Specifies the maximum number of `awaiting messages` allowed in the queue.`"x-max-length": 100`
6. **Max Priority:** Specifies the maximum priority level for messages in the queue.`"x-max-priority": 10`
7. **Message TTL:** Specifies the maximum time a message can stay in the queue before being discarded.`"x-message-ttl": 60000`
8. **Dead-letter Exchange:** Specifies the exchange to which messages should be `automatically sent` when they are rejected or expired.`"x-dead-letter-exchange": "deadLetterExchange"`
9. **Binding Configuration:** A queue must be bound to an exchange, in order to receive message from exchange.
10. **Queue Mode:** Specifies the queue mode, such as "lazy" for a lazy queue that stores messages on disk.`"x-queue-mode": "lazy"`
11. **Arguments:** Additional configuration arguments for the queue, such as message `TTL or maximum length`. `{ "x-max-length": 100 }`

#### 2.1.4# Exchange:
1. **Name:** Unique name of the exchange.
2. **Type:** The common types:
    - **`Direct Exchange:`** Messages are routed to queues based on an exact match between the routing key specified by the producer and the routing key specified in the binding.
    - **`Fanout Exchange:`** Messages are broadcast to `all queues` bound to the `exchange`, regardless of `routing keys`. ``
    - **`Topic Exchange:`**  Messages are routed to queues based on `wildcard patterns` in the routing key.
        - `*`: Matches exactly one word. `"*.image"` will match `"convert.image"` but not `"convert.bitmap.image"` or `"image.jpg"`
        - `#`: Matches zero or more words. `image.#` will match `image.jpg` and `image.bitmap.32bit` but not `convert.image`
    - **`Header Exchange:`**  Messages are routed based on `header` attributes rather than routing keys. `{"x-match": "all"}`
3. **Durable:** Indicates whether the exchange should survive broker restarts.`durable: true`
4. **Auto-Delete:** Specifies whether the exchange should be automatically deleted when there is no bounded queue left.`autoDelete: false`
5. **Internal Exchange:** Indicates whether the exchange can be used only for receiving message from other exchange and cannot be published to directly.`internal: false`.
6. **Alternate Exchange:** Specifies an exchange to which messages will be sent if they cannot be routed to any queues in the current exchange. `"alternate-exchange", "alternateExchange"`.
7. **Arguments:** Additional configuration arguments for the exchange, which can be used for specific behaviors or policies.`arguments: { "x-custom-argument", "value" }`.
8. **Exchange Mode:** Specifies the exchange mode, such as "lazy" for a lazy exchange that stores messages on disk.`"x-exchange-mode", "lazy"`
9. **Exchange Binding:** Exchange binding is the process of connecting one exchange to another. This is achieved by specifying the source exchange, the destination exchange, and the routing key or pattern that determines how messages should be routed from the source to the destination.
    - Exchange to Exchange binding is useful in scenarios where you want to aggregate messages from multiple sources before routing them to queues or other exchanges based on certain criteria.
    ```c#
    channel.ExchangeDeclare(exchange: "sourceExchange", type: ExchangeType.Direct);
    channel.ExchangeDeclare(exchange: "destinationExchange", type: ExchangeType.Direct);
    channel.ExchangeBind(destination: "destinationExchange", source: "sourceExchange", routingKey: "exampleRoutingKey");
    ```