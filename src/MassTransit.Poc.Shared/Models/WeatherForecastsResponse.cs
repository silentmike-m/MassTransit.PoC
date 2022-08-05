namespace MassTransit.Poc.Shared.Models;

public sealed record WeatherForecastsResponse
{
    public Dictionary<Guid, WeatherForecast> Forecasts { get; init; } = new();
}
