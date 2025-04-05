using Microsoft.AspNetCore.Mvc;
using Domain;
using DataAccess;

namespace Presentation.Controllers
{
    public class PollController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Poll poll, [FromServices] PollRepository repo)
        {
            if (ModelState.IsValid)
            {
                repo.CreatePoll(poll);
                return RedirectToAction("Success");
            }

            return View(poll);
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
