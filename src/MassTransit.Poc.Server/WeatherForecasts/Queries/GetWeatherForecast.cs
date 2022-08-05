namespace MassTransit.Poc.Server.WeatherForecasts.Queries;

using System.Text.Json.Serialization;
using global::MassTransit.Poc.Server.WeatherForecasts.ViewModels;
using MediatR;

public sealed record GetWeatherForecast : IRequest<IReadOnlyList<WeatherForecast>>
{
    [JsonPropertyName("count")] public int Count { get; init; } = default;
}
