using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Behaviours
{
    internal class UnhandledExceptionBehaviour<TRequest, TResponxe> : IPipelineBehavior<TRequest, TResponxe>
    {
        public readonly ILogger<TRequest> _logger;

        public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponxe> Handle(TRequest request, RequestHandlerDelegate<TResponxe> next, CancellationToken cancellationToken)
        {
            try
            {
              return  await next();
            }
            catch (Exception ex)
            {
                var requestName = typeof(TRequest).Name;
                _logger.LogError(ex, "Application Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);
                throw;
            }
        }
    }
}
