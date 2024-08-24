using CleanSheet.Application.Features.Careers.Commands.Create;
using CleanSheet.Application.Features.Users.Commands.Create;
using CleanSheet.Domain.Abstractions;
using CleanSheet.Domain.Errors;
using FluentAssertions;

namespace CleanSheet.IntegrationTests.Career;
public class CreateCareerTests(IntegrationTestWebAppFactory factory)
    : BaseIntegrationTest(factory)
{
    [Theory(DisplayName = "Create should add a new career to database")]
    [InlineData("123456789123@manager.com", "123456789123", "Manager")]
    [InlineData("mister123456789123@manager.com", "Mister", "123456789123")]
    public async Task Create_ShouldAdd_NewCareerToDatabase(string email, string firstName, string lastName)
    {
        // Arrange
        var user = await Sender.Send(new CreateUserCommand(email, "Creating@n3wmanager"));
        var command = new CreateCareerCommand(user.Value, firstName, lastName);

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().BeNull();
        result.Value.Should().BeGreaterThan(0);
    }

    [Theory(DisplayName = "Create should return error given invalid input")]
    [InlineData("mm@manager.com", "m", "m")]
    [InlineData("misterm@manager.com", "mister", "m")]
    [InlineData("marvellouslydmanager@manager.com", "marvellouslyd", "manager")]
    [InlineData("mistermanagermanager@manager.com", "mister", "managermanager")]
    public async Task Create_ShouldReturnError_GivenInvalidInput(string email, string firstName, string lastName)
    {
        // Arrange
        var user = await Sender.Send(new CreateUserCommand(email, "Creating@n3wmanager"));
        var command = new CreateCareerCommand(user.Value, firstName, lastName);

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
        var command = new CreateCareerCommand(-1, "Mister", "Manager");

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(UserErrors.UserNotFound);
        result.Value.Should().Be(0);
    }
}