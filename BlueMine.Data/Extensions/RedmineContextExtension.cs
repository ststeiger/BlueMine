
using Microsoft.EntityFrameworkCore;


namespace BlueMine.Redmine
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


    // dotnet ef dbcontext scaffold "Server=CORDB2016\SDM,1532;Database=Redmine;Integrated Security=true;" Microsoft.EntityFrameworkCore.SqlServer --use-database-names -o Basigg
    // dotnet ef dbcontext scaffold "Server=CORDB2016\SDM,1532;Database=Redmine;Integrated Security=true;" Microsoft.EntityFrameworkCore.SqlServer --use-database-names –-fieldConventionName SnakeCase -o Basiggfdf 


    // -e |--entityConventionName Configure the convention name of entity class. 
    //             If omitted, the output code will will use only the same of database(‘CamelCase’, ‘SnakeCase’...)
    // -f |–-fieldConventionName Configure the convention name of field of entity class. 
    //     If omitted, the output code will use only the same of database. (‘CamelCase’, ‘SnakeCase’...)

    // PascalCase is the default. I believe what @toddtsic is asking is to allow snake_case as the default.
    // PascalCase: = UpperCamelCase
    // Snake case (or snake_case) is the practice of writing compound words or phrases 
    // in which the elements are separated with one underscore character(_) and no spaces, 
    // with each element's initial letter usually lowercased within the compound 
    // and the first letter either upper or lower case—as in "foo_bar" and "Hello_world" ...


    // dotnet ef dbcontext scaffold -h


    public partial class RedmineContext : DbContext
    {
        
        
        public RedmineContext(DbContextOptions<RedmineContext> options)
            : base(options)
        {
            this.ChangeTracker.AutoDetectChangesEnabled = false;
            this.Database.AutoTransactionsEnabled = true;
            this.Database.SetCommandTimeout(30);
            // this.Database.
            // this.Model.SqlServer().
        }
        
        
    }
    
    
}
