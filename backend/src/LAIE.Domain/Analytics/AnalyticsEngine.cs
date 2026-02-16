namespace LAIE.Domain.Analytics;

public interface IAnalyticsEngine
{
    AnalyticsComputationResult Compute(AnalyticsInput input);
    MonteCarloResult RunMonteCarlo(MonteCarloInput input);
}

public sealed class AnalyticsEngine : IAnalyticsEngine
{
    public AnalyticsComputationResult Compute(AnalyticsInput input)
    {
        var mean = input.DecisionScores.Average();
        var variance = input.DecisionScores.Select(x => Math.Pow(x - mean, 2)).Average();
        var standardDeviation = Math.Sqrt(variance);

        var weightedScore = input.DecisionScores
            .Zip(input.DecisionWeights, (score, weight) => score * weight)
            .Sum() / Math.Max(0.001, input.DecisionWeights.Sum());

        var riskStreak = DetectRiskStreak(input.DecisionScores, input.RiskThreshold);
        var emotionalSpikeClusters = DetectEmotionalSpikeClusters(input.EmotionalSeries, input.EmotionalSpikeThreshold);
        var stabilityDecay = CalculateStabilityDecay(input.DecisionScores, input.DecayFactor);
        var growthAcceleration = CalculateGrowthAcceleration(input.TrajectorySeries);

        return new AnalyticsComputationResult(
            weightedScore,
            mean,
            variance,
            standardDeviation,
            riskStreak,
            emotionalSpikeClusters,
            stabilityDecay,
            growthAcceleration);
    }

    public MonteCarloResult RunMonteCarlo(MonteCarloInput input)
    {
        var random = new Random(input.Seed);
        var outcomes = new List<double>(input.Runs);

        for (var run = 0; run < input.Runs; run++)
        {
            double projection = input.StartingValue;
            for (var step = 0; step < input.HorizonSteps; step++)
            {
                var noise = NextGaussian(random, input.ExpectedReturn, input.Volatility);
                projection *= 1 + noise;
            }

            outcomes.Add(projection);
        }

        outcomes.Sort();
        var p50 = outcomes[(int)(0.50 * outcomes.Count)];
        var p90 = outcomes[(int)(0.90 * outcomes.Count)];
        var confidenceIntervalLow = outcomes[(int)(0.05 * outcomes.Count)];
        var confidenceIntervalHigh = outcomes[(int)(0.95 * outcomes.Count)];

        return new MonteCarloResult(outcomes, p50, p90, confidenceIntervalLow, confidenceIntervalHigh);
    }

    private static int DetectRiskStreak(IReadOnlyList<double> scores, double threshold)
    {
        var currentStreak = 0;
        var maxStreak = 0;

        foreach (var score in scores)
        {
            if (score <= threshold)
            {
                currentStreak++;
                maxStreak = Math.Max(maxStreak, currentStreak);
            }
            else
            {
                currentStreak = 0;
            }
        }

        return maxStreak;
    }

    private static int DetectEmotionalSpikeClusters(IReadOnlyList<double> emotions, double threshold)
    {
        var clusters = 0;
        var inCluster = false;
        foreach (var value in emotions)
        {
            if (value >= threshold && !inCluster)
            {
                clusters++;
                inCluster = true;
            }
            else if (value < threshold)
            {
                inCluster = false;
            }
        }

        return clusters;
    }

    private static double CalculateStabilityDecay(IReadOnlyList<double> scores, double decayFactor)
    {
        double accumulated = 0;
        for (var i = 0; i < scores.Count; i++)
        {
            accumulated += scores[i] * Math.Exp(-decayFactor * i);
        }

        return accumulated / scores.Count;
    }

    private static double CalculateGrowthAcceleration(IReadOnlyList<double> trajectory)
    {
        if (trajectory.Count < 3) return 0;

        var velocities = new List<double>();
        for (var i = 1; i < trajectory.Count; i++)
        {
            velocities.Add(trajectory[i] - trajectory[i - 1]);
        }

        var accelerations = new List<double>();
        for (var i = 1; i < velocities.Count; i++)
        {
            accelerations.Add(velocities[i] - velocities[i - 1]);
        }

        return accelerations.Average();
    }

    private static double NextGaussian(Random random, double mean, double stdDev)
    {
        var u1 = 1.0 - random.NextDouble();
        var u2 = 1.0 - random.NextDouble();
        var randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
        return mean + stdDev * randStdNormal;
    }
}

public sealed record AnalyticsInput(
    IReadOnlyList<double> DecisionScores,
    IReadOnlyList<double> DecisionWeights,
    IReadOnlyList<double> EmotionalSeries,
    IReadOnlyList<double> TrajectorySeries,
    double RiskThreshold,
    double EmotionalSpikeThreshold,
    double DecayFactor);

public sealed record AnalyticsComputationResult(
    double DecisionWeightedScore,
    double Mean,
    double Variance,
    double StandardDeviation,
    int RiskStreak,
    int EmotionalSpikeClusters,
    double StabilityDecay,
    double GrowthAccelerationIndex);

public sealed record MonteCarloInput(
    double StartingValue,
    double ExpectedReturn,
    double Volatility,
    int Runs,
    int HorizonSteps,
    int Seed);

public sealed record MonteCarloResult(
    IReadOnlyList<double> Outcomes,
    double MedianP50,
    double UpperP90,
    double ConfidenceLow,
    double ConfidenceHigh);
