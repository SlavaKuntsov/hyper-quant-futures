using Hangfire.PostgreSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hangfire;

public static class HangfireExtension
{
	public static IServiceCollection AddHangfire(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		var hangfireConnectionString =
			Environment.GetEnvironmentVariable("HANGFIRE_CONNECTION_STRING");

		if (string.IsNullOrEmpty(hangfireConnectionString))
			hangfireConnectionString = configuration.GetConnectionString("HangfireDb");

		services.AddHangfire(
			config =>
			{
				config.UsePostgreSqlStorage(
					hangfireConnectionString,
					new PostgreSqlStorageOptions
					{
						DistributedLockTimeout = TimeSpan.FromSeconds(30),
						PrepareSchemaIfNecessary = true,
						QueuePollInterval = TimeSpan.FromSeconds(5)
					});
			});

		services.AddHangfireServer(
			serverOptions =>
			{
				serverOptions.ServerName = "Hangfire.Postgres server 1";
			});
		
		return services;
	}
}