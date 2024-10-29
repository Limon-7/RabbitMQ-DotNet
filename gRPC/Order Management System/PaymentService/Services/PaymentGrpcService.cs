using Grpc.Core;


namespace PaymentService.Services;

public class PaymentGrpcService : PaymentService.PaymentServiceBase
{
    public override async Task<PaymentResponse> ProcessPayment(PaymentRequest request, ServerCallContext context)
    {
        try
        {
            // Validate the input
            if (request == null || request.Amount <= 0)
            {
                return new PaymentResponse { PaymentStatus = "Failure: Invalid Request" };
            }

            // Respect cancellation
            if (context.CancellationToken.IsCancellationRequested)
            {
                return new PaymentResponse { PaymentStatus = "Cancelled" };
            }

            // Simulate payment processing delay
            await Task.Delay(2000, context.CancellationToken);

            // Determine payment status
            var paymentStatus = "Success";

            return new PaymentResponse { PaymentStatus = paymentStatus };
        }
        catch (TaskCanceledException)
        {
            return new PaymentResponse { PaymentStatus = "Cancelled" };
        }
        catch (Exception ex)
        {
            // Log exception (not shown)
            return new PaymentResponse { PaymentStatus = $"Failure: {ex.Message}" };
        }
    }
}
