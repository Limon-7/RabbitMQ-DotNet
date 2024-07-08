### gRPC: google Remote Procedure Call
- Uses HTTP/2 protocol to transport binary messages(TLS)
- Focus on high performance
- Relies on Protocol buffers to define the contract between end points
- Multi language support(C# client can call Pyrhon services)
- Frequently used as a method of service to service communication
- Complex use of HTTP/2 prohibits use of gRPC in browser-based apps. The way to use it we have to use a proxy or Json transcoding which effectively turns a gRPC service into a rest based API.

#### .proto: 
That is kind of interface and impleted it somewhere else.
####
