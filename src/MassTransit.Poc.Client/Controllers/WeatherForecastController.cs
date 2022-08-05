using Microsoft.AspNetCore.Mvc;

namespace MassTransit.Poc.Client.Controllers;

using global::MassTransit.Poc.Client.WeatherForecasts.Queries;
using global::MassTransit.Poc.Client.WeatherForecasts.ViewModels;
using MediatR;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IMediator mediator;

    public WeatherForecastController(IMediator mediator)
        => this.mediator = mediator;

    [HttpPost(Name = "GetWeatherForecast")]
    public async Task<WeatherForecasts> GetWeatherForecast(GetWeatherForecast request)
        => await this.mediator.Send(request, CancellationToken.None);
}
