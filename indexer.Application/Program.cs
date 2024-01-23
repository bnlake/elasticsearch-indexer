using indexer.Application;
using Microsoft.Extensions.Hosting;
using Serilog;
var builder = Host.CreateApplicationBuilder();

builder.Services.AddIndexerApplication(builder.Configuration);
builder.Services.AddSerilog(config => config.ReadFrom.Configuration(builder.Configuration).WriteTo.Console());

var host = builder.Build();

await host.RunAsync();
