using Futures.API.Extensions;
using Futures.API.Middlewares;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
var host = builder.Host;

// services configurations
services
	.AddApi()
	.AddAuth(configuration);

host.AddLogging();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

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

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.Run();