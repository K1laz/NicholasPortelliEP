using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Domain;

namespace Presentation.Filters
{
    public class VoteOnlyOnce: ActionFilterAttribute
    {
        public static Dictionary<string, List<int>> UserVotes = new();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var username = context.HttpContext.Session.GetString("Username");
            if (username == null)
            {
                context.Result = new RedirectToActionResult("Index", "Login", null);
                return;
            }

            var pollId = (int)context.ActionArguments["id"];
            if (!UserVotes.ContainsKey(username))
            {
                UserVotes[username] = new List<int>();
            }

            if (UserVotes[username].Contains(pollId))
            {
                context.HttpContext.Session.SetString("AlreadyVoted", "true");
                context.Result = new RedirectToActionResult("Vote", "Poll", new { id = pollId });
                return;
            }

            // Mark as voted
            UserVotes[username].Add(pollId);

            base.OnActionExecuting(context);
        }
    }
}
