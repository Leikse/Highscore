using Highscore.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Highscore.Data;

public class HighscoreContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Score> Score { get; set; }
    public DbSet<Game> Game { get; set; }

    public HighscoreContext(DbContextOptions<HighscoreContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var administratorRole = new IdentityRole("Administrator");

        builder
            .Entity<IdentityRole>()
            .HasData(administratorRole);
    }
}

