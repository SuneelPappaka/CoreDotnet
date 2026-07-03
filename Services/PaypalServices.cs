
namespace CoreDotnet.Services
{
    /// <summary>
    /// if you want to use paypal as payment gateway, you can implement this service 
    /// and StripeServices charing money high , so you can use paypal as payment gateway, but you need to implement this service and register it in the DI container
    /// 
    /// 
    /// </summary>
    public class PaypalServices: IPaymentService
    {
        public string pay(decimal amount, string currency, string paymentMethod)
        {
            return $"paid {amount} using paypal";
        }   
    
    }
}
