using indexer.Application;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddIndexerApplication();
    })
    .Build();

await host.RunAsync();
