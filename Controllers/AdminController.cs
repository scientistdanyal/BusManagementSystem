using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace BusManagementSystem.Controllers
{
    public class AdminController : Controller
    {
        // In a real app, move these to configuration / database and hash passwords
        private const string AdminUsername = "admin";
        private const string AdminPassword = "admin123";
        public const string AdminSessionKey = "IsAdmin";

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString(AdminSessionKey) == "true")
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            if (username == AdminUsername && password == AdminPassword)
            {
                HttpContext.Session.SetString(AdminSessionKey, "true");
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid username or password.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove(AdminSessionKey);
            return RedirectToAction("Login");
        }
    }
}


