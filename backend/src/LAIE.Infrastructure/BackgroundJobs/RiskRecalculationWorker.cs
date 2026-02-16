using LAIE.Domain.Analytics;

namespace LAIE.Infrastructure.BackgroundJobs;

public sealed class RiskRecalculationWorker : BackgroundService
{
    private readonly ILogger<RiskRecalculationWorker> _logger;

    public RiskRecalculationWorker(ILogger<RiskRecalculationWorker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Running scheduled LAIE risk recalculation at {time}", DateTimeOffset.UtcNow);
            await Task.Delay(TimeSpan.FromMinutes(15), stoppingToken);
        }
    }
}
