using Futures.Domain.Models;

namespace Futures.Domain.Interfaces.Repositories;

public interface IPriceDifferencesRepository
{
	Task<Guid> CreateAsync(PriceDifference priceDifference, CancellationToken cancellationToken);
}