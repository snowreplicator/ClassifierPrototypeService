﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Prototype.ClassifierPrototypeService.Application.Common;

public abstract class BaseApplicationService<TRequest, TResponse> : IApplicationService<TRequest, TResponse>
    where TRequest : BaseRequest
{
    private readonly ILogger _logger;
    private readonly IRequestContext _requestContext;

    public Guid UserGuid { get; set; }

    protected BaseApplicationService(ILogger logger, IRequestContext requestContext)
    {
        _logger = logger;
        _requestContext = requestContext;
    }

    public async Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            await _requestContext.BeginTransactionAsync();
            //_logger.LogInformation($"'{GetType().Name}' request '{typeof(TRequest).Name}' handling. TracingId: {_requestContext.TracingId}");
            _logger.LogInformation($"'{GetType().Name}' request '{typeof(TRequest).Name}' handling.");
            TResponse response = await ProtectedHandle(request, cancellationToken);
            await _requestContext.CommitTransactionAsync();

            return response;
        }
        catch (Exception exception)
        {
            string exceptionLoggerString = exception.ToString();

            //_logger.LogCritical($"'{GetType().Name}' request '{typeof(TRequest).Name}' exception. \n{exceptionLoggerString}. TracingId: {_requestContext.TracingId}");
            _logger.LogCritical($"'{GetType().Name}' request '{typeof(TRequest).Name}' exception. \n{exceptionLoggerString}.");

            await _requestContext.CancelTransactionAsync();

            throw;
        }
        finally
        {
            //_logger.LogInformation($"'{GetType().Name}' request '{typeof(TRequest).Name}' handled. TracingId: {_requestContext.TracingId}");
            _logger.LogInformation($"'{GetType().Name}' request '{typeof(TRequest).Name}' handled.");
        }
    }

    protected abstract Task<TResponse> ProtectedHandle(TRequest request, CancellationToken cancellationToken = default);
}
