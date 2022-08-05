namespace MassTransit.Poc.Client.WeatherForecasts.Queries;

using System.Text.Json.Serialization;
using global::MassTransit.Poc.Client.WeatherForecasts.ViewModels;
using MediatR;

public sealed record GetWeatherForecast : IRequest<WeatherForecasts>
{
    [JsonPropertyName("count")] public int Count { get; init; } = default;
}
