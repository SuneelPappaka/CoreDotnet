using CoreDotnet.Data;
using CoreDotnet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CoreDotnet.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public CartController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext= applicationDbContext;
        }
        public IActionResult Index()
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItems = _applicationDbContext.CartItem.Where(x=>x.UserId==userid).Include(x=>x.Product).ToList();
            return View(cartItems);
        }
    }
}
