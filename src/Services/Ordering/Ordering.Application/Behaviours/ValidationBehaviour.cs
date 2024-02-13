using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Ordering.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ValidationException = Ordering.Application.Exceptions.ValidationException;

namespace Ordering.Application.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validator)
        {
            _validators = validator;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var ValidationResult = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context,cancellationToken)));
                var failures = ValidationResult.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                    throw new ValidationException(failures);
            }
            return await next();
        }
    }
}
