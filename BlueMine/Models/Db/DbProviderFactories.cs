
namespace Mono.Sucks
{
    // https://github.com/npgsql/Npgsql.EntityFrameworkCore.PostgreSQL/issues/159
    // https://stackoverflow.com/questions/7005302/postgresql-how-to-make-case-insensitive-query
    // https://blogs.msdn.microsoft.com/dotnet/2017/08/14/announcing-entity-framework-core-2-0/
    // https://github.com/aspnet/EntityFrameworkCore/issues/4797
    // https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools.DotNet/


    // https://docs.microsoft.com/en-us/ef/core/get-started/install/
    // https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet
    // https://stackoverflow.com/questions/45202713/how-to-update-version-of-microsoft-netcore-app-sdk-in-vs-2017


    // 1. Add entries manually to csproj
    // 2. update runtimeframework-version
    // <TargetFramework>netcoreapp2.0</TargetFramework>
    // <RuntimeFrameworkVersion>2.0.3</RuntimeFrameworkVersion>
    // cd /d D:\username\Documents\Visual Studio 2017\Projects\BlueMine\BlueMine
    // # dotnet add package Microsoft.EntityFrameworkCore.Design
    // 3. Manually restore packages
    // dotnet restore
    // 4. dotnet ef runs in same folder as csproj

    // Scaffold-DbContext "Server=(localdb)\mssqllocaldb;Database=IssueTracker;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
    // Scaffold-DbContext "Server=COR-W10-112\\SQLEXPRESS;Database=IssueTracker;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

    // https://blog.jetbrains.com/dotnet/2017/08/09/running-entity-framework-core-commands-rider/
    // https://stackoverflow.com/questions/48086047/rider-ef-code-first-migrations

    // dotnet ef dbcontext scaffold "Server=COR-W10-112\\SQLEXPRESS;Integrated Security=true;" Microsoft.EntityFrameworkCore.SqlServer -c AcmeDataContext
    // dotnet ef dbcontext scaffold "Server=COR-W10-112\SQLEXPRESS;User ID=SomeWebSerice;Password=TOP_SECRET" Microsoft.EntityFrameworkCore.SqlServer -c AcmeDataContext
    // dotnet ef dbcontext scaffold "Server=localhost;User ID=SomeWebSerice;Password=TOP_SECRET" Microsoft.EntityFrameworkCore.SqlServer -c AcmeDataContext

    // dotnet ef dbcontext scaffold "Server=localhost;Database=COR_Basic_Demo_V4;User ID=SomeWebSerice;Password=TOP_SECRET;" Microsoft.EntityFrameworkCore.SqlServer -o Basigg    
    // dotnet ef dbcontext scaffold "Server=localhost;Database=COR_Basic_Demo_V4;User ID=SomeWebSerice;Password=TOP_SECRET;" Microsoft.EntityFrameworkCore.SqlServer --data-annotations -o Basigg 
    // dotnet ef dbcontext scaffold "Server=COR-W10-112\SQLEXPRESS;Database=COR_Basic_Demo_V4;Integrated Security=true;" Microsoft.EntityFrameworkCore.SqlServer --data-annotations -o Basigg 
    // dotnet ef dbcontext scaffold "Server=COR-W10-112\SQLEXPRESS;Database=COR_Basic_Demo_V4;Integrated Security=true;" Microsoft.EntityFrameworkCore.SqlServer --use-database-names --data-annotations -o Basigg 

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
