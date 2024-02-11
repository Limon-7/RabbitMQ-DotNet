### 1#Integration pattern:
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


### 2# Messaging Protocol:
Messaging protocols define the rules and conventions for exchanging messages between systems or components within a distributed environment. These protocols facilitate communication, ensuring that messages are formatted, transmitted, and interpreted consistently.
1. AMQP (Advanced Message Queuing Protocol)(Processing)
2. MQTT (Message Queuing Telemetry Transport)
3. HTTP/HTTPS (Hypertext Transfer Protocol/Secure)
4. WebSockets
5. SAOP
6. gRPC (gRPC Remote Procedure Call)(Processing)

#### 2.1# AMQP (Advanced Message Queuing Protocol)(Processing):
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
