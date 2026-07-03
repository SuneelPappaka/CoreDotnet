using CoreDotnet.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoreDotnet.Controllers
{
    public class OrderController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly INotificationService _notificationService;
        public OrderController(IPaymentService paymentserivice,INotificationService notificationService)
        {
            _paymentService = paymentserivice;
            _notificationService = notificationService;
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
        [HttpPost("Register")]
        public IActionResult RegisterNotifictioAction()
        {
            string result = _notificationService.EmailNotification("Suneel123@gmail.com","Registeration", "Order placed successfully","Suneel123@gmail.com");
            return View();
        }
        [HttpPost("sendotp")]
        public IActionResult SendOtpNotifictioAction()
        {
            string result = _notificationService.SmsNotification("7799195250", "Order placed successfully");
            return View();
        }
    }
}
