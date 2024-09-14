using CleanSheet.Application.Features.Matches.Commands.Create;
using CleanSheet.Domain.Abstractions;
using CleanSheet.Domain.Enums;
using CleanSheet.Domain.Errors;
using FluentAssertions;

namespace CleanSheet.IntegrationTests.Matches;
public class CreateMatchTests(IntegrationTestWebAppFactory factory)
    : BaseIntegrationTest(factory)
{
    [Fact(DisplayName = "Create a new home match")]
    public async Task Create_Should_AddNewHomeMatchToDatabase()
    {
        // Arrange
        var command = new CreateMatchCommand(Data.Team.Id, Data.Opponent.Id, MatchLocation.Home, new DateOnly(2024,09,12), Competition.Friendly, null);

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().Be(Error.None);
        result.Value.Should().BeGreaterThan(0);
    }

    [Fact(DisplayName = "Create a new away match")]
    public async Task Create_Should_AddNewAwayMatchToDatabase()
    {
        // Arrange
        var command = new CreateMatchCommand(Data.Team.Id, Data.Opponent.Id, MatchLocation.Away, new DateOnly(2024, 09, 12), Competition.Friendly, null);

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().Be(Error.None);
        result.Value.Should().BeGreaterThan(0);
    }

    [Fact(DisplayName = "Create a new neutral match")]
    public async Task Create_Should_AddNewNeutralMatchToDatabase()
    {
        // Arrange
        var command = new CreateMatchCommand(Data.Team.Id, Data.Opponent.Id, MatchLocation.Neutral, new DateOnly(2024, 09, 12), Competition.Friendly, "Wembley");

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().Be(Error.None);
        result.Value.Should().BeGreaterThan(0);
    }

    [Fact(DisplayName = "Fail to create match for non existing team")]
    public async Task Create_Should_ReturnError_When_TeamNotFound()
    {
        // Arrange
        var command = new CreateMatchCommand(Data.InvalidId, Data.Opponent.Id, MatchLocation.Home, new DateOnly(2024, 09, 12), Competition.Friendly, null);

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(TeamErrors.NotFound);
    }

    [Fact(DisplayName = "Fail to create match for non existing opponent")]
    public async Task Create_Should_ReturnError_When_OpponentNotFound()
    {
        // Arrange
        var command = new CreateMatchCommand(Data.Team.Id, Data.InvalidId, MatchLocation.Home, new DateOnly(2024, 09, 12), Competition.Friendly, null);

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(OpponentErrors.NotFound);
    }

    [Fact(DisplayName = "Fail to create match with empty neutral stadium")]
    public async Task Create_Should_ReturnError_When_StadiumIsEmptyAndMatchLocationIsNeutral()
    {
        // Arrange
        var command = new CreateMatchCommand(Data.Team.Id, Data.Opponent.Id, MatchLocation.Neutral, new DateOnly(2024, 09, 12), Competition.Friendly, null);

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(MatchErrors.UndefinedNeutralStadium);
    }

    [Theory(DisplayName = "Fail to create match with invalid input")]
    [InlineData(int.MinValue, int.MinValue)]
    [InlineData(int.MinValue, 1)]
    [InlineData(2, int.MinValue)]
    public async Task Create_Should_ReturnError_When_InputIsInvalid(int location, int competition)
    {
        // Arrange
        var command = new CreateMatchCommand(Data.Team.Id, Data.Opponent.Id, (MatchLocation) location, new DateOnly(2024, 09, 12), (Competition) competition, null);

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(IValidationResult.ValidationError);
    }
}
