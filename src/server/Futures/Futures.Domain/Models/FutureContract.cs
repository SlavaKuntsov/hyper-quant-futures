using Futures.Domain.Enums;

namespace Futures.Domain.Models;

public class FutureContract
{
	public Guid Id { get; set; }
	public string Symbol { get; set; }
	public DateTime ExpirationDate { get; set; }
	public string Asset { get; set; }
	public ContractType ContractType { get; set; }

	public FutureContract()
	{
	}
	
	public FutureContract(
		Guid id,
		string symbol,
		DateTime expirationDate,
		string asset,
		ContractType contractType)
	{
		Id = id;
		Symbol = symbol;
		ExpirationDate = expirationDate;
		Asset = asset;
		ContractType = contractType;
	}
}