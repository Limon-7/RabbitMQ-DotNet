## gRPC: google Remote Procedure Call
RPC (Remote Procedure Call) is called “remote” because it enables communications between remote services when services are deployed to different servers under microservice architecture. From the user’s point of view, it acts like a local function call.

1. **`Basic Unary RPC`**: Client sends one request and gets one response.
2. **`Server` Streaming `RPC`**: Client sends one request and receives a stream of responses.
3. **`Client Streaming RPC`**: Client sends multiple requests and gets one response.
4. **`Bidirectional Streaming RPC`**: Both client and server send streams of data.

***Key Points:***
- Uses HTTP/2 protocol to transport binary messages(TLS)
- Focus on high performance
- Relies on Protocol buffers to define the contract between end points
- Complex use of HTTP/2 prohibits use of gRPC in browser-based apps. The way to use it we have to use a proxy or Json transcoding which effectively turns a gRPC service into a rest based API.

---

### Why do we need gRPC?
1. ***Efficiency:*** gRPC uses HTTP/2 as its underlying protocol, allowing it to multiplex multiple requests over a single TCP connection. This reduces latency and improves efficiency, especially in microservices architectures, where service to service communication happens frequently.
   1. **`Multiplexing`**: Multiple requests can be sent over a single TCP connection, reducing the number of connections needed and lowering latency
   2. **`Binary Format`**: HTTP/2 transmits data in binary format, which is faster to parse and more compact than the text-based HTTP/1.1.
   3. **`Header Compression`**: It uses `HPACK compression` for headers, reducing the overhead for frequently sent data.
   
2. *`Protocol Buffers (Protobuf)`**: gRPC uses Protocol Buffers for `data serialization`, which is a compact, efficient `binary format`. Protobuf is `faster and smaller` than traditional `text-based formats like JSON or XML`, reducing bandwidth usage and speeding up `serialization and deserialization`.

3. ***Language Agnostic:*** It supports multiple programming languages, making it easier to build distributed systems where services are written in different languages.

4. ***Strong Typing:*** It uses Protocol Buffers (protobufs) for data serialization, providing strong typing and efficient serialization of data across services.

5. ***Bi-directional Streaming:*** gRPC supports `bidirectional streaming`, allowing the client and server to `send multiple messages` back and forth on a `single connection` without waiting for a response after each message. This is ideal for real-time applications and reduces the time spent on network round-trips.

---
### When should we consider using gRPC?
#### Synchronous API calls:
When particular services need to `call each other synchronously` and require `immediate responses`.
For example, In an e-commerce application where the `OrderService` needs to validate `payment` through the `PaymentService` before processing an order.

#### Low Latency Requirements
When you need fast responses and real-time communication. For example, in a chat application where user messages need to be processed in `real-time`. `gRPC's bidirectional streaming uses to send and receive messages simultaneously.`

#### Strongly Typed Contracts
When you want strict type-checking for your API contracts to ensure `safety and maintainability`. A finance application where strict data validation is required for `transactions`.

----
### gRPC vs. Messaging Protocols

| Feature               | gRPC                                     | Messaging Protocols                                |
|-----------------------|------------------------------------------|---------------------------------------------------|
| **Communication Type** | Synchronous                             | Asynchronous                                      |
| **Latency**           | Low latency                              | Higher latency (due to queuing)                   |
| **Data Contracts**    | Strongly typed contracts                 | No strict contracts; message formats may vary     |
| **Use Case Examples** | Real-time data processing, APIs         | Event-driven architecture, task queues            |
| **Fault Tolerance**   | Retries must be implemented at app level | Built-in message persistence and retries          |
| **Complexity**        | Easier to implement for direct calls    | More complex due to additional infrastructure     |
| **Scalability**       | Can be scaled but requires careful design | Excellent scalability with distributed systems   |

----

### How gRPC work?
![image](/gRPC.Service/images/grpc.jpg)
Step 1: A REST call is made from the client. The request body is usually in JSON format.

Steps 2 - 4: The order service (gRPC client) receives the REST call, transforms it, and makes an RPC call to the payment service. gRPC encodes the client stub into a binary format and sends it to the low-level transport layer.

Step 5: gRPC sends the packets over the network via HTTP2. Because of binary encoding and network optimizations, gRPC is said to be 5X faster than JSON.

Steps 6 - 8: The payment service (gRPC server) receives the packets from the network, decodes them, and invokes the server application.

Steps 9 - 11: The result is returned from the server application, and gets encoded and sent to the transport layer.

Steps 12 - 14: The order service receives the packets, decodes them, and sends the result to the client application.


## C# use-case