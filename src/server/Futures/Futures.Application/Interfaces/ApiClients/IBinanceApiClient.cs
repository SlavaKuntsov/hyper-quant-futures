using Futures.Application.DTOs;

namespace Futures.Application.Interfaces.ApiClients;

public interface IBinanceApiClient
{
	Task<decimal> GetFuturePriceAsync(string symbol, CancellationToken cancellationToken);
	Task<IEnumerable<BinanceFutureContract>> GetFuturesAsync(
		string symbol,
		CancellationToken cancellationToken);
}