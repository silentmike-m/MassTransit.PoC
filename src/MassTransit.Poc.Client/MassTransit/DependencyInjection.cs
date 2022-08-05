namespace MassTransit.Poc.Client.MassTransit;

using System.Security.Authentication;
using global::MassTransit.Poc.Shared.Requests;

internal static class DependencyInjection
{
    public static void AddMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqOptions = configuration.GetSection(RabbitMqOptions.SectionName).Get<RabbitMqOptions>();

        services.AddMassTransit(configure =>
        {
            configure.AddRequestClient<GetWeatherForecastsRequest>();

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
