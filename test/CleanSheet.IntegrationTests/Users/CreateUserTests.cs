﻿using CleanSheet.Application.Features.Users.Commands.Create;
using CleanSheet.Domain.Abstractions;
using CleanSheet.Domain.Errors;
using FluentAssertions;

namespace CleanSheet.IntegrationTests.Users;
public class CreateUserTests(IntegrationTestWebAppFactory factory)
    : BaseIntegrationTest(factory)
{
    [Fact(DisplayName = "Create should add a new user to database")]
    public async Task Create_ShouldAdd_NewUserToDatabase()
    {
        // Arrange
        var command = new CreateUserCommand("test@newuser.com", "Creating@n3wuser");

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().Be(Error.None);
        result.Value.Should().BeGreaterThan(0);
    }

    [Theory(DisplayName = "Create should return error given invalid input")]
    [InlineData("test@newuser.com", "weakpassword")]
    [InlineData("test", "Strong@p4assword")]
    public async Task Create_ShouldReturnError_GivenInvalidInput(string username, string password)
    {
        // Arrange
        var command = new CreateUserCommand(username, password);

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(IValidationResult.ValidationError);
    }

    [Fact(DisplayName = "Create should return error given already registered e-mail")]
    public async Task Create_ShouldReturnError_GivenAlreadyRegisteredEmail()
    {
        // Arrange
        var command = new CreateUserCommand("test@user.com", "Creating@n3wuser");

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(UserErrors.EmailAlreadyRegistered);
    }
}
