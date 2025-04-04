using Futures.Application.Handlers.Commands.Futures.FuturesDifference;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Futures.API.Controllers;

[ApiController]
[Route("[controller]")]
public class FuturesController(
	IMediator mediator,
	ILogger<FuturesController> logger) : ControllerBase
{
	[HttpPost("/futures/difference")]
	public async Task<IActionResult> Create(
		[FromQuery] string symbol,
		CancellationToken cancellationToken)
	{
		logger.LogInformation(
			"Starting to calculate the difference of futures for {Symbol} symbol.",
			symbol);

		var difference = await mediator.Send(
			new FuturesDifferenceCommand(symbol),
			cancellationToken);

		logger.LogInformation(
			"Finish to calculate the difference of futures for {Symbol} symbol.",
			symbol);

		return Ok(difference);
	}
}