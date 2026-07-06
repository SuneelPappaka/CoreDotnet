using CoreDotnet.DataAccess.Data;
using CoreDotnet.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreDotnet.Controllers
{
    [Authorize(Roles ="Admin")]/////if authorize we can access any views  ,access only for admin users
    // Tempdata we can access any where in the controller
    // Viewdata and viewbag access only in the action

    public class AdminProductController : Controller
    {

        private readonly ApplicationDbContext _applicationDbContext;
        public AdminProductController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
          List<Product> _products=  _applicationDbContext.Products.ToList();
            return View(_products);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product _product)
        {
            if(_product.ProductName== "" || _product.Description == "" || _product.Price <= 0)
            {
                ModelState.AddModelError(string.Empty, "Please fill in all required fields.");
            };
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
                TempData["Success"] = "Record Inserted Successfully";
                return RedirectToAction("Index");
            }
            else { 
            return View(_product);
            }
            
            
        }
        public async Task<IActionResult> Edit(int Id)
        {
           Product _products = await _applicationDbContext.Products.FindAsync(Id);
            return View(_products);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Product _product)
        {
            if (_product.ProductName == "" || _product.Description == "" || _product.Price <= 0)
            {
                ModelState.AddModelError(string.Empty, "Please fill in all required fields.");
            }
            ;
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
                _applicationDbContext.Products.Update(_product);

                await _applicationDbContext.SaveChangesAsync();
                TempData["Success"] = "Record Updated Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(_product);
            }
        }
        public async Task<IActionResult> Delete(int Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var _products = await _applicationDbContext.Products.FindAsync(Id);
            if (_products == null)
            {
                return NotFound();
            }
            //if(_products != null)
            //{
            //    _applicationDbContext.Products.Remove(_products);
            //}
            //await _applicationDbContext.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            return View(_products);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var _products = await _applicationDbContext.Products.FindAsync(Id);
            if (_products == null)
            {
                return NotFound();
            }
            if (_products != null)
            {
                _applicationDbContext.Products.Remove(_products);
            }
            await _applicationDbContext.SaveChangesAsync();
            TempData["Success"] = "Record Deleted Successfully";
            return RedirectToAction(nameof(Index));
        }

    }
}
