using System.ComponentModel;

namespace Futures.Domain.Enums;

public enum Role
{
	[Description("User")]
	User = 0,
	[Description("Admin")]
	Admin = 1
}