using Futures.Domain.Interfaces.Repositories;
using Futures.Domain.Models;

namespace Futures.Persistence.Repositories;

public class PriceDifferencesRepository(FuturesDbContext context) : IPriceDifferencesRepository
{
	public async Task<Guid> CreateAsync(PriceDifference priceDifference, CancellationToken cancellationToken)
	{
		await context.PriceDifferences.AddAsync(priceDifference, cancellationToken);

		return priceDifference.Id;
	}
}