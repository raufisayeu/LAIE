using LAIE.Domain.Common;

namespace LAIE.Domain.Entities;

public sealed class EmotionalLog : BaseEntity
{
    public Guid UserId { get; private set; }
    public decimal Intensity { get; private set; }
    public string Trigger { get; private set; } = string.Empty;
    public DateTime LoggedAtUtc { get; private set; }
}

public sealed class HabitEntry : BaseEntity
{
    public Guid UserId { get; private set; }
    public string HabitName { get; private set; } = string.Empty;
    public bool Completed { get; private set; }
    public decimal ConsistencyScore { get; private set; }
    public DateTime EntryDateUtc { get; private set; }
}

public sealed class GoalTarget : BaseEntity
{
    public Guid UserId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public decimal TargetValue { get; private set; }
    public DateTime DeadlineUtc { get; private set; }
}

public sealed class ScoreSnapshot : BaseEntity
{
    public Guid UserId { get; private set; }
    public decimal StabilityScore { get; private set; }
    public decimal GrowthAccelerationIndex { get; private set; }
    public decimal RiskScore { get; private set; }
}

public sealed class RiskProfile : BaseEntity
{
    public Guid UserId { get; private set; }
    public decimal Volatility { get; private set; }
    public int CurrentRiskStreak { get; private set; }
    public string Tier { get; private set; } = string.Empty;
}

public sealed class SimulationRun : BaseEntity
{
    public Guid UserId { get; private set; }
    public int NumberOfRuns { get; private set; }
    public decimal ProjectedP50 { get; private set; }
    public decimal ProjectedP90 { get; private set; }
}

public sealed class SystemAuditLog : BaseEntity
{
    public Guid? UserId { get; private set; }
    public string Action { get; private set; } = string.Empty;
    public string MetadataJson { get; private set; } = "{}";
}
