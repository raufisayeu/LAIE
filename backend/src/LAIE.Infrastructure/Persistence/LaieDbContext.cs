using LAIE.Application.Abstractions;
using LAIE.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LAIE.Infrastructure.Persistence;

public sealed class LaieDbContext : DbContext, IApplicationDbContext
{
    public LaieDbContext(DbContextOptions<LaieDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<Decision> Decisions => Set<Decision>();
    public DbSet<EmotionalLog> EmotionalLogs => Set<EmotionalLog>();
    public DbSet<HabitEntry> HabitEntries => Set<HabitEntry>();
    public DbSet<GoalTarget> GoalTargets => Set<GoalTarget>();
    public DbSet<ScoreSnapshot> ScoreSnapshots => Set<ScoreSnapshot>();
    public DbSet<RiskProfile> RiskProfiles => Set<RiskProfile>();
    public DbSet<SimulationRun> SimulationRuns => Set<SimulationRun>();
    public DbSet<SystemAuditLog> SystemAuditLogs => Set<SystemAuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LaieDbContext).Assembly);
        modelBuilder.Entity<UserRole>().HasKey(x => new { x.UserId, x.RoleId });
    }
}
