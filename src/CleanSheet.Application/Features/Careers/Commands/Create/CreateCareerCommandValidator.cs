using FluentValidation;

namespace CleanSheet.Application.Features.Careers.Commands.Create;
public class CreateCareerCommandValidator : AbstractValidator<CreateCareerCommand>
{
    public CreateCareerCommandValidator()
    {
        RuleFor(x => x.Manager)
            .Length(2, 12).WithMessage("Manager fisrt name must be between 2 and 12 characters.");
    }
}
