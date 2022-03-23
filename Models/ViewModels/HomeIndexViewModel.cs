using Highscore.Models.Domain;

namespace Highscore.Models.ViewModels;

public class HomeIndexViewModel
{
    public IEnumerable<Score> HighscoreDetails { get; set; } = new List<Score>();
}

