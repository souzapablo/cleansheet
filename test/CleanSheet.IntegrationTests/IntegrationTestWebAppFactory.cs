using CleanSheet.Domain.Entities;
using CleanSheet.Domain.Enums;
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
            new("test-team", "Test Team Arena", "Test Team",
            [
                new("Test Player 1", 1, 80, new DateOnly(2000, 09, 12), PlayerPosition.Gk),
                new("Test Player 1", 10, 99, new DateOnly(1980, 12, 12), PlayerPosition.Cam),
                new("Test Player 1", 11, 80, new DateOnly(2004, 05, 15), PlayerPosition.St),
            ]),
            new("empty-test-team", "Empty Test Team Arena", "Empty Test Team", [])
        ];

        List<User> users = [new User("test@user.com", "Test@1234"), new User("testNew@user.com", "Test@1234")];

        var career = Career.Create(new Manager("Mister", "Manager"), initialTeams[0]);

        users[0].AddCareer(career.Value);

        if (!dbContext.InitialTeams.Any())
        {
            dbContext.InitialTeams.AddRange(initialTeams);
        }

        if (!dbContext.Users.Any())
        {
            dbContext.Users.AddRange(users);
        }

        await dbContext.SaveChangesAsync();
    }
}
