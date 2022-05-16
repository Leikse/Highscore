using Highscore.Models.Domain;

namespace Highscore.Areas.API.V2.Models;

public class ScoreDto
{
    public int Score { get; set; }
    public Game Game { get; set; }
}
