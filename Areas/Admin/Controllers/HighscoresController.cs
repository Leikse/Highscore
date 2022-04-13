#nullable disable
using Highscore.Areas.Admin.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Highscore.Data;
using Highscore.Models.Domain;

namespace Highscore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HighscoresController : Controller
    {
        private readonly HighscoreContext _context;

        public HighscoresController(HighscoreContext context)
        {
            _context = context;
        }

        // GET: Admin/Highscores
        public async Task<IActionResult> Index()
        {
            var scores = _context.Score
                .Include(x => x.Game)
                .OrderByDescending(x => x.Game.Name)
                .ToList();

            var viewModel = new List<HighscoreViewModel>();

            foreach (var item in scores)
            {
                viewModel.Add(new HighscoreViewModel(item.Id, item.PlayerName, item.Date.ToString("yyyy-MM-dd"), item.PlayerScore, item.Game.Name));
            }

            return View(viewModel);
        }

        // GET: Admin/Highscores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var score = await _context.Score
                .FirstOrDefaultAsync(m => m.Id == id);
            if (score == null)
            {
                return NotFound();
            }

            score = await _context.Score.FindAsync(id);
            _context.Score.Remove(score);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Highscores");
        }
    }
}
