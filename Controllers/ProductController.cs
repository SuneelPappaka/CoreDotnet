using CoreDotnet.Data;
using CoreDotnet.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreDotnet.Controllers
{
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
    }
}

