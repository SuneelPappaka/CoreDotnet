namespace CoreDotnet.Services
{
    public class StripeServices : IPaymentService
    {
        public string pay(decimal amount, string currency, string paymentMethod)
        {
            return $"paid {amount} using striper";
        }
    }
}
