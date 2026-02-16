using FluentValidation;

namespace LAIE.Application.Decisions.Commands;

public sealed class CreateDecisionCommandValidator : AbstractValidator<CreateDecisionCommand>
{
    public CreateDecisionCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(120);
        RuleFor(x => x.Context).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.ImpactWeight).InclusiveBetween(0.1m, 10m);
        RuleFor(x => x.Confidence).InclusiveBetween(0m, 1m);
        RuleFor(x => x.EmotionalIntensity).InclusiveBetween(0m, 1m);
        RuleFor(x => x.OutcomeScore).InclusiveBetween(-1m, 1m);
    }
}
