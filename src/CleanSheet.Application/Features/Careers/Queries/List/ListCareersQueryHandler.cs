using CleanSheet.Application.Features.Careers.Responses;
using CleanSheet.Domain.Abstractions;
using CleanSheet.Domain.Errors;
using CleanSheet.Domain.Repositories;
using MediatR;

namespace CleanSheet.Application.Features.Careers.Queries.List;
public class ListCareersQueryHandler(
    ICareerRepository careerRepository,
    IUserRepository userRepository): IRequestHandler<ListCareersQuery, Result<List<CareerResponse>>>
{
    public async Task<Result<List<CareerResponse>>> Handle(ListCareersQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);
        
        if (user is null)
            return Result.Failure<List<CareerResponse>>(UserErrors.NotFound);

        var careers = await careerRepository.GetByUserIdAsync(request.UserId, cancellationToken);

        return careers.Select(x =>
        new CareerResponse(
            $"{x.Manager.FirstName} {x.Manager.LastName}",
            x.Teams.OrderByDescending(t => t.Id)
                .FirstOrDefault()?.Name,
            x.LastUpdate))
            .ToList();
    }
}