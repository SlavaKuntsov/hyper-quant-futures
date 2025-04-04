namespace Futures.Domain.Models;

public class PricePoint
{
	public Guid Id { get; set; }
	public Guid FutureContractId { get; set; }
	public DateTime Timestamp { get; set; }
	public decimal Price { get; set; }

	public virtual FutureContract FutureContract { get; set; } = null!;

	public PricePoint()
	{
	}

	public PricePoint(
		Guid id,
		Guid futureContractId,
		DateTime timestamp,
		decimal price)
	{
		Id = id;
		FutureContractId = futureContractId;
		Timestamp = timestamp;
		Price = price;
	}
}