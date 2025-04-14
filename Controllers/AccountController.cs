using Microsoft.AspNetCore.Mvc;
using TicketsWebMVC.Services;
using TicketsWebMVC.Models;

namespace TicketsWebMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _authService.LoginAsync(model.Email, model.Password);
                if (user != null)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Credenciales inválidas");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Login");
        }




        public IActionResult Index()
        {
            return View();
        }
    }
}
