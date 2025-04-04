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

		return services;
	}
}