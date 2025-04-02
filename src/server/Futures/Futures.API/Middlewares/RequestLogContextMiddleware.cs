using Serilog.Context;

namespace Futures.API.Middlewares;

public class RequestLogContextMiddleware(RequestDelegate next)
{
	public Task InvokeAsync(HttpContext httpContext)
	{
		using (LogContext.PushProperty("CorrelationId", httpContext.TraceIdentifier))
		{
			return next(httpContext);
		}
	}
}