using CleanSheet.Application.Features.Careers.Queries.List;
using CleanSheet.Domain.Abstractions;
using CleanSheet.Domain.Errors;
using FluentAssertions;

namespace CleanSheet.IntegrationTests.Careers;

public class ListCareerTests(IntegrationTestWebAppFactory factory)
    : BaseIntegrationTest(factory)
{
    [Fact(DisplayName = "List careers when user have any career")]
    public async Task List_ShouldHaveCareers_GivenUserHaveAnyCareer()
    {
        // Arrange
        var query = new ListCareersQuery(1);

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().Be(Error.None);
        result.Value.Should().NotBeEmpty();
    }

    [Fact(DisplayName = "List should be empty when user have no careers")]
    public async Task List_ShouldBeEmpty_GivenUserHaveNoCareer()
    {
        // Arrange
        var query = new ListCareersQuery(2);

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().Be(Error.None);
        result.Value.Should().BeEmpty();
    }

    [Fact(DisplayName = "Error given user is not registered")]
    public async Task List_ShouldReturnError_GivenUserIsNotFound()
    {
        // Arrange
        var query = new ListCareersQuery(-1);

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(UserErrors.NotFound);
    }
}
