using Payment.Worker.Consumers;
using Payment.Worker.Producers;
using Shared.MQ;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMQModule();
        services.AddHostedService<PaymentRequestConsumer>();
        services.AddScoped<ReceivedPaymentProducer>();
    })
    .Build();

await host.RunAsync();