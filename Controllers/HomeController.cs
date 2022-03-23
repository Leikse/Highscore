using Microsoft.AspNetCore.Mvc;
using Highscore.Data;
using Highscore.Models.ViewModels;

namespace Highscore.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(HighscoreContext context)
        {
            Context = context;
        }

        private HighscoreContext Context { get; }

        public IActionResult Index()
        {
            var highscores = Context.Score.ToList();

            var viewModel = new HomeIndexViewModel
            {
                HighscoreDetails = highscores,
            };

            return View(viewModel);
        }
    }
}