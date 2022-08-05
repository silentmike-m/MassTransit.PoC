namespace MassTransit.Poc.Server.MassTransit;

using System.Security.Authentication;
using global::MassTransit.Poc.Server.MassTransit.Consumers;

internal static class DependencyInjection
{
    public static void AddMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqOptions = configuration.GetSection(RabbitMqOptions.SectionName).Get<RabbitMqOptions>();

        services.AddMassTransit(configure =>
        {
            configure.AddConsumer<GetWeatherForecastsRequestConsumer>();

            configure.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitMqOptions.HostName, rabbitMqOptions.Port, rabbitMqOptions.VirtualHost, host =>
                {
                    host.Password(rabbitMqOptions.Password);
                    host.Username(rabbitMqOptions.User);

                    if (rabbitMqOptions.UseSsl)
                    {
                        host.UseSsl(ssl => ssl.Protocol = SslProtocols.Tls12 | SslProtocols.Tls13);
                    }
                });

                cfg.ConfigureEndpoints(context);
            });
        });
    }
}
