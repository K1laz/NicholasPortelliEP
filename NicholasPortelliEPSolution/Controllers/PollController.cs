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
        public IActionResult Create(Poll poll, [FromServices] IPollRepository repo)
        {
            if (ModelState.IsValid)
            {
                repo.CreatePoll(poll);  // Works with both DB or File based on Program.cs
                return RedirectToAction("Success");
            }

            return View(poll);
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Index([FromServices] IPollRepository repo)
        {
            var polls = repo.GetPolls()
                .OrderByDescending(p => p.DateCreated)
                .ToList();

            return View(polls);
        }

        public IActionResult Vote(int id, [FromServices] IPollRepository repo)
        {
            var poll = repo.GetPolls().FirstOrDefault(p => p.Id == id);

            if (poll == null)
                return NotFound();

            return View(poll);
        }

        [HttpPost]
        public IActionResult SubmitVote(int id, int selectedOption, [FromServices] IPollRepository repo)
        {
            var poll = repo.GetPolls().FirstOrDefault(p => p.Id == id);
            if (poll == null) return NotFound();

            // Increase vote count based on selected option
            switch (selectedOption)
            {
                case 1: poll.Option1VotesCount++; break;
                case 2: poll.Option2VotesCount++; break;
                case 3: poll.Option3VotesCount++; break;
                default: return BadRequest("Invalid option selected.");
            }

            repo.Vote(poll);
            return RedirectToAction("Success");
        }

        public IActionResult Results(int id, [FromServices] IPollRepository repo)
        {
            var poll = repo.GetPolls().FirstOrDefault(p => p.Id == id);
            if (poll == null) return NotFound();

            return View(poll); // This points to Views/Poll/Results.cshtml
        }

    }
}
