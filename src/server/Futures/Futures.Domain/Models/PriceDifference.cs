using Futures.Domain.Enums;

namespace Futures.Domain.Models;

public class PriceDifference
{
	public Guid Id { get; set; }
	public Guid FuturesContractId { get; set; }
	public DateTime Timestamp { get; set; }
	public TimeIntervalType IntervalType { get; set; }
	public decimal Difference { get; set; }
	
	public virtual FutureContract FutureContract { get; set; } = null!;

	public PriceDifference()
	{
	}
}