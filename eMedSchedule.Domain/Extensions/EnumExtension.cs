using System.ComponentModel;
using System.Reflection;

namespace eMedSchedule.Domain.Extensions
{
    public static class EnumExtension
    {
        public static string ToDescriptionString<TEnum>(this TEnum @enum) where TEnum : Enum
        {
            string name = Enum.GetName(typeof(TEnum), @enum);
            FieldInfo field = typeof(TEnum).GetField(name);
            DescriptionAttribute attribute = field?.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? name;
        }
    }
}