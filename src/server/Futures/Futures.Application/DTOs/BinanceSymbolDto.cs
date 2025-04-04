namespace Futures.Application.DTOs;

public record BinanceSymbolDto(
	string Symbol,
	string ContractType,
	long DeliveryDate);