using Grpc.Core;
using PaymentService;// Payment gRPC client namespace
using Grpc.Net.Client;

namespace OrderService.Services;
public class OrderGrpcService:OrderService.OrderServiceBase
{
    private readonly PaymentService.PaymentService.PaymentServiceBase _paymentClient;

    public OrderGrpcService(PaymentService.PaymentService.PaymentServiceBase paymentClient)
    {
        _paymentClient = paymentClient;
    }

    public override async Task<OrderResponse> SubmitOrder(OrderRequest request, ServerCallContext context)
    {
        var paymentResponse = await _paymentClient.ProcessPayment(new PaymentRequest
        {
            OrderId = request.OrderId,
            Amount = request.Amount,
            PaymentMethod = request.PaymentMethod
        });

        var status = paymentResponse.PaymentStatus == "Success" ? "Order Submitted" : "Failed";
        return new OrderResponse { Status = status };
    }
}
