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
		logger.LogInformation("Getting futures.");

		var futuresPair = await binanceApiClient.GetFuturesAsync(
			request.Symbol,
			cancellationToken);

		if (!futuresPair.Any())
			throw new NotFoundException($"No futures found for symbol: {request.Symbol}.");

		foreach (var future in futuresPair)
		{
			logger.LogInformation(
				"Getting future {Symbol}.",
				future.Symbol);
		
					
		}

		throw new NotImplementedException();
	}
}