namespace MassTransit.Poc.Server.Controllers;

using global::MassTransit.Poc.Server.WeatherForecasts.Queries;
using global::MassTransit.Poc.Server.WeatherForecasts.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IMediator mediator;

    public WeatherForecastController(IMediator mediator)
        => this.mediator = mediator;

    [HttpPost(Name = "GetWeatherForecast")]
    public async Task<IReadOnlyList<WeatherForecast>> GetWeatherForecast(GetWeatherForecast request)
        => await this.mediator.Send(request, CancellationToken.None);
}
