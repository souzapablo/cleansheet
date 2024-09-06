using CleanSheet.Domain.Entities;
using CleanSheet.Domain.Enums;
using CleanSheet.Infrastructure.Services;

namespace CleanSheet.IntegrationTests;
public static class Data
{
    public static PasswordHasher Hasher = new();

    public static long InvalidId { get; } = -1;
    public static string UserPassword { get; } = "Test@1234";
    public static User UserWithCareers { get; } = new User("test@user.com", Hasher.Hash(UserPassword));
    public static User UserWithNoCareers { get; } = new User("testNew@user.com", Hasher.Hash(UserPassword));

    public static InitialTeam PopulatedInitialTeam { get; } = new InitialTeam("test-team", "Test Team Arena", "Test Team",
        [
            new("Test Player 1", 1, 80, new DateOnly(2000, 09, 12), PlayerPosition.Gk),
            new("Test Player 1", 10, 99, new DateOnly(1980, 12, 12), PlayerPosition.Cam),
            new("Test Player 1", 11, 80, new DateOnly(2004, 05, 15), PlayerPosition.St),
        ]);
    public static InitialTeam EmptyInitialTeam { get; } = new("empty-test-team", "Empty Test Team Arena", "Empty Test Team", []);


    public static Manager CareerManager { get; } = new("Mister", "Manager");
    public static Career Career { get; } = Career.Create(CareerManager, PopulatedInitialTeam).Value;
}
