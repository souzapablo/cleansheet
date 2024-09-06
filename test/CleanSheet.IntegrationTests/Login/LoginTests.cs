using CleanSheet.Application.Features.Auth.Commands.Login;
using CleanSheet.Domain.Abstractions;
using CleanSheet.Domain.Errors;
using FluentAssertions;

namespace CleanSheet.IntegrationTests.Login;

public class LoginTests(IntegrationTestWebAppFactory factory) 
    : BaseIntegrationTest(factory)
{
    [Fact(DisplayName = "Generate token")]
    public async Task Login_ShouldReturn_Token_GivenValidCredentials()
    {
        // Arrange
        var command = new LoginCommand(Data.UserWithCareers.Username, Data.UserPassword);

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().Be(Error.None);
        result.Value?.Token.Should().NotBeNull();
    }

    [Theory(DisplayName = "Fail to login due to invalid credentials")]
    [InlineData("loginfailure@test.com", "Test@1234")]
    [InlineData("wrongpassword@test.com", "Test@123")]
    public async Task Login_ShouldReturn_Error_GivenInvalidCredentials(string emailLogin, string loginPassword)
    {
        // Arrange
        var command = new LoginCommand(emailLogin, loginPassword);

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(AuthErrors.InvalidCredentials);
    }
}
