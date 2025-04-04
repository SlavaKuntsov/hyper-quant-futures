using Futures.Domain.Interfaces.Repositories;
using Futures.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Futures.Persistence.Repositories;

public class FutureContractContractsRepository(FuturesDbContext context) : IFutureContractsRepository
{
	public async Task<FutureContract?> GetAsync(string symbol, CancellationToken cancellationToken)
	{
		return await context.FutureContracts
			.AsNoTracking()
			.Where(m => m.Symbol.ToLower() == symbol.ToLower())
			.FirstOrDefaultAsync(cancellationToken);
	}

	public async Task<Guid> CreateAsync(FutureContract futureContract, CancellationToken cancellationToken)
	{
		await context.FutureContracts.AddAsync(futureContract, cancellationToken);
		
		return futureContract.Id;
	}
}