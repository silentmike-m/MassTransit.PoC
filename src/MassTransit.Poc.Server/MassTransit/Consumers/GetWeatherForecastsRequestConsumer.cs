namespace MassTransit.Poc.Server.MassTransit.Consumers;

using global::MassTransit.Poc.Server.WeatherForecasts.Queries;
using global::MassTransit.Poc.Shared.Models;
using global::MassTransit.Poc.Shared.Requests;
using MediatR;

internal sealed class GetWeatherForecastsRequestConsumer : IConsumer<GetWeatherForecastsRequest>
{
    private readonly ILogger<GetWeatherForecastsRequestConsumer> logger;
    private readonly IMediator mediator;

    public GetWeatherForecastsRequestConsumer(ILogger<GetWeatherForecastsRequestConsumer> logger, IMediator mediator)
        => (this.logger, this.mediator) = (logger, mediator);

    public async Task Consume(ConsumeContext<GetWeatherForecastsRequest> context)
    {
        this.logger.LogInformation("Received get WeatherForecasts request");

        var request = new GetWeatherForecast
        {
            Count = context.Message.Count,
        };

        var weatherForecasts = await this.mediator.Send(request, CancellationToken.None);

        var responseWeatherForecasts = new Dictionary<Guid, WeatherForecast>();

        foreach (var weatherForecast in weatherForecasts)
        {
            var responseWeatherForecast = new WeatherForecast
            {
                Id = Guid.NewGuid(),
                Date = weatherForecast.Date,
                Summary = weatherForecast.Summary,
                TemperatureC = weatherForecast.TemperatureC,
                TemperatureF = weatherForecast.TemperatureF,
            };

            responseWeatherForecasts.Add(responseWeatherForecast.Id, responseWeatherForecast);
        }

        var response = new WeatherForecastsResponse
        {
            Forecasts = responseWeatherForecasts,
        };

        await context.RespondAsync(response);
    }
}
