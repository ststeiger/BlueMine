
namespace Mono.Sucks
{
    // https://github.com/npgsql/Npgsql.EntityFrameworkCore.PostgreSQL/issues/159
    // https://stackoverflow.com/questions/7005302/postgresql-how-to-make-case-insensitive-query
    // https://blogs.msdn.microsoft.com/dotnet/2017/08/14/announcing-entity-framework-core-2-0/


    // https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet


    public static class DbProviderFactories
    {


        public static System.Data.Common.DbProviderFactory GetFactory<T>()
        {
            System.Type t = typeof(T);
            return GetFactory(t);
        } // End Function GetFactory


        public static System.Data.Common.DbProviderFactory GetFactory(string assemblyType)
        {
#if TARGET_JVM // case insensitive GetType is not supported
			Type type = Type.GetType (assemblyType, false);
#else
            System.Type type = System.Type.GetType(assemblyType, false, true);
#endif

            return GetFactory(type);
        } // End Function GetFactory


        public static System.Data.Common.DbProviderFactory GetFactory(System.Type type)
        {
            if (type != null && type.IsSubclassOf(typeof(System.Data.Common.DbProviderFactory)))
            {
                // Provider factories are singletons with Instance field having
                // the sole instance
                System.Reflection.FieldInfo field = type.GetField("Instance"
                    , System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static
                );

                if (field != null)
                {
                    return (System.Data.Common.DbProviderFactory)field.GetValue(null);
                    //return field.GetValue(null) as DbProviderFactory;
                } // End if (field != null)

            } // End if (type != null && type.IsSubclassOf(typeof(System.Data.Common.DbProviderFactory)))

            throw new System.InvalidOperationException("DataProvider is missing!");
        } // End Function GetFactory

    } // End Class DbProviderFactories


} // End Namespace Mono.Sucks
