using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

using ImageGallery.Exceptions;

namespace ImageGallery
{
    public class PipelineValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public PipelineValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var validationFailures = _validators
             .Select(validator => validator.Validate(request))
             .SelectMany(validationResult => validationResult.Errors)
             .Where(validationFailure => validationFailure != null)
             .ToList();

            if (validationFailures.Any())
            {
                var error = string.Join("\r\n", validationFailures);
                throw new PipelineValidationException(error);
            }

            return next();
        }
    }
}
