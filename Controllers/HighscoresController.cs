#nullable disable
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Highscore.Data;
using Highscore.Models.Domain;
using Highscore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Highscore.Controllers;

public class HighscoresController : Controller
{
    private readonly HighscoreContext _context;

    public HighscoresController(HighscoreContext context)
    {
        _context = context;
    }

    // GET: Highscores
    public async Task<IActionResult> Index()
    {
        return View(await _context.Score.ToListAsync());
    }

    // GET: Highscores/Details/5
    public async Task<IActionResult> Details(int? id)
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

        return View(score);
    }

    // GET: Highscores/Create
    [Route("highscores/new")]
    public IActionResult Create()
    {
        var viewModel = new RegisterHighscoreViewModel
        {
            GameItems = new List<SelectListItem>()
        };

        foreach (var game in _context.Game.ToList())
        {
            viewModel.GameItems.Add(new SelectListItem { Value = game.Name, Text = game.Name });
        };

        return View(viewModel);
    }

    // POST: Highscores/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("highscores/new")]
    public async Task<IActionResult> Create(RegisterHighscoreViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var game = _context.Game.FirstOrDefault(x => x.Name == viewModel.Game);

            var score = new Score
            {
                PlayerName = viewModel.PlayerName,
                Date = viewModel.Date,
                PlayerScore = viewModel.PlayerScore,
                Game = game
            };

            _context.Add(score);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        viewModel = new RegisterHighscoreViewModel
        {
            GameItems = new List<SelectListItem>()
        };

        foreach (var game in _context.Game.ToList())
        {
            viewModel.GameItems.Add(new SelectListItem { Value = game.Name, Text = game.Name });
        };

        return View(viewModel);
    }

    // GET: Highscores/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var score = await _context.Score.FindAsync(id);
        if (score == null)
        {
            return NotFound();
        }
        return View(score);
    }

    // POST: Highscores/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,PlayerName,Date,PlayerScore")] Score score)
    {
        if (id != score.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(score);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScoreExists(score.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(score);
    }

    // GET: Highscores/Delete/5
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

        return View(score);
    }

    // POST: Highscores/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var score = await _context.Score.FindAsync(id);
        _context.Score.Remove(score);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ScoreExists(int id)
    {
        return _context.Score.Any(e => e.Id == id);
    }
}

