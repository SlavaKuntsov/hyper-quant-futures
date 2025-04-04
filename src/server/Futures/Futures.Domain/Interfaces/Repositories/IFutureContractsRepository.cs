using Futures.Domain.Models;

namespace Futures.Domain.Interfaces.Repositories;

public interface IFutureContractsRepository
{
	Task<FutureContract?> GetAsync(string symbol, CancellationToken cancellationToken);
	Task<Guid> CreateAsync(FutureContract futureContract, CancellationToken cancellationToken);
}