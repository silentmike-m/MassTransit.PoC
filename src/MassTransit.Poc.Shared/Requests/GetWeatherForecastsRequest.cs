namespace MassTransit.Poc.Shared.Requests;

public sealed record GetWeatherForecastsRequest
{
    public int Count { get; init; } = default;
}
