namespace MassTransit.Poc.Client;

using FluentValidation;
using FluentValidation.Results;
using MediatR;

public sealed class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> requestValidators;
    private readonly IEnumerable<IValidator<TResponse>> responseValidators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> requestValidators, IEnumerable<IValidator<TResponse>> responseValidators)
        => (this.requestValidators, this.responseValidators) = (requestValidators, responseValidators);

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        if (requestValidators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var validation = this.requestValidators.Select(v => v.ValidateAsync(context, cancellationToken));

            await Validate(validation);
        }

        var response = await next();

        if (this.responseValidators.Any())
        {
            var context = new ValidationContext<TResponse>(response);
            var validation = this.responseValidators.Select(v => v.ValidateAsync(context, cancellationToken));

            await Validate(validation);
        }

        return response;
    }

    private static async Task Validate(IEnumerable<Task<ValidationResult>> validation)
    {
        var validationResults = await Task.WhenAll(validation);

        var failures = validationResults
            .SelectMany(vr => vr.Errors)
            .Where(vr => vr is not null)
            .ToList();

        if (failures.Count > 0)
        {
            throw new ValidationException(failures);
        }
    }
}
