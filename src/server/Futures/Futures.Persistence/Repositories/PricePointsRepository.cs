using Futures.Domain.Interfaces.Repositories;
using Futures.Domain.Models;

namespace Futures.Persistence.Repositories;

public class PricePointsRepository(FuturesDbContext context) : IPricePointsRepository
{
	public async Task<Guid> CreateAsync(PricePoint pricePoint, CancellationToken cancellationToken)
	{
		await context.PricePoints.AddAsync(pricePoint, cancellationToken);

		return pricePoint.Id;
	}
}