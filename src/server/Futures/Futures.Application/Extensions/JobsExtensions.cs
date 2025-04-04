using Futures.Application.Handlers.Commands.Futures.FuturesDifference;
using Futures.Domain.Enums;
using Hangfire;

namespace Futures.Application.Extensions;

public static class JobsExtensions
{
	public static void AddJobs()
	{
		// // 10-sec job for testing
		// RecurringJob.AddOrUpdate<FuturesDifferenceCommandHandler>(
		// 	"10sec-futures-difference",
		// 	x => x.Handle(
		// 		new FuturesDifferenceCommand("BTCUSDT", TimeIntervalType.Hourly),
		// 		CancellationToken.None),
		// 	"*/10 * * * * *");

		RecurringJob.AddOrUpdate<FuturesDifferenceCommandHandler>(
			"hourly-futures-difference",
			x => x.Handle(
				new FuturesDifferenceCommand("BTCUSDT", TimeIntervalType.Hourly),
				CancellationToken.None),
			"0 * * * *");

		RecurringJob.AddOrUpdate<FuturesDifferenceCommandHandler>(
			"daily-futures-difference",
			x => x.Handle(
				new FuturesDifferenceCommand("BTCUSDT", TimeIntervalType.Daily),
				CancellationToken.None),
			"59 23 * * *");
	}
}