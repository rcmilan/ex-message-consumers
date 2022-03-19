using Payment.Worker.Consumers;
using Shared.MQ;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMQModule();
        services.AddHostedService<PaymentRequestConsumer>();
    })
    .Build();

await host.RunAsync();