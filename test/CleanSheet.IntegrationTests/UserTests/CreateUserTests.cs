using CleanSheet.Application.Features.Users.Commands.Create;
using FluentAssertions;

namespace CleanSheet.IntegrationTests.UserTests;
public class CreateUserTests : BaseIntegrationTest
{
    public CreateUserTests(IntegrationTestWebAppFactory factory)
        : base(factory) { }

    [Fact]
    public async Task Create_ShouldAdd_NewUserToDatabase()
    {
        // Arrange
        var command = new CreateUserCommand("teste@newuser.com", "Creating@n3wuser");

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().BeNull(); 
        result.Value.Should().BeGreaterThan(0);
    }
}
 