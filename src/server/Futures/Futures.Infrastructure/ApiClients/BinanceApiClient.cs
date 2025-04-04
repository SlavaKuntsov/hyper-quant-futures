using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Futures.Application.DTOs;
using Futures.Application.Interfaces.ApiClients;

namespace Futures.Infrastructure.ApiClients;

public class BinanceApiClient(HttpClient httpClient) : IBinanceApiClient
{
	public async Task<decimal> GetFuturePriceAsync(string symbol, CancellationToken cancellationToken)
	{
		var response = await httpClient.GetFromJsonAsync<BinanceTicker>(
			$"/fapi/v1/ticker/price?symbol={symbol}",
			cancellationToken);

		return decimal.Parse(response!.Price);
	}

	public async Task<IEnumerable<BinanceFutureContract>> GetFuturesAsync(
		string symbol,
		CancellationToken cancellationToken)
	{
		var response = await httpClient.GetFromJsonAsync<BinanceExchangeInfo>(
			"/fapi/v1/exchangeInfo",
			cancellationToken);

		return response!.Symbols
			.Where(s => s.Symbol.StartsWith($"{symbol.ToUpper()}_"))
			.Select(
				s => new BinanceFutureContract(
					s.Symbol,
					DateTimeOffset.FromUnixTimeMilliseconds(s.DeliveryDate).DateTime));
	}

	private record BinanceTicker(string Symbol, string Price);

	private record BinanceExchangeInfo(List<BinanceSymbol> Symbols);

	private record BinanceSymbol(
		[property: JsonPropertyName("symbol")] string Symbol,
		[property: JsonPropertyName("contractType")] string ContractType,
		[property: JsonPropertyName("deliveryDate")] long DeliveryDate);
}