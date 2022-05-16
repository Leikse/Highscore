using Highscore.Areas.API.V1.Models;
using Highscore.Data;
using Highscore.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Highscore.Areas.API.V1.Controllers;

[Area("API")]
[Route("api/[controller]")]
[ApiController]
public class ScoresController : ControllerBase
{
    public ScoresController(HighscoreContext context)
    {
        Context = context;
    }

    private HighscoreContext Context { get; }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IEnumerable<ScoreDto> Index([FromQuery] string? name)
    {
        var score = string.IsNullOrEmpty(name)
            ? Context.Score.ToList()
            : Context.Score.Where(x => x.Game.Name.Contains(name)).ToList();

        var scoreDtos = score.Select(score => new ScoreDto
        {
            Score = score.PlayerScore,
            Game = score.Game,
        });

        return scoreDtos;
    }

    /// <summary>
    /// Get highscore by ID
    /// </summary>
    /// <param name="id">Score ID</param>
    /// <returns>Highscore</returns>

    [HttpGet("{id}", Name = "GetScore")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ScoreDto> GetScore(int id)
    {
        var score = Context.Score.Include(x => x.Game).FirstOrDefault(x => x.Id == id);

        if (score == null)
        {
            return NotFound();
        }

        var scoreDto = new ScoreDto
        {
            Score = score.PlayerScore,
            Game = score.Game
        };

        return scoreDto;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult RegisterHighscore(RegisterHighscoreDto registerHighscoreDto)
    {
        var game = Context.Game.FirstOrDefault(x => x.Name == registerHighscoreDto.Game);

        var score = new Score
        {
            PlayerName = registerHighscoreDto.PlayerName,
            Date = DateTime.Parse(registerHighscoreDto.Date),
            PlayerScore = registerHighscoreDto.Score,
            Game = game
        };

        Context.Add(score);

        Context.SaveChanges();

        return CreatedAtAction(
            nameof(GetScore),
            new { id = score.Id },
            null);
    }
}
