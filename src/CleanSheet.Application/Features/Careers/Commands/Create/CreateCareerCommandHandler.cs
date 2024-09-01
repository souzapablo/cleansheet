using CleanSheet.Domain.Abstractions;
using CleanSheet.Domain.Entities;
using CleanSheet.Domain.Errors;
using CleanSheet.Domain.Repositories;
using MediatR;

namespace CleanSheet.Application.Features.Careers.Commands.Create;
public class CreateCareerCommandHandler(
    ICareerRepository careerRepository,
    IUserRepository userRepository,
    IInitialTeamRepository initialTeamRepository) : IRequestHandler<CreateCareerCommand, Result<long>>
{
    public async Task<Result<long>> Handle(CreateCareerCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
            return Result.Failure<long>(UserErrors.NotFound);

        var initialTeam = await initialTeamRepository
            .GetBySlugAsync(request.InitialTeam, cancellationToken);

        if (initialTeam is null)
            return Result.Failure<long>(InitialTeamErrors.NotFound);

        var manager = new Manager(request.ManagerFirstName, request.ManagerLastName);

        var newCareerResult = Career.Create(manager, initialTeam);

        if (!newCareerResult.IsSuccess)
            return Result.Failure<long>(newCareerResult.Error);

        user.AddCareer(newCareerResult.Value);

        await careerRepository.AddAsync(newCareerResult.Value, cancellationToken);

        return newCareerResult.Value.Id;
    }
}
