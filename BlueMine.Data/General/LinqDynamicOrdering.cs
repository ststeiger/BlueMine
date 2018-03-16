
namespace System.Linq
{


    public static class LinqDynamicOrdering
    {

        // https://stackoverflow.com/questions/41244/dynamic-linq-orderby-on-ienumerablet/233505#233505
        private static void Test()
        {
            System.Collections.Generic.List<string> ls =
                new System.Collections.Generic.List<string>();

            ls.AsQueryable().OrderBy("a, b asc, c desc");
        } // End Sub Test 


        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "OrderBy");
        }

        public static IOrderedQueryable<T> OrderByDescending<T>(
            this IQueryable<T> source,
            string property)
        {
            return ApplyOrder<T>(source, property, "OrderByDescending");
        }

        public static IOrderedQueryable<T> ThenBy<T>(
            this IOrderedQueryable<T> source,
            string property)
        {
            return ApplyOrder<T>(source, property, "ThenBy");
        }

        public static IOrderedQueryable<T> ThenByDescending<T>(
            this IOrderedQueryable<T> source,
            string property)
        {
            return ApplyOrder<T>(source, property, "ThenByDescending");
        }

        static IOrderedQueryable<T> ApplyOrder<T>(
            IQueryable<T> source,
            string property,
            string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof(T);

            Expressions.ParameterExpression arg =
                Expressions.Expression.Parameter(type, "x");

            Expressions.Expression expr = arg;
            foreach (string prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ
                Reflection.PropertyInfo pi = type.GetProperty(prop);
                expr = Expressions.Expression.Property(expr, pi);
                type = pi.PropertyType;
            } // Next prop 

            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);

            Expressions.LambdaExpression lambda =
                Expressions.Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });

            return (IOrderedQueryable<T>)result;
        } // End Function ApplyOrder 


    } // End  static class LinqDynamicOrdering 


} // End Namespace System.Linq
