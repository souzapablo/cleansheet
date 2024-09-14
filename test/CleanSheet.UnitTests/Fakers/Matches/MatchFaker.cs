using Bogus;
using CleanSheet.Domain.Entities;
using CleanSheet.Domain.Enums;
using CleanSheet.UnitTests.Fakers.Opponents;
using CleanSheet.UnitTests.Fakers.Teams;

namespace CleanSheet.UnitTests.Fakers.Matches;
public static class MatchFaker
{
    public static Match CreateMatch() =>
        new Faker<Match>()
            .CustomInstantiator(f => new Match(
                OpponentFaker.CreateFaker(),
                f.PickRandom<MatchLocation>(),
                DateOnly.FromDateTime(f.Date.Past()),
                f.PickRandom<Competition>()
            ))
            .RuleFor(m => m.TeamId, f => f.Random.Long(1))
            .RuleFor(m => m.Team, f => TeamFaker.CreateFaker())
            .RuleFor(m => m.HomeGoals, f => f.Random.Int(0, 5))
            .RuleFor(m => m.AwayGoals, f => f.Random.Int(0, 5))
            .RuleFor(m => m.Result, f => f.PickRandom<MatchResult>())
            .RuleFor(m => m.Stadium, f => f.Address.StreetName())
            .Generate();

    public static Match CreateHomeMatch() =>
        new Faker<Match>()
            .CustomInstantiator(f => new Match(
                OpponentFaker.CreateFaker(),
                MatchLocation.Home,
                DateOnly.FromDateTime(f.Date.Past()),
                f.PickRandom<Competition>()
            ))
            .RuleFor(m => m.TeamId, f => f.Random.Long(1))
            .RuleFor(m => m.Team, f => TeamFaker.CreateFaker())
            .RuleFor(m => m.HomeGoals, f => f.Random.Int(0, 5))
            .RuleFor(m => m.AwayGoals, f => f.Random.Int(0, 5))
            .RuleFor(m => m.Result, f => f.PickRandom<MatchResult>())
            .RuleFor(m => m.Stadium, f => f.Address.StreetName())
            .Generate();

    public static Match CreateAwayMatch() =>
        new Faker<Match>()
            .CustomInstantiator(f => new Match(
                OpponentFaker.CreateFaker(),
                MatchLocation.Away,
                DateOnly.FromDateTime(f.Date.Past()),
                f.PickRandom<Competition>()
            ))
            .RuleFor(m => m.TeamId, f => f.Random.Long(1))
            .RuleFor(m => m.Team, f => TeamFaker.CreateFaker())
            .RuleFor(m => m.HomeGoals, f => f.Random.Int(0, 5))
            .RuleFor(m => m.AwayGoals, f => f.Random.Int(0, 5))
            .RuleFor(m => m.Result, f => f.PickRandom<MatchResult>())
            .RuleFor(m => m.Stadium, f => f.Address.StreetName())
            .Generate();

    public static Match CreateNeutralMatch() =>
        new Faker<Match>()
            .CustomInstantiator(f => new Match(
                OpponentFaker.CreateFaker(),
                MatchLocation.Neutral,
                DateOnly.FromDateTime(f.Date.Past()),
                f.PickRandom<Competition>()
            ))
            .RuleFor(m => m.TeamId, f => f.Random.Long(1))
            .RuleFor(m => m.Team, f => TeamFaker.CreateFaker())
            .RuleFor(m => m.HomeGoals, f => f.Random.Int(0, 5))
            .RuleFor(m => m.AwayGoals, f => f.Random.Int(0, 5))
            .RuleFor(m => m.Result, f => f.PickRandom<MatchResult>())
            .RuleFor(m => m.Stadium, f => f.Address.StreetName())
            .Generate();
}
