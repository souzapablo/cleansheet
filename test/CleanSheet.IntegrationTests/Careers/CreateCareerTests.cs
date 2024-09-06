using CleanSheet.Application.Features.Careers.Commands.Create;
using CleanSheet.Domain.Abstractions;
using CleanSheet.Domain.Errors;
using FluentAssertions;

namespace CleanSheet.IntegrationTests.Careers;
public class CreateCareerTests(IntegrationTestWebAppFactory factory)
    : BaseIntegrationTest(factory)
{
    [Theory(DisplayName = "Create should add a new career to database")]
    [InlineData("123456789123", "Manager")]
    [InlineData("Mister", "123456789123")]
    public async Task Create_ShouldAdd_NewCareerToDatabase(string firstName, string lastName)
    {
        // Arrange
        var command = new CreateCareerCommand(1, firstName, lastName, "test-team");

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().Be(Error.None);
        result.Value.Should().BeGreaterThan(0);
    }

    [Theory(DisplayName = "Create should return error given invalid input")]
    [InlineData("m", "m")]
    [InlineData("mister", "m")]
    [InlineData("marvellouslyd", "manager")]
    [InlineData("mister", "managermanager")]
    public async Task Create_ShouldReturnError_GivenInvalidInput(string firstName, string lastName)
    {
        // Arrange
        var command = new CreateCareerCommand(1, firstName, lastName, "test-team");

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(IValidationResult.ValidationError);
    }

    [Fact(DisplayName = "Create should return error given user is not found")]
    public async Task Create_ShouldReturn_UserNotFound_GivenUserNotFound()
    {
        // Arrange
        var command = new CreateCareerCommand(-1, "Mister", "Manager", "initial-team");

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(UserErrors.NotFound);
    }

    [Fact(DisplayName = "Create should return error given initial team is not found")]
    public async Task Create_ShouldReturn_InitialTeamNotFound_GivenUserNotFound()
    {
        // Arrange
        var command = new CreateCareerCommand(1, "Mister", "Manager", "other-initial-team");

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(InitialTeamErrors.NotFound);
    }

    [Fact(DisplayName = "Create should return error given initial team is not found")]
    public async Task Create_ShouldReturn_EmptyInitialSquad_GivenEmptySquad()
    {
        // Arrange
        var command = new CreateCareerCommand(1, "Mister", "Manager", "empty-test-team");

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(InitialTeamErrors.EmptyInitialSquad);
    }
}