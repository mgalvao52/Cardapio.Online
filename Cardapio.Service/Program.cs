using Cardapio.Application.Helpers;
using Cardapio.Service;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.Configure<DefaultConfiguration>(configuration.GetSection("DefaultConfiguration"));
        services.AddHostedService<Simulator>();
    })    
    .Build();



await host.RunAsync();
