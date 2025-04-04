using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Databases;

public static class DatabasesExtension
{
	public static IServiceCollection AddPostgres<TContextImplementation>(
		this IServiceCollection services,
		IConfiguration configuration) where TContextImplementation : DbContext
	{
		var name = typeof(TContextImplementation).Name;

		var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")
								?? configuration.GetConnectionString(name);

		services.AddDbContextPool<TContextImplementation>(
			options =>
			{
				options.UseNpgsql(connectionString);
			},
			128);

		return services;
	}
}