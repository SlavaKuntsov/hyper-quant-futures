using Futures.Application.Interfaces.ApiClients;
using Futures.Infrastructure.ApiClients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Redis;

namespace Futures.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		services.AddRedis(configuration);

		services.AddHttpClient<IBinanceApiClient, BinanceApiClient>(
			client =>
			{
				client.BaseAddress = new Uri("https://fapi.binance.com");
				client.Timeout = TimeSpan.FromSeconds(30);
			});

		return services;
	}
}