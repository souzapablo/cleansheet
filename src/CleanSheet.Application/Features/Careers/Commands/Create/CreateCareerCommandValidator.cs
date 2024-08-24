using FluentValidation;

namespace CleanSheet.Application.Features.Careers.Commands.Create;
public class CreateCareerCommandValidator : AbstractValidator<CreateCareerCommand>
{
    public CreateCareerCommandValidator()
    {
        RuleFor(x => x.ManagerFirstName)
            .Length(2, 12).WithMessage("Manager first name must be between 2 and 12 characters.");

        RuleFor(x => x.ManagerLastName)
            .Length(2, 12).WithMessage("Manager last name must be between 2 and 12 characters.");
    }
}
