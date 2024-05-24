using Boocic.AccountVm;
using Boocic.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Boocic.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _sigInManager;


        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _sigInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }
            User user = new User()
            {
                Name = registerVM.Email,
                Email = registerVM.Email,
            };
            var result = await _userManager.CreateAsync(user, registerVM.Password);

            if (!result.Succeeded)
            {

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
    
            return RedirectToAction("Login");

        }




        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginVm loginVM)
        {
            if (!ModelState.IsValid)
            {

                return View(loginVM);
            }
            User user;
            if (loginVM.Email.Contains("@"))
            {

                user = await _userManager.FindByEmailAsync(loginVM.Email);

            }
            else
            {
                user = await _userManager.FindByNameAsync(loginVM.Username);
            }

            if (user == null)
            {

                ModelState.AddModelError("", "Sehv ad veya kod");
                return View();
            }
            var result = await _sigInManager.CheckPasswordSignInAsync(user, loginVM.Password, true);

            if (!result.Succeeded)
            {

                ModelState.AddModelError("", "Sehv ad veya kod");
                return View();
            }

            if (result.IsLockedOut)
            {

                ModelState.AddModelError("", "birazdan yoxla");
                return View();
            }
            await _sigInManager.SignInAsync(user, loginVM.RememberMe);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _sigInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //Burda biraz alemi qarishdirdim bir birine,Templete deyishdim deye,Xaish edirem nezere alarsiniz.
    }
}

