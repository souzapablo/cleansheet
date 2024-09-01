using CleanSheet.Application.Features.Auth.Commands.Login;
using CleanSheet.Application.Features.Users.Commands.Create;
using CleanSheet.Domain.Abstractions;
using CleanSheet.Domain.Errors;
using FluentAssertions;

namespace CleanSheet.IntegrationTests.Login;

public class LoginTests(IntegrationTestWebAppFactory factory) 
    : BaseIntegrationTest(factory)
{
    [Fact(DisplayName = "Login should return token given valid credentials")]
    public async Task Login_ShouldReturn_Token_GivenValidCredentials()
    {
        // Arrange
        var user = await Sender.Send(new CreateUserCommand("login@test.com", "Test@1234"));
        var command = new LoginCommand("login@test.com", "Test@1234");

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().Be(Error.None);
        result.Value?.Token.Should().NotBeNull();
    }

    [Theory(DisplayName = "Login should return error given invalid credentials")]
    [InlineData("wrongemail@test.com", "Test@1234", "loginfailure@test.com", "Test@1234")]
    [InlineData("wrongpassword@test.com", "Test@1234", "wrongpassword@test.com", "Test@123")]
    public async Task Login_ShouldReturn_Error_GivenInvalidCredentials(string email, string password, string emailLogin, string loginPassword)
    {
        // Arrange
        var user = await Sender.Send(new CreateUserCommand(email, password));
        var command = new LoginCommand(emailLogin, loginPassword);

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(AuthErrors.InvalidCredentials);
    }
}
