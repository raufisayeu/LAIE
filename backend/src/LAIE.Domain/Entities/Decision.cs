using LAIE.Domain.Common;
using LAIE.Domain.Enums;

namespace LAIE.Domain.Entities;

public sealed class Decision : BaseEntity
{
    public Guid UserId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Context { get; private set; } = string.Empty;
    public DecisionCategory Category { get; private set; }
    public decimal ImpactWeight { get; private set; }
    public decimal Confidence { get; private set; }
    public decimal EmotionalIntensity { get; private set; }
    public decimal OutcomeScore { get; private set; }

    private Decision() { }

    public Decision(Guid userId, string title, string context, DecisionCategory category,
        decimal impactWeight, decimal confidence, decimal emotionalIntensity, decimal outcomeScore)
    {
        UserId = userId;
        Title = title;
        Context = context;
        Category = category;
        ImpactWeight = impactWeight;
        Confidence = confidence;
        EmotionalIntensity = emotionalIntensity;
        OutcomeScore = outcomeScore;
    }
}
