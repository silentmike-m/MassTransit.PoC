namespace MassTransit.Poc.Client.WeatherForecasts.ViewModels;

public sealed class WeatherForecasts
{
    public IReadOnlyList<WeatherForecast> Forecasts { get; init; } = new List<WeatherForecast>();
}
