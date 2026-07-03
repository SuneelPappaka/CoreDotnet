namespace CoreDotnet.Services
{
    public class RayZorPayServices : IPaymentService
    {
        /// <summary>
        //// This is a payment service that uses RayZorPay as the payment gateway. You can use this service to charge money from your customers. You can implement this service and register it in the DI container.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        /// <param name="paymentMethod"></param>
        /// <returns></returns>
        public string pay(decimal amount, string currency, string paymentMethod)
        {
            return $"paid {amount} using RayZorPay";
        }   
    
    }
}
