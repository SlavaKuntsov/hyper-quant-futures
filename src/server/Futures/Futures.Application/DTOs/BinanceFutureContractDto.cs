namespace Futures.Application.DTOs;

public record BinanceFutureContractDto(
	string Symbol,
	string ContractType,
	DateTime DeliveryDate);