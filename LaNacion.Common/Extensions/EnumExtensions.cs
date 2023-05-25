using System;
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
    }
}
