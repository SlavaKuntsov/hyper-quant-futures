namespace Futures.Infrastructure.Auth;

public sealed record JwtOptions(
	int ExpiresHours,
	string SecretKey,
	int AccessTokenExpirationMinutes,
	int RefreshTokenExpirationDays);