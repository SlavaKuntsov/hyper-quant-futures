using Futures.Domain.Interfaces.Repositories;

namespace Futures.Persistence.Repositories;

public class FuturesRepository(FuturesDbContext context) : IFuturesRepository
{
	// public async Task<HallEntity?> GetAsync(string name, CancellationToken cancellationToken)
	// {
	// 	return await context.Halls
	// 		.AsNoTracking()
	// 		.Where(m => m.Name.ToLower() == name.ToLower())
	// 		.FirstOrDefaultAsync(cancellationToken);
	// }
}