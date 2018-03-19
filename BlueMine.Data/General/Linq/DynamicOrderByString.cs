
namespace System.Linq
{
    
    internal class OrderByLinqExpression
    {
        public System.Linq.Expressions.Expression Selector;
        public bool Ascending;


        internal OrderByLinqExpression()
        { }


        internal OrderByLinqExpression(string expr, System.Linq.Expressions.ParameterExpression instance)
        {
            string[] parts = expr.Split(new char[]{' ', '\t', '\r', '\n'},
                System.StringSplitOptions.RemoveEmptyEntries
            );
            
            this.Selector = System.Linq.Expressions.Expression.PropertyOrField(instance, parts[0]);
            this.Ascending = System.StringComparer
                .InvariantCultureIgnoreCase.Equals(parts[1], "ASC");
        }


        private static System.Linq.Expressions.Expression GetSelector_old(
            string member,
            System.Type entityType
            )
        {
            // Note: entityType = typeof(T);


            System.Linq.Expressions.ParameterExpression arg =
                System.Linq.Expressions.Expression.Parameter(entityType, "x");
            System.Linq.Expressions.Expression expr = arg;
            
            System.Reflection.BindingFlags bf =
                System.Reflection.BindingFlags.IgnoreCase
                | System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.NonPublic
                | System.Reflection.BindingFlags.Instance
                | System.Reflection.BindingFlags.Static;
            System.Type type = null;
            
            System.Reflection.PropertyInfo pi = entityType.GetProperty(member, bf);
            if (pi == null)
            {
                System.Reflection.FieldInfo fi = entityType.GetField(member, bf);
                expr = System.Linq.Expressions.Expression.Field(expr, fi);
                type = fi.FieldType;
            }
            else
            {
                expr = System.Linq.Expressions.Expression.Property(expr, pi);
                type = pi.PropertyType;    
            }

            Type delegateType = typeof(Func<,>).MakeGenericType(entityType, type);

            System.Linq.Expressions.LambdaExpression lambda =
                System.Linq.Expressions.Expression.Lambda(delegateType, expr, arg);

            return lambda;
        }
        
    }
    
    
    public static class DynamicOrderByString
    {

        public static IQueryable<T> OrderByString<T>(
            this IQueryable<T> source, string sortString)
        {
            System.Linq.Expressions.ParameterExpression[] parameters =
                new System.Linq.Expressions.ParameterExpression[] {
                System.Linq.Expressions.Expression.Parameter(source.ElementType, "")
            };

            string[] expressions = sortString.Split(new char[] {',', ';'},
                System.StringSplitOptions.RemoveEmptyEntries
            );
            
            System.Collections.Generic.List<OrderByLinqExpression> ls = 
                new System.Collections.Generic.List<OrderByLinqExpression>();
            
            for (int i = 0; i < expressions.Length; ++i)
            {
                expressions[i] = expressions[i].Trim();
                ls.Add(new OrderByLinqExpression(expressions[i], parameters[0]));
            }

            return (IQueryable<T>)OrderByString<T>(source, ls, parameters);
        }
        
        
        private static IQueryable OrderByString<T>(
            this IQueryable source,
            System.Collections.Generic.IEnumerable<OrderByLinqExpression> orderings,
            System.Linq.Expressions.ParameterExpression[] parameters
            )
        {
            Type type = typeof(T);
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);

            System.Linq.Expressions.Expression queryExpr = source.Expression;
            string methodAsc = "OrderBy";
            string methodDesc = "OrderByDescending";
            foreach (OrderByLinqExpression o in orderings) 
            {
                queryExpr = System.Linq.Expressions.Expression.Call(
                    typeof(Queryable), o.Ascending ? methodAsc : methodDesc,
                    new Type[] { source.ElementType, o.Selector.Type },
                    queryExpr, System.Linq.Expressions.Expression
                        .Quote(System.Linq.Expressions.Expression
                            .Lambda(o.Selector, parameters)));
                 methodAsc = "ThenBy";
                 methodDesc = "ThenByDescending";
            }
            
            return source.Provider.CreateQuery(queryExpr);
        }
        
        
    }
    
    
}
