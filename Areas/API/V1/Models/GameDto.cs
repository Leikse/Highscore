using System.ComponentModel.DataAnnotations;

namespace Highscore.Areas.API.V1.Models;

/// <summary>
/// A game
/// </summary>
public class GameDto
{
    /// <summary>
    /// Game ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Game title
    /// </summary>
    [Required]
    public string Name { get; set; }
}
