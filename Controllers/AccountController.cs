using CoreDotnet.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CoreDotnet.ViewModels;

namespace CoreDotnet.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login(string returnurl)
        {
            if( returnurl==null)
            {
                NotFound();
            };
        
            return View(returnurl);
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel _registerViewModel)
        {
            return View();
        }

    }
}
