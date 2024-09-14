using CleanSheet.Domain.Entities;
using CleanSheet.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace CleanSheet.IntegrationTests;
public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:16.3-alpine3.20")
        .WithDatabase("CleanSheet-Dev")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CleanSheetDbContext>();
        await dbContext.Database.MigrateAsync();
        await SeedDatabaseAsync(dbContext);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services
                .SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<CleanSheetDbContext>));

            if (descriptor is not null)
                services.Remove(descriptor);

            services.AddDbContext<CleanSheetDbContext>(options =>
            {
                options
                    .UseNpgsql(_dbContainer.GetConnectionString());
            });
        });
    }

    Task IAsyncLifetime.DisposeAsync()
    {
        return _dbContainer.StopAsync();
    }

    private static async Task SeedDatabaseAsync(CleanSheetDbContext dbContext)
    {
        List<InitialTeam> initialTeams =
        [
            Data.PopulatedInitialTeam,
            Data.EmptyInitialTeam
        ];
        
        List<User> users =
        [
            Data.UserWithCareers,
            Data.UserWithNoCareers
        ];

        Data.UserWithCareers.AddCareer(Data.Career);

        Data.Team.AddOpponent(Data.Opponent);

        Data.Career.AddTeam(Data.Team);

        if (!dbContext.InitialTeams.Any())
        {
            dbContext.InitialTeams.AddRange(initialTeams);
        }

        if (!dbContext.Users.Any())
        {
            dbContext.Users.AddRange(users);
        }

        if (!dbContext.Teams.Any())
        {
            dbContext.Teams.Add(Data.Team);
        }

        await dbContext.SaveChangesAsync();
    }
}
