using Futures.Domain.Models;

namespace Futures.Domain.Interfaces.Repositories;

public interface IPricePointsRepository
{
	Task<Guid> CreateAsync(PricePoint pricePoint, CancellationToken cancellationToken);
}