### gRPC: google Remote Procedure Call
RPC (Remote Procedure Call) is called “remote” because it enables communications between remote services when services are deployed to different servers under microservice architecture. From the user’s point of view, it acts like a local function call.

***Key Points:***
- Uses HTTP/2 protocol to transport binary messages(TLS)
- Focus on high performance
- Relies on Protocol buffers to define the contract between end points
- Complex use of HTTP/2 prohibits use of gRPC in browser-based apps. The way to use it we have to use a proxy or Json transcoding which effectively turns a gRPC service into a rest based API.

#### Why do we need gRPC?
1. ***Efficiency:*** gRPC uses HTTP/2 as its underlying protocol, allowing it to multiplex multiple requests over a single TCP connection. This reduces latency and improves efficiency, especially in microservices architectures, where service to service communication happens frequently.

2. ***Language Agnostic:*** It supports multiple programming languages, making it easier to build distributed systems where services are written in different languages.

3. ***Strong Typing:*** It uses Protocol Buffers (protobufs) for data serialization, providing strong typing and efficient serialization of data across services.

4. ***Bi-directional Streaming:*** gRPC supports streaming requests and responses, allowing for more flexible and efficient communication patterns.

#### How gRPC work?
![image](/gRPC.Service/images/grpc.jpg)
Step 1: A REST call is made from the client. The request body is usually in JSON format.

Steps 2 - 4: The order service (gRPC client) receives the REST call, transforms it, and makes an RPC call to the payment service. gRPC encodes the client stub into a binary format and sends it to the low-level transport layer.

Step 5: gRPC sends the packets over the network via HTTP2. Because of binary encoding and network optimizations, gRPC is said to be 5X faster than JSON.

Steps 6 - 8: The payment service (gRPC server) receives the packets from the network, decodes them, and invokes the server application.

Steps 9 - 11: The result is returned from the server application, and gets encoded and sent to the transport layer.

Steps 12 - 14: The order service receives the packets, decodes them, and sends the result to the client application.