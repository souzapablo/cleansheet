using CleanSheet.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanSheet.Infrastructure;
public class CleanSheetDbContext(
    DbContextOptions<CleanSheetDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Career> Careers { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<InitialTeam> InitialTeams { get; set; }
    public DbSet<Match> Matches { get; set; }
    public DbSet<Opponent> Opponents { get; set; }
    public DbSet<Goal> Goals { get; set; }
    public DbSet<Assist> Assists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = typeof(CleanSheetDbContext).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
}
