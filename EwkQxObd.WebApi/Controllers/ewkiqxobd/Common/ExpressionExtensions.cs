using System.Linq.Expressions;

namespace EwkQxObd.WebApi.Controllers.ewkiqxobd.Common
{
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> OrElse<T>(
            this Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right)
        {
            var parameter = Expression.Parameter(typeof(T));

            var leftVisitor = new ReplaceParameterVisitor(left.Parameters[0], parameter);
            var leftBody = leftVisitor.Visit(left.Body);

            var rightVisitor = new ReplaceParameterVisitor(right.Parameters[0], parameter);
            var rightBody = rightVisitor.Visit(right.Body);

            return Expression.Lambda<Func<T, bool>>(
                Expression.OrElse(leftBody!, rightBody!),
                parameter
            );
        }
    }
}
