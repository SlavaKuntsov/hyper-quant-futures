namespace Futures.Application.DTOs;

public record BinanceErrorDto(
	int Code,
	string Message);