using System.ComponentModel.DataAnnotations;

namespace NRIAwards.Common.Entity.Enum;

public static class EnumExtensions
{
    public static string GetDisplayName<T>(this T value) where T : System.Enum
    {
        return value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DisplayAttribute), false)
            .Cast<DisplayAttribute>().FirstOrDefault()?.GetName() ?? value.ToString();
    }
}