# Life Analytics Intelligence Engine (LAIE)

Enterprise-grade SaaS MVP blueprint for behavioral analytics, risk modeling, and long-range growth projections.

## 1) Enterprise Architecture

```text
/backend
  /src
    /LAIE.Api                     -> API surface, auth, policies, swagger, middleware
    /LAIE.Application             -> CQRS commands/queries, validation, DTO orchestration
    /LAIE.Domain                  -> DDD entities/value logic + AnalyticsEngine math core
    /LAIE.Infrastructure          -> EF Core context, token services, background workers
  /tests
    /LAIE.AnalyticsEngine.Tests   -> Unit tests for scoring + simulation
/frontend
  /src
    /components                   -> Chart-driven widgets
    /pages                        -> Landing/Auth/Dashboard/Decisions/Habits/Analytics/Admin/Profile
    /services                     -> API service wrappers
    /styles                       -> Dark SaaS design system
/database
  laie-schema.sql                -> SQL Server relational schema
```

## 2) Backend Stack

- **ASP.NET Core .NET 8** (structured for Web API startup)
- **Clean Architecture + CQRS** using MediatR request handlers
- **DDD-first domain model** with behavioral entities and scoring abstractions
- **Entity Framework Core** context and configuration entry points
- **SQL Server schema** with normalized relations
- **JWT + refresh token service** and role-based policies (`AdminOnly`, `AnalystOrAdmin`)
- **Global exception middleware** with validation/error response envelope
- **FluentValidation pipeline behavior**
- **Serilog request logging**
- **Swagger/OpenAPI setup**
- **Background recalculation worker** (Hangfire-style recurring pattern)

> Optional enhancement hook: infrastructure is prepared for Redis cache registration.

## 3) Advanced AnalyticsEngine Module

`LAIE.Domain/Analytics/AnalyticsEngine.cs` includes:

1. Decision Weighted Scoring
2. Volatility Standard Deviation
3. Risk Streak Detection
4. Emotional Spike Clustering
5. Monte Carlo Projection (1200 runs in query sample)
6. Stability Decay Modeling
7. Growth Acceleration Index
8. Core statistics: mean, variance, standard deviation

All algorithms are deterministic/testable by injecting explicit inputs and seeds.

## 4) Sample API Endpoints

| Method | Route | Purpose | Security |
|---|---|---|---|
| `POST` | `/api/auth/login` | Issue JWT + refresh token | Public |
| `POST` | `/api/decisions` | Create weighted decision item | Authenticated |
| `GET` | `/api/analytics/snapshot/{userId}` | Aggregated intelligence + Monte Carlo projection | Analyst/Admin policy |
| `GET` | `/api/admin/audit-logs` | Admin audit stream access | Admin policy |

## 5) Frontend Experience (Responsive SaaS UI)

- Modular sidebar navigation
- Dynamic Chart.js panels
- Animated loading placeholders (`pulse` card)
- Dark theme token system
- Mobile breakpoints for enterprise dashboard portability

### Included Pages (8)
- Landing
- Auth
- Dashboard
- Decision Management
- Habit Tracking
- Analytics Intelligence Center
- Admin Control Panel
- User Profile

## 6) Dashboard Visualizations Included

- Growth trajectory (line)
- Radar personality stability
- Financial exposure bars
- Simulation probability/distribution placeholder for API data
- Confidence interval rendering scaffold
- Dedicated analytics center list includes risk heatmap and emotional volatility trend

## 7) Database Model (SQL Server)

Tables:
- Users
- Roles
- UserRoles
- Decisions
- EmotionalLogs
- HabitEntries
- GoalTargets
- ScoreSnapshots
- RiskProfiles
- SimulationRuns
- SystemAuditLogs

## 8) Testing Strategy

`LAIE.AnalyticsEngine.Tests` validates:
- Statistical metric integrity
- Risk streak and spike clustering behavior
- Monte Carlo quantile consistency (`P90 > P50`, confidence band ordering)

## 9) Production Readiness Notes

- Enforce secrets via Key Vault / environment variables
- Migrate background worker to Hangfire with persistent SQL/Redis job store
- Add refresh token persistence + revocation list
- Add OpenTelemetry traces and central SIEM integration
- Harden with rate limiting, anti-brute-force, and anomaly alerts
