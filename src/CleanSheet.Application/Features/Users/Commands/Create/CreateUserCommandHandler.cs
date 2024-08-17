using CleanSheet.Domain.Abstractions;
using CleanSheet.Domain.Entities;
using CleanSheet.Domain.Errors;
using CleanSheet.Domain.Repositories;
using MediatR;

namespace CleanSheet.Application.Features.Users.Commands.Create;
public class CreateUserCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher) : IRequestHandler<CreateUserCommand, Result<long>>
{
    public async Task<Result<long>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByUsernameAsync(request.Username, cancellationToken);

        if (user is not null)
            return Result.Failure<long>(UserErrors.EmailAlreadyRegistered);

        var passwordHash = passwordHasher.Hash(request.Password);

        var newUser = new User(request.Username, passwordHash);

        await userRepository.AddAsync(newUser, cancellationToken);

        return Result.Success(newUser.Id);
    }
}
