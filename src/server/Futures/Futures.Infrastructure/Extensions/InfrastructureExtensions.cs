using System.Net;
using Futures.Application.Interfaces.ApiClients;
using Futures.Infrastructure.ApiClients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;
using Redis;

namespace Futures.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		// can use redis
		services.AddRedis(configuration);

		// // default http client configuration
		// services.AddHttpClient<IBinanceApiClient, BinanceApiClient>(
		// 	client =>
		// 	{
		// 		client.BaseAddress = new Uri("https://fapi.binance.com");
		// 		client.Timeout = TimeSpan.FromSeconds(30);
		// 	});

		// http client configuration with Polly
		var retryPolicy = HttpPolicyExtensions
			.HandleTransientHttpError()
			.OrResult(msg => msg.StatusCode == HttpStatusCode.TooManyRequests)
			.WaitAndRetryAsync(
				Backoff.DecorrelatedJitterBackoffV2(
					TimeSpan.FromSeconds(1),
					3));

		var circuitBreakerPolicy = HttpPolicyExtensions
			.HandleTransientHttpError()
			.CircuitBreakerAsync(
				3,
				TimeSpan.FromSeconds(30));

		services.AddHttpClient<IBinanceApiClient, BinanceApiClient>(
				client =>
				{
					client.BaseAddress = new Uri("https://fapi.binance.com");
					client.Timeout = TimeSpan.FromSeconds(30);
				})
			.AddPolicyHandler(retryPolicy)
			.AddPolicyHandler(circuitBreakerPolicy)
			.AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(10));

		return services;
	}
}