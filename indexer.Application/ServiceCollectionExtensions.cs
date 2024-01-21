using indexer.Application.Interfaces;
using indexer.Application.Jobs;
using indexer.Repository;
using indexer.Repository.Repositories;
using indexer.Repository.Repositories.UCR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;

namespace indexer.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIndexerApplication(this IServiceCollection services)
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<SemaphoreSlim>(new SemaphoreSlim(10));

        services.AddScoped<UCRContext>((_) =>
        {
            var options = new DbContextOptionsBuilder<UCRContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new UCRContext(options);
        });
        services.AddScoped<IContentRepository, UCRRepository>();

        services.AddScoped<IJob<IndexAllContentJob>, IndexAllContentJob>();

        return services;
    }
}