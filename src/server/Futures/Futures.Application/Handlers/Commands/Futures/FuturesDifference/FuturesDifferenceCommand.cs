using Domain.Exceptions;
using Futures.Application.Data;
using Futures.Application.Interfaces.ApiClients;
using Futures.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Futures.Application.Handlers.Commands.Futures.FuturesDifference;

public sealed record FuturesDifferenceCommand(string Symbol) : IRequest<decimal>;

public sealed class FuturesDifferenceCommandHandler(
	IBinanceApiClient binanceApiClient,
	IFuturesDbContext context,
	IFuturesRepository futuresRepository,
	ILogger<FuturesDifferenceCommandHandler> logger) : IRequestHandler<FuturesDifferenceCommand, decimal>
{
	public async Task<decimal> Handle(FuturesDifferenceCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Fetching futures for symbol {Symbol}.", request.Symbol);

		var futuresPair = await binanceApiClient.GetFuturesAsync(
			request.Symbol,
			cancellationToken);

		var sortedFutures = futuresPair
			.OrderBy(f => f.DeliveryDate);

		if (!futuresPair.Any())
			throw new NotFoundException($"No futures found for symbol: {request.Symbol}.");

		
		var currentQuarterPrice = await binanceApiClient.GetFuturePriceAsync(
			sortedFutures.First().Symbol,
			cancellationToken);

		var nextQuarterPrice = await binanceApiClient.GetFuturePriceAsync(
			sortedFutures.Skip(1).First().Symbol,
			cancellationToken);

		var difference = nextQuarterPrice - currentQuarterPrice;

		logger.LogInformation("Calculated price difference: {Difference}", difference);

		return difference;
	}
}