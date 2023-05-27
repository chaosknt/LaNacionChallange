using System;
using System.Reflection;

namespace LaNacion.Common.Reflexion
{
    public static class ReflectionHelper
    {
        public static bool DoesPropertyExist(Type classType, string propertyName)
        {
            PropertyInfo propertyInfo = classType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);

            return propertyInfo != null;
        }
    }
}