using Futures.Domain.Enums;

namespace Futures.Domain.Models;

public class PriceDifference
{
	public Guid Id { get; set; }
	public Guid CurrentFuturesContractId { get; set; }
	public Guid NextFuturesContractId { get; set; }
	public DateTime Timestamp { get; set; }
	public TimeIntervalType IntervalType { get; set; }
	public decimal Difference { get; set; }

	public virtual FutureContract CurrentFutureContract { get; set; } = null!;
	public virtual FutureContract NextFutureContract { get; set; } = null!;

	public PriceDifference()
	{
	}

	public PriceDifference(
		Guid id,
		Guid currentFuturesContractId,
		Guid nextFuturesContractId,
		DateTime timestamp,
		TimeIntervalType intervalType,
		decimal difference)
	{
		Id = id;
		CurrentFuturesContractId = currentFuturesContractId;
		NextFuturesContractId = nextFuturesContractId;
		Timestamp = timestamp;
		IntervalType = intervalType;
		Difference = difference;
	}
}