using CleanSheet.Application.Features.Careers.Commands.Create;
using CleanSheet.Application.Features.Users.Commands.Create;
using CleanSheet.Domain.Abstractions;
using CleanSheet.Domain.Errors;
using FluentAssertions;

namespace CleanSheet.IntegrationTests.Career;
public class CreateCareerTests(IntegrationTestWebAppFactory factory)
    : BaseIntegrationTest(factory)
{
    [Fact(DisplayName = "Create should add a new career to database")]
    public async Task Create_ShouldAdd_NewCareerToDatabase()
    {
        // Arrange
        var user = await Sender.Send(new CreateUserCommand("create@manager.com", "Creating@n3wmanager"));
        var command = new CreateCareerCommand(user.Value, "Manager");

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().BeNull();
        result.Value.Should().BeGreaterThan(0);
    }

    [Theory(DisplayName = "Create should return error given invalid input")]
    [InlineData("create@manager2.com", "m")]
    [InlineData("create@manager3.com", "manager input is too long and career will not be created")]
    public async Task Create_ShouldReturnError_GivenInvalidInput(string email, string manager)
    {
        // Arrange
        var user = await Sender.Send(new CreateUserCommand(email, "Creating@n3wmanager"));
        var command = new CreateCareerCommand(user.Value, manager);

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(IValidationResult.ValidationError);
        result.Value.Should().Be(default);
    }

    [Fact(DisplayName = "Create should return error given user is not found")]
    public async Task Create_ShouldReturn_UserNotFound_GivenUserNotFound()
    {
        // Arrange
        var command = new CreateCareerCommand(-1, "Manager");

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(UserErrors.UserNotFound);
        result.Value.Should().Be(0);
    }
}