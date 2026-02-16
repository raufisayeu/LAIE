using LAIE.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LAIE.Application.Abstractions;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Decision> Decisions { get; }
    DbSet<EmotionalLog> EmotionalLogs { get; }
    DbSet<HabitEntry> HabitEntries { get; }
    DbSet<ScoreSnapshot> ScoreSnapshots { get; }
    DbSet<RiskProfile> RiskProfiles { get; }
    DbSet<SimulationRun> SimulationRuns { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
