using System;
using System.Linq.Expressions;

namespace Recipes.Client
{
    public static class ExtendSimpleExpression
    {
        public static string GetPropertyName<T>(this Expression<Func<T, object>> expression)
        {
            return GetMemberExpression(expression).Member.Name;
        }

        public static bool IsInvertedPropertyExpression<T>(this Expression<Func<T, object>> expression)
        {
            if (expression.Body.NodeType == ExpressionType.Convert)
                return ((UnaryExpression)expression.Body).Operand.NodeType == ExpressionType.Not;
            return false;
        }

        private static MemberExpression GetMemberExpression<TModel, T>(Expression<Func<TModel, T>> expression)
        {
            var memberExpression = (MemberExpression)null;
            if (expression.Body.NodeType == ExpressionType.Convert)
            {
                var unaryExpression = (UnaryExpression)expression.Body;
                memberExpression = unaryExpression.Operand.NodeType != ExpressionType.Not ? unaryExpression.Operand as MemberExpression : ((UnaryExpression)unaryExpression.Operand).Operand as MemberExpression;
            }
            else if (expression.Body.NodeType == ExpressionType.MemberAccess)
                memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
                throw new ArgumentException("Not a member access");
            return memberExpression;
        }
    }
}