using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ctor.Application.Common.Enums;

namespace Ctor.Application.Common.Extensions;
public static class LinqExtensions
{
    public static IQueryable<T> DynamicOrderBy<T>(this IQueryable<T> query, string orderBy, Order order = Order.ASC)
    {
        //if (typeof(T).GetProperty(orderBy) == null)
        //    throw new ArgumentException($"Sort property '{orderBy}' invalid");

        var parameter = Expression.Parameter(query.ElementType);
        var property = Expression.Property(parameter, orderBy);
        var lambda = Expression.Lambda(property, parameter);

        var methodName = order == Order.DESC ? "OrderByDescending" : "OrderBy";

        var methodCallExpression = Expression.Call(typeof(Queryable), 
            methodName, new Type[] { query.ElementType, property.Type },
            query.Expression, Expression.Quote(lambda));

        return query.Provider.CreateQuery<T>(methodCallExpression);
    }
}
