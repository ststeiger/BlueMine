
namespace System.Linq
{
    
    public class DynamicOrdering
    {
        public System.Linq.Expressions.Expression Selector;
        public bool Ascending;


        public DynamicOrdering()
        { }
        
        public DynamicOrdering(string expr, System.Type type)
        {
            string[] parts = expr.Split(new char[]{' ', '\t', '\r', '\n'},
                System.StringSplitOptions.RemoveEmptyEntries
            );
            
            this.Selector = GetSelector(parts[0], type);
            this.Ascending = System.StringComparer
                .InvariantCultureIgnoreCase.Equals(parts[1], "asc");
        }


        public System.Linq.Expressions.Expression GetSelector(
            string member,
            System.Type entityType
            )
        {
            
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
            
            // use reflection (not ComponentModel) to mirror LINQ
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
    
    
    public static class aaaaaa
    {

        public static IQueryable<T> OrderBy<T>(
            this IQueryable<T> source, string sortString)
        {
            System.Type t = typeof(T);
            string[] expressions = sortString.Split(new char[] {',', ';'},
                System.StringSplitOptions.RemoveEmptyEntries
            );
            
            System.Collections.Generic.List<DynamicOrdering> ls = 
                new System.Collections.Generic.List<DynamicOrdering>();
            
            for (int i = 0; i < expressions.Length; ++i)
            {
                expressions[i] = expressions[i].Trim();
                ls.Add(new DynamicOrdering(expressions[i], t));
            }
            
            return (IQueryable<T>) OrderBy<T>(source, ls);
        }
        
        
        public static IQueryable OrderBy<T>(
            this IQueryable source,
            System.Collections.Generic.IEnumerable<DynamicOrdering> orderings
            )
        {
            Type type = typeof(T);
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);

            System.Linq.Expressions.ParameterExpression[] parameters = 
                new System.Linq.Expressions.ParameterExpression[] {
                System.Linq.Expressions.Expression.Parameter(source.ElementType, "") 
            };
            
            System.Linq.Expressions.Expression queryExpr = source.Expression;
            string methodAsc = "OrderBy";
            string methodDesc = "OrderByDescending";
            foreach (DynamicOrdering o in orderings) 
            {
                queryExpr = System.Linq.Expressions.Expression.Call(
                    typeof(Queryable), o.Ascending ? methodAsc : methodDesc,
                    new Type[] { source.ElementType, o.Selector.Type },
                    queryExpr, System.Linq.Expressions.Expression
                        .Quote(System.Linq.Expressions.Expression
                            .Lambda(o.Selector, parameters)));
                // methodAsc = "ThenBy";
                // methodDesc = "ThenByDescending";
            }
            
            return source.Provider.CreateQuery(queryExpr);
        }
        
        
    }
    
    
}
