namespace Futures.Application.Data;

public interface IFuturesDbContext
{
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}