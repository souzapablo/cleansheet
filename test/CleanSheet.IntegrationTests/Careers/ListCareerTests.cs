using CleanSheet.Application.Features.Careers.Queries.List;
using CleanSheet.Domain.Abstractions;
using CleanSheet.Domain.Errors;
using FluentAssertions;

namespace CleanSheet.IntegrationTests.Careers;

public class ListCareerTests(IntegrationTestWebAppFactory factory)
    : BaseIntegrationTest(factory)
{
    [Fact(DisplayName = "List user careers")]
    public async Task List_ShouldHaveCareers_GivenUserHaveAnyCareer()
    {
        // Arrange
        var query = new ListCareersQuery(Data.UserWithCareers.Id);

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().Be(Error.None);
        result.Value.Should().NotBeEmpty();
    }

    [Fact(DisplayName = "List empty careers")]
    public async Task List_ShouldBeEmpty_GivenUserHaveNoCareer()
    {
        // Arrange
        var query = new ListCareersQuery(Data.UserWithNoCareers.Id);

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().Be(Error.None);
        result.Value.Should().BeEmpty();
    }

    [Fact(DisplayName = "Fail to find user")]
    public async Task List_ShouldReturnError_GivenUserIsNotFound()
    {
        // Arrange
        var query = new ListCareersQuery(Data.InvalidId);

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(UserErrors.NotFound);
    }
}
