using Microsoft.AspNetCore.Mvc;

namespace Petland_Shop.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult MyAccount()
        {
            return View();
        }
    }
}
