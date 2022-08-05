namespace MassTransit.Poc.Shared.Models;

public sealed record WeatherForecast
{
    public Guid Id { get; init; } = Guid.Empty;
    public DateTime Date { get; init; } = DateTime.MinValue;

    public int TemperatureC { get; init; } = default;

    public int TemperatureF { get; init; } = default;

    public string? Summary { get; init; } = default;
}
