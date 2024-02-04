using indexer.Application.Interfaces;
using indexer.Application.Jobs;
using indexer.Application.Models;
using indexer.Application.Services;
using indexer.Repository;
using indexer.Repository.Repositories;
using indexer.Repository.Repositories.UCR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace indexer.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIndexerApplication(this IServiceCollection services, IConfigurationManager configuration)
    {
        // Add configuration objects
        services.AddOptions<IndexOptions>().Bind(configuration.GetSection(nameof(IndexOptions)));

        // Add database contexts
        services.AddDbContext<UCRContext>(options => options.UseNpgsql(configuration.GetConnectionString(UCRContext.DatabaseAlias)));

        services.AddHostedService<Worker>();

        services.AddScoped<IContentRepository, UCRRepository>();
        services.AddScoped<IElasticSearchService, ElasticSearchService>();
        services.AddScoped<IJob<IndexAllContentJob>, IndexAllContentJob>();
        services.AddScoped<IIndexDocumentProcesser, IndexDocumentProcesser>();

        return services;
    }
}
