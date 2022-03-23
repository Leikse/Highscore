using Highscore.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Highscore.Data;

public class HighscoreContext : DbContext
{
    public DbSet<Score> Score { get; set; }
    public DbSet<Game> Game { get; set; }

    public HighscoreContext(DbContextOptions<HighscoreContext> options) : base(options)
    {

    }
}

