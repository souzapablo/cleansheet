using CleanSheet.Application.Abstractions;
using CleanSheet.Domain.Abstractions;
using CleanSheet.Domain.Errors;
using CleanSheet.Domain.Repositories;
using MediatR;

namespace CleanSheet.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler(
    IUserRepository userRepository,
    IPasswordHasher passwordHasher,
    IJwtProvider jwtProvider) : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByUsernameAsync(request.Username, cancellationToken);

        if (user is null)
            return Result.Failure<LoginResponse>(AuthErrors.InvalidCredentials);

        if (!passwordHasher.Verify(request.Password, user.Password))
            return Result.Failure<LoginResponse>(AuthErrors.InvalidCredentials);

        var token = jwtProvider.Generate(user);

        return new LoginResponse(token);
    }
}
