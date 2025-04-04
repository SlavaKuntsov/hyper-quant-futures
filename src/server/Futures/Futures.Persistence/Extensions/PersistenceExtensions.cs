using Databases;
using Futures.Domain.Interfaces.Repositories;
using Futures.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Futures.Persistence.Extensions;

public static class PersistenceExtensions
{
	public static IServiceCollection AddPersistence(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		services.AddPostgres<FuturesDbContext>(configuration);

		services.AddScoped<IFuturesRepository, FuturesRepository>();

		return services;
	}
}