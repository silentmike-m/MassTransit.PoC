namespace MassTransit.Poc.Client.MassTransit.QueryHandlers;

using global::MassTransit.Poc.Client.WeatherForecasts.Queries;
using global::MassTransit.Poc.Client.WeatherForecasts.ViewModels;
using global::MassTransit.Poc.Shared.Models;
using global::MassTransit.Poc.Shared.Requests;
using MediatR;
using WeatherForecast = global::MassTransit.Poc.Client.WeatherForecasts.ViewModels.WeatherForecast;

internal sealed class GetWeatherForecastHandler : IRequestHandler<GetWeatherForecast, WeatherForecasts>
{
    private readonly ILogger<GetWeatherForecastHandler> logger;
    private readonly IRequestClient<GetWeatherForecastsRequest> requestClient;

    public GetWeatherForecastHandler(ILogger<GetWeatherForecastHandler> logger, IRequestClient<GetWeatherForecastsRequest> requestClient)
        => (this.logger, this.requestClient) = (logger, requestClient);

    public async Task<WeatherForecasts> Handle(GetWeatherForecast request, CancellationToken cancellationToken)
    {
        this.logger.BeginScope("Try to get weather forecasts in count {Count}", request.Count);

        var getWeatherForecastsRequest = new GetWeatherForecastsRequest
        {
            Count = request.Count,
        };

        var response = await requestClient.GetResponse<WeatherForecastsResponse>(getWeatherForecastsRequest, cancellationToken);

        var resultWeatherForecasts = new List<WeatherForecast>();

        foreach (var responseWeatherForecast in response.Message.Forecasts)
        {
            var weatherForecast = new WeatherForecast
            {
                Date = responseWeatherForecast.Value.Date,
                Summary = responseWeatherForecast.Value.Summary,
                TemperatureC = responseWeatherForecast.Value.TemperatureC,
                TemperatureF = responseWeatherForecast.Value.TemperatureF,
            };

            resultWeatherForecasts.Add(weatherForecast);
        }

        var result = new WeatherForecasts
        {
            Forecasts = resultWeatherForecasts,
        };

        return result;
    }
}
