namespace MassTransit.Poc.Server.WeatherForecasts.ViewModels;

public sealed record WeatherForecast
{
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF { get; set; }

    public string? Summary { get; set; }
}
