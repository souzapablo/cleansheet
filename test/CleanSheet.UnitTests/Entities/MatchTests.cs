using CleanSheet.Domain.Abstractions;
using CleanSheet.Domain.Errors;
using CleanSheet.UnitTests.Fakers.Matches;
using CleanSheet.UnitTests.Fakers.Teams;
using FluentAssertions;

namespace CleanSheet.UnitTests.Entities;
public class MatchTests
{
    [Fact(DisplayName = "Set match stadium to home")]
    public void Match_Should_Set_Stadium_Home()
    {
        // Arrange
        var match = MatchFaker.CreateHomeMatch();
        var team = TeamFaker.CreateFaker();

        // Act
        var result = match.DetermineStadium(team);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().Be(Error.None);
        match.Stadium.Should().Be(team.Stadium);
    }

    [Fact(DisplayName = "Set match stadium to away")]
    public void Match_Should_Set_Stadium_Away()
    {
        // Arrange
        var match = MatchFaker.CreateAwayMatch();
        var team = TeamFaker.CreateFaker();

        // Act
        var result = match.DetermineStadium(team);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().Be(Error.None);
        match.Stadium.Should().Be(match.Opponent.Stadium);
    }

    [Fact(DisplayName = "Set match stadium to neutral")]
    public void Match_Should_Set_Stadium_Neutral()
    {
        // Arrange
        var match = MatchFaker.CreateNeutralMatch();
        var team = TeamFaker.CreateFaker();

        // Act
        var result = match.DetermineStadium(team, "Neutral Stadium");

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().Be(Error.None);
        match.Stadium.Should().Be("Neutral Stadium");
    }

    [Fact(DisplayName = "Fail to set stadium")]
    public void Match_Should_Failt_To_Set_Stadium()
    {
        // Arrange
        var match = MatchFaker.CreateNeutralMatch();
        var team = TeamFaker.CreateFaker();

        // Act
        var result = match.DetermineStadium(team);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(MatchErrors.UndefinedNeutralStadium);
    }
}
