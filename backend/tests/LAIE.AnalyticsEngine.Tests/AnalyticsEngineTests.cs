using FluentAssertions;
using LAIE.Domain.Analytics;
using Xunit;

namespace LAIE.AnalyticsEngine.Tests;

public sealed class AnalyticsEngineTests
{
    private readonly global::LAIE.Domain.Analytics.AnalyticsEngine _engine = new();

    [Fact]
    public void Compute_Should_ReturnExpectedStatisticalMetrics()
    {
        var input = new AnalyticsInput(
            DecisionScores: new[] { -0.4, 0.2, 0.1, -0.5, 0.7, 0.8 },
            DecisionWeights: new[] { 1.5, 2.0, 0.8, 1.2, 2.2, 1.7 },
            EmotionalSeries: new[] { 0.1, 0.88, 0.91, 0.2, 0.83, 0.9, 0.3 },
            TrajectorySeries: new[] { 5.0, 5.6, 6.9, 8.9, 12.5 },
            RiskThreshold: -0.2,
            EmotionalSpikeThreshold: 0.8,
            DecayFactor: 0.12);

        var result = _engine.Compute(input);

        result.StandardDeviation.Should().BeGreaterThan(0);
        result.RiskStreak.Should().Be(1);
        result.EmotionalSpikeClusters.Should().Be(2);
        result.GrowthAccelerationIndex.Should().BePositive();
    }

    [Fact]
    public void RunMonteCarlo_Should_ProduceConsistentQuantiles()
    {
        var simulation = _engine.RunMonteCarlo(new MonteCarloInput(
            StartingValue: 100,
            ExpectedReturn: 0.02,
            Volatility: 0.08,
            Runs: 1500,
            HorizonSteps: 60,
            Seed: 42));

        simulation.Outcomes.Count.Should().Be(1500);
        simulation.UpperP90.Should().BeGreaterThan(simulation.MedianP50);
        simulation.ConfidenceHigh.Should().BeGreaterThan(simulation.ConfidenceLow);
    }
}
