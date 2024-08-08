using FluentValidation;

namespace CleanSheet.Application.Features.Users.Commands.Create;
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("E-mail is required.")
            .EmailAddress().WithMessage("E-mail is invalid.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
            .WithMessage("Password must be at least 8 characters long and include at least one uppercase letter, one lowercase letter, one number, and one special character (e.g., @$!%*?&).");
    }
}
