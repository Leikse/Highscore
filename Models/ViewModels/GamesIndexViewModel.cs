using Highscore.Models.Domain;

namespace Highscore.Models.ViewModels;

public class GamesIndexViewModel
{
    public Game Game { get; set; }
    public IEnumerable<Score> Scores { get; set; } = new List<Score>();
}

