namespace MassTransit.Poc.Client.MassTransit.Validators;

using FluentValidation;
using global::MassTransit.Poc.Client.WeatherForecasts.ViewModels;

public sealed class WeatherForecastsValidator : AbstractValidator<WeatherForecasts>
{
    public WeatherForecastsValidator()
    {
        RuleFor(response => response.Forecasts)
            .Must(forecasts => forecasts.Count >= 1)
            ;
    }
}
