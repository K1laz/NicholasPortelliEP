using Domain;
using Microsoft.AspNetCore.Mvc;

namespace NicholasPortelliEPSolution.Controllers
{
    public class LoginController : Controller
    {
        private readonly List<User> _users = new()
        {
            new User { Username = "admin", Password = "password" },
        };

        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            var user = _users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                HttpContext.Session.SetString("Username", username);
                return RedirectToAction("Index", "Poll");
            }

            ViewBag.Error = "Invalid credentials";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
