using System.ComponentModel;
using System.Reflection;

namespace Futures.Domain.Extensions;

public static class EnumExtensions
{
	public static string GetDescription(this Enum value)
	{
		var field = value.GetType().GetField(value.ToString());
		var attribute = (DescriptionAttribute)field.GetCustomAttribute(typeof(DescriptionAttribute));

		return attribute == null ? value.ToString() : attribute.Description;
	}
}