using indexer.Application.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace indexer.Application;

public class Worker(ILogger<Worker> Logger, IServiceProvider Provider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

        Logger.LogTrace("Creating service scope");
        var scope = Provider.CreateScope();

        Logger.LogTrace("Getting required service");
        var job = scope.ServiceProvider.GetRequiredService<IndexAllContentJob>();

        Logger.LogTrace("Invoking execute method on job");
        await job.ExecuteAsync(stoppingToken);

        Logger.LogInformation("Worker has completed");
    }
}
