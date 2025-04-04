using System.ComponentModel;

namespace Futures.Domain.Enums;

public enum TimeIntervalType
{
	[Description(nameof(None))]
	None,

	[Description(nameof(Hourly))]
	Hourly,

	[Description(nameof(Daily))]
	Daily
}