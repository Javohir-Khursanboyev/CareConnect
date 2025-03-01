﻿using Microsoft.AspNetCore.Diagnostics;

public class InternalServerExceptionHandler(ILogger<InternalServerExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await httpContext.Response.WriteAsJsonAsync(new Response
        {
            StatusCode = 500,
            Message = exception.Message,
        });

        logger.LogError(exception.Message);

        return true;
    }
}