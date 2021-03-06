using Highscore.Models.Domain;

namespace Highscore.Models.ViewModels;

public class HomeIndexViewModel
{
    public IEnumerable<Game> Games { get; set; } = new List<Game>();
    public IEnumerable<Score> Scores { get; set; } = new List<Score>();
}

