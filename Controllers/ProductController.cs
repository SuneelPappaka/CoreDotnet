using CoreDotnet.Data;
using CoreDotnet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CoreDotnet.Controllers
{
    /// <summary>
    /// [Authorize]/////if authorize we can access any views
    /// </summary>
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ProductController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            List<Product> _products = _applicationDbContext.Products.ToList();
            return View(_products);
        }
        public IActionResult Details(int Id)
        {
            var _products = _applicationDbContext.Products.FirstOrDefault(p=>p.Id==Id);
            return View(_products);
        }
        [Authorize]//////if authorize we can access any views
        [HttpPost]
        public async Task<IActionResult> Details(CartItem cartItem)
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var carddetails = _applicationDbContext.CartItem.FirstOrDefault(x => x.ProductId == cartItem.Id && x.UserId == userid);
            if (carddetails != null)
            {
                carddetails.Quantit += cartItem.Quantit;
                _applicationDbContext.CartItem.Update(carddetails);
            }
            else
            {
                cartItem.Id = 0;
                cartItem.UserId = userid;
                _applicationDbContext.CartItem.Add(cartItem);
            }
            
            await _applicationDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}

