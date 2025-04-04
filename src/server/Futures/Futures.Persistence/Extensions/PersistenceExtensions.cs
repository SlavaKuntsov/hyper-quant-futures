using Databases;
using Futures.Application.Data;
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
		services.AddPostgres<IFuturesDbContext, FuturesDbContext>(configuration);

		services.AddScoped<IFutureContractsRepository, FutureContractContractsRepository>();
		services.AddScoped<IPricePointsRepository, PricePointsRepository>();
		services.AddScoped<IPriceDifferencesRepository, PriceDifferencesRepository>();

		return services;
	}
}