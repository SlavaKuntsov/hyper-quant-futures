using Domain.Exceptions;
using Futures.Application.Data;
using Futures.Application.Interfaces.ApiClients;
using Futures.Domain.Enums;
using Futures.Domain.Interfaces.Repositories;
using Futures.Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Futures.Application.Handlers.Commands.Futures.FuturesDifference;

public sealed record FuturesDifferenceCommand(
	string Symbol,
	TimeIntervalType TimeIntervalType) : IRequest<decimal>;

public sealed class FuturesDifferenceCommandHandler(
	IBinanceApiClient binanceApiClient,
	IFuturesDbContext context,
	IFutureContractsRepository futureContractsRepository,
	IPricePointsRepository pricePointRepository,
	IPriceDifferencesRepository priceDifferencesRepository,
	ILogger<FuturesDifferenceCommandHandler> logger) : IRequestHandler<FuturesDifferenceCommand, decimal>
{
	public async Task<decimal> Handle(FuturesDifferenceCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Fetching futures for symbol {Symbol}.", request.Symbol);

		var futuresPair = await binanceApiClient.GetFuturesAsync(
			request.Symbol,
			cancellationToken);
		
		// you can use Redis caching for this "/exchangeInfo" requests

		if (!futuresPair.Any())
			throw new NotFoundException($"No futures found for symbol: {request.Symbol}.");

		var sortedFutures = futuresPair
			.OrderBy(f => f.DeliveryDate);

		var currentFuturesSymbol = sortedFutures.First().Symbol;
		var nextFuturesSymbol = sortedFutures.Skip(1).First().Symbol;

		var currentFuture = await futureContractsRepository.GetAsync(
			currentFuturesSymbol,
			cancellationToken);

		var nextFuture = await futureContractsRepository.GetAsync(
			nextFuturesSymbol,
			cancellationToken);

		if (currentFuture is null)
		{
			currentFuture = new FutureContract(
				Guid.NewGuid(),
				currentFuturesSymbol,
				sortedFutures.First().DeliveryDate,
				request.Symbol,
				ContractType.CurrentQuarter);

			await futureContractsRepository.CreateAsync(currentFuture, cancellationToken);
		}

		if (nextFuture is null)
		{
			nextFuture = new FutureContract(
				Guid.NewGuid(),
				nextFuturesSymbol,
				sortedFutures.Skip(1).First().DeliveryDate,
				request.Symbol,
				ContractType.NextQuarter);

			await futureContractsRepository.CreateAsync(nextFuture, cancellationToken);
		}

		var currentQuarterPrice = await binanceApiClient.GetFuturePriceAsync(
			currentFuture.Symbol,
			cancellationToken);

		var nextQuarterPrice = await binanceApiClient.GetFuturePriceAsync(
			nextFuture.Symbol,
			cancellationToken);

		var date = DateTime.UtcNow;

		var currentPricePoint = new PricePoint(
			Guid.NewGuid(),
			currentFuture.Id,
			date,
			currentQuarterPrice);

		var nextPricePoint = new PricePoint(
			Guid.NewGuid(),
			nextFuture.Id,
			date,
			nextQuarterPrice);

		await pricePointRepository.CreateAsync(currentPricePoint, cancellationToken);
		await pricePointRepository.CreateAsync(nextPricePoint, cancellationToken);

		var difference = nextQuarterPrice - currentQuarterPrice;

		var priceDifference = new PriceDifference(
			Guid.NewGuid(),
			currentFuture.Id,
			nextFuture.Id,
			date,
			request.TimeIntervalType,
			difference);

		await priceDifferencesRepository.CreateAsync(priceDifference, cancellationToken);

		await context.SaveChangesAsync(cancellationToken);

		logger.LogInformation("Calculated price difference: {Difference}", difference);

		// maybe need to notify user about new price difference
		// emailService.Send(difference);

		return difference;
	}
}