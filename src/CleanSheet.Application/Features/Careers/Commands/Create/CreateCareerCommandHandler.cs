using CleanSheet.Domain.Abstractions;
using CleanSheet.Domain.Entities;
using CleanSheet.Domain.Errors;
using CleanSheet.Domain.Repositories;
using MediatR;

namespace CleanSheet.Application.Features.Careers.Commands.Create;
public class CreateCareerCommandHandler(
    ICareerRepository careerRepository,
    IUserRepository userRepository) : IRequestHandler<CreateCareerCommand, Result<long>>
{
    public async Task<Result<long>> Handle(CreateCareerCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
            return Result.Failure<long>(UserErrors.UserNotFound);

        var career = new Career(request.Manager);

        user.AddCareer(career);

        await careerRepository.AddAsync(career, cancellationToken);

        return Result.Success(career.Id);
    }
}
