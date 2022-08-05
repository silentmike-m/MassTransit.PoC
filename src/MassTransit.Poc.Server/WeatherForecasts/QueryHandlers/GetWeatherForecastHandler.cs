namespace MassTransit.Poc.Server.WeatherForecasts.QueryHandlers;

using global::MassTransit.Poc.Server.WeatherForecasts.Queries;
using global::MassTransit.Poc.Server.WeatherForecasts.ViewModels;
using MediatR;

internal sealed class GetWeatherForecastHandler : IRequestHandler<GetWeatherForecast, IReadOnlyList<WeatherForecast>>
{
    private static readonly string[] Summaries = {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<GetWeatherForecastHandler> logger;

    public GetWeatherForecastHandler(ILogger<GetWeatherForecastHandler> logger)
        => this.logger = logger;

    public async Task<IReadOnlyList<WeatherForecast>> Handle(GetWeatherForecast request, CancellationToken cancellationToken)
    {
        this.logger.BeginScope("Try to get weather forecasts in count {Count}", request.Count);

        var result = new List<WeatherForecast>();

        if (request.Count > 0)
        {
            foreach (var index in Enumerable.Range(1, request.Count))
            {
                var temperatureC = Random.Shared.Next(-20, 55);
                var temperatureF = 32 + (int)(temperatureC / 0.5556);

                var weatherForecast = new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                    TemperatureC = temperatureC,
                    TemperatureF = temperatureF,
                };

                result.Add(weatherForecast);
            }
        }

        return await Task.FromResult(result);
    }
}
