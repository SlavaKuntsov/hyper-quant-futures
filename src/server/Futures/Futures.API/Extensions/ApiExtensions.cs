using Microsoft.Extensions.DependencyInjection;

namespace Futures.API.Extensions;

public static class ApiExtensions
{
	public static IServiceCollection AddApi(this IServiceCollection services)
	{
		return services;
	}
}