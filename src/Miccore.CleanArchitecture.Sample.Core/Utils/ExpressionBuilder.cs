using System.Linq.Expressions;

namespace Miccore.CleanArchitecture.Sample.Core.Utils;

public static class ExpressionBuilder<T> where T : class
{
    public static Expression<Func<T, bool>> CreateContainsExpression(List<string> columns, string value)
    {
        var param = Expression.Parameter(typeof(T), "t");
        Expression? body = null;

        foreach (var colums in columns)
        {
            var member = Expression.Property(param, colums);
            var constant = Expression.Constant(value);

            var expression = Expression.Call(member, "Contains", Type.EmptyTypes, constant);
            body = body == null ? expression : Expression.Or(body, expression);
        }

        return Expression.Lambda<Func<T, bool>>(body, param);
    }

    public static Expression<Func<T, bool>> CreateEqualExpression(string propertyName, object value)
    {
        var param = Expression.Parameter(typeof(T), "t");

        var member = Expression.Property(param, propertyName);

        var constant = Expression.Constant(value);

        var body = Expression.Equal(member, constant);

        return Expression.Lambda<Func<T, bool>>(body, param);
    }

    public static Expression<Func<T, bool>> ReduceExpression(Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        var invokedExpr = Expression.Invoke(right, left.Parameters.Cast<Expression>());

        var expression = Expression.AndAlso(left.Body, invokedExpr);

        return Expression.Lambda<Func<T, bool>>(expression, left.Parameters);;
    }
}