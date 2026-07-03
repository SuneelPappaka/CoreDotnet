namespace CoreDotnet.Services
{
    public interface IPaymentService
    {
        string pay  (decimal amount, string currency, string paymentMethod);
    }
}
