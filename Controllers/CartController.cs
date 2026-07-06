using CoreDotnet.DataAccess.Data;
using CoreDotnet.Models;
using CoreDotnet.Utility;
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
        public async Task<ActionResult> plus(int cartid)
        {
            var cartfromDb = _applicationDbContext.CartItem.FirstOrDefault(x => x.Id == cartid);
            if (cartfromDb == null)
            {
                NotFound();
            }
            cartfromDb.Quantit += 1;
            _applicationDbContext.CartItem.Update(cartfromDb);
           await _applicationDbContext.SaveChangesAsync();
            TempData["Success"] = "Card Item Added Successfully";
            return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult> minus(int cartid)
        {
            var cartfromDb = _applicationDbContext.CartItem.FirstOrDefault(x => x.Id == cartid);
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (cartfromDb == null)
            {
                NotFound();
            }
            if (cartfromDb.Quantit <= 1)
            {
                _applicationDbContext.CartItem.Remove(cartfromDb);
                await _applicationDbContext.SaveChangesAsync();
                var count = _applicationDbContext.CartItem.Where(x => x.UserId == userid).Count();
                HttpContext.Session.SetInt32(SD.SessionCart, count);
            }
            else
            {
                cartfromDb.Quantit -= 1;
                _applicationDbContext.CartItem.Update(cartfromDb);
                await _applicationDbContext.SaveChangesAsync();
            }
            TempData["Success"] = "Card Item Deleted Successfully";
            return RedirectToAction(nameof(Index));
        }
        public async Task<ActionResult> remove(int cartid)
        {
            var cartfromDb = _applicationDbContext.CartItem.FirstOrDefault(x => x.Id == cartid);
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (cartfromDb == null)
            {
                NotFound();
            }
                _applicationDbContext.CartItem.Remove(cartfromDb);
                await _applicationDbContext.SaveChangesAsync();
                var count = _applicationDbContext.CartItem.Where(x => x.UserId == userid).Count();
            TempData["Success"] = "Card Item Removed Successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
