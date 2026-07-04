using CoreDotnet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreDotnet.Controllers
{
    [Authorize]/////if authorize we can access any views
    public class SubscriptionController : Controller
    {
        private readonly IPaymentService _paymentService;
        public SubscriptionController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        public IActionResult Index()
        {
            IPaymentService paymentService = new StripeServices();
            string result = paymentService.pay(100, "USD", "credit card");
            return View();
        }
        public IActionResult Paypal()
        {
            string result = _paymentService.pay(100, "USD", "credit card");
            return View();
        }
    }
}
