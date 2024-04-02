using Consumer;
using Mottu.Service.Data;
using Mottu.Service.Interfaces;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
    })
    .Build();
host.Run();