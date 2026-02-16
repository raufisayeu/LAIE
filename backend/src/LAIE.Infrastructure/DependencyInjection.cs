using LAIE.Application.Abstractions;
using LAIE.Domain.Analytics;
using LAIE.Infrastructure.Authentication;
using LAIE.Infrastructure.BackgroundJobs;
using LAIE.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LAIE.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<LaieDbContext>());
        services.AddScoped<IAnalyticsEngine, AnalyticsEngine>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddHostedService<RiskRecalculationWorker>();

        return services;
    }
}
