using FluentValidation;
using MediatR;

namespace CanvasVault.Application.Behaviors
{
	public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
	{
		private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

		public override bool Equals(object? obj)
		{
			return obj is ValidationBehavior<TRequest, TResponse> behavior &&
				   EqualityComparer<IEnumerable<IValidator<TRequest>>>.Default.Equals(_validators, behavior._validators);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(_validators);
		}

		public async Task<TResponse> Handle(
			TRequest request,
			RequestHandlerDelegate<TResponse> next,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(next);
			if (_validators.Any())
			{
				var context = new ValidationContext<TRequest>(request);
				var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
				var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

				if (failures.Count != 0)
				{
					throw new ValidationException(failures);
				}
			}
			return await next();
		}
	}
}
