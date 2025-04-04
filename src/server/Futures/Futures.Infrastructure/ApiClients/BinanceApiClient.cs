using System.Globalization;
using System.Net.Http.Json;
using Domain.Exceptions;
using Futures.Application.DTOs;
using Futures.Application.Interfaces.ApiClients;

namespace Futures.Infrastructure.ApiClients;

public class BinanceApiClient(HttpClient httpClient) : IBinanceApiClient
{
	public async Task<decimal> GetFuturePriceAsync(
		string symbol,
		CancellationToken cancellationToken)
	{
		var response = await httpClient.GetAsync(
			$"/fapi/v1/ticker/price?symbol={symbol}",
			cancellationToken);

		if (!response.IsSuccessStatusCode)
		{
			var errorContent = await response.Content.ReadFromJsonAsync<BinanceErrorDto>(
				cancellationToken);

			throw new UnprocessableContentException(errorContent.Message);
		}

		var ticker = await response.Content.ReadFromJsonAsync<BinanceTickerDto>(
			cancellationToken);

		return decimal.Parse(ticker.Price, CultureInfo.InvariantCulture);
	}

	public async Task<IEnumerable<BinanceFutureContractDto>> GetFuturesAsync(
		string symbol,
		CancellationToken cancellationToken)
	{
		var response = await httpClient.GetFromJsonAsync<BinanceExchangeInfoDto>(
			"/fapi/v1/exchangeInfo",
			cancellationToken);

		return response!.Symbols
			.Where(s => s.Symbol.StartsWith($"{symbol.ToUpper()}_"))
			.Select(
				s => new BinanceFutureContractDto(
					s.Symbol,
					s.ContractType,
					DateTimeOffset.FromUnixTimeMilliseconds(s.DeliveryDate).DateTime));
	}
}