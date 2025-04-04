using System.ComponentModel;

namespace Futures.Domain.Enums;

public enum ContractType
{
	[Description("CURRENT_QUARTER")]
	CurrentQuarter,

	[Description("NEXT_QUARTER")]
	NextQuarter
}