using Futures.Domain.Enums;

namespace Futures.Domain.Models;

public class FutureContract
{
	public Guid Id { get; set; }
	public string Symbol { get; set; }
	public DateTime ExpirationDate { get; set; }
	public string Asset { get; set; }
	public ContractType Type { get; set; }

	public FutureContract()
	{
	}
}