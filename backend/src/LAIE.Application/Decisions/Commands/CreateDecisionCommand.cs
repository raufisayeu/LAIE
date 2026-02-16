using LAIE.Application.Abstractions;
using LAIE.Domain.Entities;
using LAIE.Domain.Enums;
using MediatR;

namespace LAIE.Application.Decisions.Commands;

public sealed record CreateDecisionCommand(
    Guid UserId,
    string Title,
    string Context,
    DecisionCategory Category,
    decimal ImpactWeight,
    decimal Confidence,
    decimal EmotionalIntensity,
    decimal OutcomeScore) : IRequest<Guid>;

public sealed class CreateDecisionCommandHandler : IRequestHandler<CreateDecisionCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateDecisionCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreateDecisionCommand request, CancellationToken cancellationToken)
    {
        var decision = new Decision(
            request.UserId,
            request.Title,
            request.Context,
            request.Category,
            request.ImpactWeight,
            request.Confidence,
            request.EmotionalIntensity,
            request.OutcomeScore);

        await _dbContext.Decisions.AddAsync(decision, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return decision.Id;
    }
}
