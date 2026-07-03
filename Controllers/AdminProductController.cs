using CoreDotnet.Data;
using CoreDotnet.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreDotnet.Controllers
{
    public class AdminProductController : Controller
    {

        private readonly ApplicationDbContext _applicationDbContext;
        public AdminProductController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product _product)
        {
            ModelState.Remove(nameof(_product.ImagePath));
            if (ModelState.IsValid)
            {
                if (_product.ImageFile != null && _product.ImageFile.Length > 0)
                {
                    var fileName = Path.GetFileName(_product.ImageFile.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        _product.ImageFile.CopyTo(stream);
                    }
                    _product.ImagePath = "/images/" + fileName;
                   
                }
                _product.CreatedAt = DateTime.Now;
                _applicationDbContext.Products.Add(_product);

                await _applicationDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else { 
            return View(_product);
            }
            
        }
    }
}
