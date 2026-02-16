using LAIE.Application.Abstractions;
using LAIE.Domain.Analytics;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LAIE.Application.Analytics.Queries;

public sealed record GetAnalyticsSnapshotQuery(Guid UserId) : IRequest<AnalyticsSnapshotDto>;

public sealed class GetAnalyticsSnapshotQueryHandler : IRequestHandler<GetAnalyticsSnapshotQuery, AnalyticsSnapshotDto>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IAnalyticsEngine _analyticsEngine;

    public GetAnalyticsSnapshotQueryHandler(IApplicationDbContext dbContext, IAnalyticsEngine analyticsEngine)
    {
        _dbContext = dbContext;
        _analyticsEngine = analyticsEngine;
    }

    public async Task<AnalyticsSnapshotDto> Handle(GetAnalyticsSnapshotQuery request, CancellationToken cancellationToken)
    {
        var decisions = await _dbContext.Decisions
            .Where(d => d.UserId == request.UserId)
            .OrderBy(d => d.CreatedAtUtc)
            .ToListAsync(cancellationToken);

        var emotions = await _dbContext.EmotionalLogs
            .Where(e => e.UserId == request.UserId)
            .OrderBy(e => e.LoggedAtUtc)
            .Select(e => (double)e.Intensity)
            .ToListAsync(cancellationToken);

        var scores = decisions.Select(d => (double)d.OutcomeScore).ToList();
        var weights = decisions.Select(d => (double)d.ImpactWeight).ToList();
        var trajectory = decisions.Select((d, index) => (double)d.OutcomeScore * (index + 1)).ToList();

        var computed = _analyticsEngine.Compute(new AnalyticsInput(
            scores,
            weights,
            emotions,
            trajectory,
            RiskThreshold: -0.2,
            EmotionalSpikeThreshold: 0.8,
            DecayFactor: 0.08));

        var monteCarlo = _analyticsEngine.RunMonteCarlo(new MonteCarloInput(
            StartingValue: 100,
            ExpectedReturn: 0.015,
            Volatility: Math.Max(0.01, computed.StandardDeviation / 10),
            Runs: 1200,
            HorizonSteps: 52,
            Seed: request.UserId.GetHashCode()));

        return new AnalyticsSnapshotDto(
            computed.DecisionWeightedScore,
            computed.StandardDeviation,
            computed.RiskStreak,
            computed.EmotionalSpikeClusters,
            computed.StabilityDecay,
            computed.GrowthAccelerationIndex,
            monteCarlo.MedianP50,
            monteCarlo.UpperP90,
            monteCarlo.ConfidenceLow,
            monteCarlo.ConfidenceHigh);
    }
}

public sealed record AnalyticsSnapshotDto(
    double DecisionWeightedScore,
    double Volatility,
    int RiskStreak,
    int EmotionalSpikeClusters,
    double StabilityDecay,
    double GrowthAccelerationIndex,
    double ProjectionP50,
    double ProjectionP90,
    double ConfidenceLow,
    double ConfidenceHigh);
