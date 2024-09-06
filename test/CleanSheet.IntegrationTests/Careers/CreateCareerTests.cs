using CleanSheet.Application.Features.Careers.Commands.Create;
using CleanSheet.Domain.Abstractions;
using CleanSheet.Domain.Errors;
using FluentAssertions;

namespace CleanSheet.IntegrationTests.Careers;
public class CreateCareerTests(IntegrationTestWebAppFactory factory)
    : BaseIntegrationTest(factory)
{
    [Theory(DisplayName = "Create a new career to an user")]
    [InlineData("123456789123", "Manager")]
    [InlineData("Mister", "123456789123")]
    public async Task Create_ShouldAdd_NewCareerToDatabase(string firstName, string lastName)
    {
        // Arrange
        var command = new CreateCareerCommand(Data.UserWithCareers.Id, firstName, lastName, Data.PopulatedInitialTeam.Slug);

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().Be(Error.None);
        result.Value.Should().BeGreaterThan(0);
    }

    [Theory(DisplayName = "Fail to create manager with big and small names")]
    [InlineData("m", "m")]
    [InlineData("mister", "m")]
    [InlineData("marvellouslyd", "manager")]
    [InlineData("mister", "managermanager")]
    public async Task Create_ShouldReturnError_GivenInvalidInput(string firstName, string lastName)
    {
        // Arrange
        var command = new CreateCareerCommand(Data.UserWithCareers.Id, firstName, lastName, Data.PopulatedInitialTeam.Slug);

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(IValidationResult.ValidationError);
    }

    [Fact(DisplayName = "Fail to find user")]
    public async Task Create_ShouldReturn_UserNotFound_GivenUserNotFound()
    {
        // Arrange
        var command = new CreateCareerCommand(Data.InvalidId, "Mister", "Manager", "initial-team");

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(UserErrors.NotFound);
    }

    [Fact(DisplayName = "Fail to find initial team")]
    public async Task Create_ShouldReturn_InitialTeamNotFound_GivenUserNotFound()
    {
        // Arrange
        var command = new CreateCareerCommand(Data.UserWithCareers.Id, "Mister", "Manager", "other-initial-team");

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(InitialTeamErrors.NotFound);
    }

    [Fact(DisplayName = "Initial team has no initial squad")]
    public async Task Create_ShouldReturn_EmptyInitialSquad_GivenEmptySquad()
    {
        // Arrange
        var command = new CreateCareerCommand(Data.UserWithCareers.Id, "Mister", "Manager", Data.EmptyInitialTeam.Slug);

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(InitialTeamErrors.EmptyInitialSquad);
    }
}