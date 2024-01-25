using indexer.Application.Interfaces;
using indexer.Application.Jobs;
using indexer.Repository;
using indexer.Repository.Repositories;
using indexer.Repository.Repositories.UCR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;

namespace indexer.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIndexerApplication(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<SemaphoreSlim>(new SemaphoreSlim(10));

        services.AddDbContext<UCRContext>(options => options.UseNpgsql(configuration.GetConnectionString(UCRContext.DatabaseAlias)));
        services.AddScoped<IContentRepository, UCRRepository>();

        services.AddScoped<IJob<IndexAllContentJob>, IndexAllContentJob>();

        return services;
    }
}