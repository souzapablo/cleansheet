using FluentValidation;

namespace CleanSheet.Application.Features.Matches.Commands.Create;

public class CreateMatchCommandValidator : AbstractValidator<CreateMatchCommand>
{
    public CreateMatchCommandValidator()
    {
        RuleFor(x => x.Location)
            .IsInEnum()
            .WithMessage("Invalid match location.");

        RuleFor(x => x.Competition)
            .IsInEnum()
            .WithMessage("Invalid competition.");
    }
}
