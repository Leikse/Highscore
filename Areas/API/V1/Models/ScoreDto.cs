using Highscore.Models.Domain;

namespace Highscore.Areas.API.V1.Models;

/// <summary>
/// Game score
/// </summary>
public class ScoreDto
{
    /// <summary>
    /// The score
    /// </summary>
    public int Score { get; set; }

    /// <summary>
    /// The game
    /// </summary>
    public Game Game { get; set; }
}
