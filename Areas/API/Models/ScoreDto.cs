using Highscore.Models.Domain;

namespace Highscore.Areas.API.Models;

public class ScoreDto
{
    public int Score { get; set; }
    public Game Game { get; set; }
}
