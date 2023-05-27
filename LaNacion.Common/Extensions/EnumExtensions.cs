using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace LaNacion.Common.Extensions
{
    public static class EnumExtensions
    {
        public static string GetFriendlyName<TEnumType>(this TEnumType enumValue) where TEnumType : struct
        {
            return enumValue.GetAttribute<TEnumType, DisplayAttribute>()?.Name ?? enumValue.ToString();
        }

        private static TAttribute GetAttribute<TEnumType, TAttribute>(this TEnumType enumValue) where TEnumType : struct
               where TAttribute : Attribute
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .FirstOrDefault()?
                            .GetCustomAttribute<TAttribute>();
        }

        public static int AsInt<TEnumType>(this TEnumType value) where TEnumType : struct, IConvertible
        {
            if (!typeof(TEnumType).IsEnum)
            {
                throw new ArgumentException("TEnumType must be an enumerated type");
            }

            return Convert.ToInt32(value);
        }

        public static Dictionary<string, TEnum> ToDisplayValueDictionary<TEnum>()
        {
            var enumType = typeof(TEnum);
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("The specified type is not an enum.");
            }

            var dictionary = new Dictionary<string, TEnum>();
            var names = System.Enum.GetNames(enumType);

            foreach (var name in names)
            {
                var enumValue = (TEnum)System.Enum.Parse(enumType, name);
                var fieldInfo = enumType.GetField(name);
                var displayAttribute = fieldInfo.GetCustomAttribute<DisplayAttribute>();
                var displayValue = displayAttribute?.GetName() ?? enumValue.ToString();

                dictionary.Add(displayValue, enumValue);
            }

            return dictionary;
        }
    }
}
