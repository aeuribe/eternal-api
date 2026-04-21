using System.ComponentModel;
using System.Reflection;

namespace eternal_api.Domain.Enums
{
    public static class StateEnumExtensions
    {
        public static string GetFullName(this StateUsEnum state)
        {
            FieldInfo? field = state.GetType().GetField(state.ToString());

            DescriptionAttribute? attribute = field?
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .FirstOrDefault() as DescriptionAttribute;

            return attribute?.Description ?? state.ToString();
        }

        public static string GetPrefix(this StateUsEnum state)
        {
            return state.ToString();
        }
    }
}