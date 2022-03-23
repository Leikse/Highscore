using Highscore.Models.Domain;

namespace Highscore.Models.ViewModels;

public class HighscoreDetailsViewModel
{
    public Score Highscore { get; set; }
    public IEnumerable<Score> HighscoreDetails { get; set; } = new List<Score>();
}

