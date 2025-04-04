namespace Futures.Domain.Models;

public class PricePoint
{
	public Guid Id { get; set; }
	public Guid FutureContractId { get; set; }
	public DateTime Timestamp { get; set; }
	public decimal Price { get; set; }

	public virtual FutureContract FutureContract { get; set; } = null!;
}