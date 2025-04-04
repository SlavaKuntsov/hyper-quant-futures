using Extensions.Common;
using Extensions.Exceptions;
using Extensions.Exceptions.Middlewares;
using Extensions.Logging;
using Extensions.Swagger;
using Futures.API.Extensions;
using Futures.Application.Extensions;
using Futures.Infrastructure.Extensions;
using Futures.Persistence.Extensions;
using Hangfire;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
var host = builder.Host;

// Если нам нужны https и у нас есть сертификаты
// builder.UseHttps();

// shared extensions
services
	.AddCommon()
	.AddExceptions()
	// .AddAuthorization(configuration)
	.AddSwagger()
	.AddHangfire(configuration)
	.AddHealthChecks();

// service extensions
services
	.AddApi()
	.AddApplication()
	.AddInfrastructure(configuration)
	.AddPersistence(configuration);

host.AddLogging();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseHangfireDashboard();

app.UseExceptionHandler();
app.UseMiddleware<RequestLogContextMiddleware>();

app.UseSerilogRequestLogging();

app.MapHealthChecks(
	"/health",
	new HealthCheckOptions
	{
		ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
	});

app.UseCookiePolicy(
	new CookiePolicyOptions
	{
		MinimumSameSitePolicy = SameSiteMode.None,
		HttpOnly = HttpOnlyPolicy.Always,
		Secure = CookieSecurePolicy.Always
	});

app.UseForwardedHeaders(
	new ForwardedHeadersOptions
	{
		ForwardedHeaders = ForwardedHeaders.All
	});
app.UseCors();

app.UseSwagger();
app.UseSwaggerUI();

// hangfire jobs configurations
JobsExtensions.AddJobs();

app.Run();