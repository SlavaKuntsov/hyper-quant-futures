using Futures.Application.Handlers.Commands.Futures.FuturesDifference;
using Futures.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Futures.API.Controllers;

[ApiController]
[Route("[controller]")]
public class FuturesController(
	IMediator mediator,
	ILogger<FuturesController> logger) : ControllerBase
{
	[HttpPost("/futures/quarterDifference")]
	public async Task<IActionResult> Create(
		[FromQuery] string symbol,
		CancellationToken cancellationToken)
	{
		logger.LogInformation(
			"Starting to calculate the difference of futures for {Symbol} symbol.",
			symbol);

		var difference = await mediator.Send(
			new FuturesDifferenceCommand(symbol, TimeIntervalType.None),
			cancellationToken);

		logger.LogInformation(
			"Successfully calculated the futures difference for symbol {Symbol}: {Difference}.",
			symbol,
			difference);

		return Ok(new { difference });
	}
}