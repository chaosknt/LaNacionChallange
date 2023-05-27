using System;
using System.Linq.Expressions;
using System.Reflection;

namespace LaNacion.Common.Filters
{
    public static class ExpressionHelper
    {
        public static Expression<Func<T, bool>> CreateFilterEqualExpression<T>(string paramName, object paramVal)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
            MemberExpression property = Expression.Property(parameter, paramName);
            ConstantExpression value = Expression.Constant(paramVal);
            BinaryExpression equalExpression = Expression.Equal(property, value);

            Expression<Func<T, bool>> lambdaExpression = Expression.Lambda<Func<T, bool>>(equalExpression, parameter);

            return lambdaExpression;
        }

        public static Expression<Func<T, bool>> CreateFilterContainsExpression<T>(string paramName, object partialValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "x");
            MemberExpression property = Expression.Property(parameter, paramName);
            MethodInfo containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            ConstantExpression value = Expression.Constant(partialValue);
            MethodCallExpression containsExpression = Expression.Call(property, containsMethod, value);

            Expression<Func<T, bool>> lambdaExpression = Expression.Lambda<Func<T, bool>>(containsExpression, parameter);

            return lambdaExpression;
        }
    }
}
