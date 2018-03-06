
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Remotion.Linq.Parsing.Structure;


namespace Microsoft.EntityFrameworkCore 
{
    

    // https://stackoverflow.com/questions/1412863/how-do-i-view-the-sql-generated-by-the-entity-framework
    // http://rion.io/2016/10/19/accessing-entity-framework-core-queries-behind-the-scenes-in-asp-net-core/
    // https://gist.github.com/rionmonster/2c59f449e67edf8cd6164e9fe66c545a
    // https://github.com/rionmonster
    public static class IQueryableExtensions
    {
        private static readonly TypeInfo QueryCompilerTypeInfo = typeof(QueryCompiler).GetTypeInfo();

        private static readonly FieldInfo QueryCompilerField = typeof(EntityQueryProvider).GetTypeInfo().DeclaredFields
            .First(x => x.Name == "_queryCompiler");

        private static readonly PropertyInfo NodeTypeProviderField =
            QueryCompilerTypeInfo.DeclaredProperties.Single(x => x.Name == "NodeTypeProvider");

        private static readonly MethodInfo CreateQueryParserMethod =
            QueryCompilerTypeInfo.DeclaredMethods.First(x => x.Name == "CreateQueryParser");

        private static readonly FieldInfo DataBaseField =
            QueryCompilerTypeInfo.DeclaredFields.Single(x => x.Name == "_database");

        private static readonly PropertyInfo DatabaseDependenciesField =
            typeof(Database).GetTypeInfo().DeclaredProperties.Single(x => x.Name == "Dependencies");

        public static string ToSql<TEntity>(this IQueryable<TEntity> query) where TEntity : class
        {
            if (!(query is EntityQueryable<TEntity>) && !(query is InternalDbSet<TEntity>))
            {
                throw new System.ArgumentException("Invalid query");
            }

            var queryCompiler = (QueryCompiler) QueryCompilerField.GetValue(query.Provider);
            var nodeTypeProvider = (INodeTypeProvider) NodeTypeProviderField.GetValue(queryCompiler);
            var parser = (IQueryParser) CreateQueryParserMethod.Invoke(queryCompiler, new object[] {nodeTypeProvider});
            var queryModel = parser.GetParsedQuery(query.Expression);
            var database = DataBaseField.GetValue(queryCompiler);
            var databaseDependencies = (DatabaseDependencies) DatabaseDependenciesField.GetValue(database);
            var queryCompilationContext = databaseDependencies.QueryCompilationContextFactory.Create(false);
            var modelVisitor = (RelationalQueryModelVisitor) queryCompilationContext.CreateQueryModelVisitor();
            modelVisitor.CreateQueryExecutor<TEntity>(queryModel);
            var sql = modelVisitor.Queries.First().ToString();

            return sql;
        } // End Function ToSql 
    } // End Class IQueryableExtensions 



    public class IQueryableExtensions1
    {
        private static readonly TypeInfo QueryCompilerTypeInfo = typeof(QueryCompiler).GetTypeInfo();

        private static readonly FieldInfo QueryCompilerField = typeof(EntityQueryProvider).GetTypeInfo()
            .DeclaredFields
            .First(x => x.Name == "_queryCompiler");

        private static readonly PropertyInfo NodeTypeProviderField =
            QueryCompilerTypeInfo.DeclaredProperties.Single(x => x.Name == "NodeTypeProvider");

        private static readonly MethodInfo CreateQueryParserMethod =
            QueryCompilerTypeInfo.DeclaredMethods.First(x => x.Name == "CreateQueryParser");

        private static readonly FieldInfo DataBaseField =
            QueryCompilerTypeInfo.DeclaredFields.Single(x => x.Name == "_database");

        private static readonly FieldInfo QueryCompilationContextFactoryField = typeof(Database).GetTypeInfo()
            .DeclaredFields.Single(x => x.Name == "_queryCompilationContextFactory");


        public static string ToSql<TEntity>(IQueryable<TEntity> query) where TEntity : class
        {
            if (!(query is EntityQueryable<TEntity>) && !(query is InternalDbSet<TEntity>))
            {
                throw new System.ArgumentException("Invalid query");
            }

            var queryCompiler = (IQueryCompiler) QueryCompilerField.GetValue(query.Provider);

            var nodeTypeProvider = (INodeTypeProvider) NodeTypeProviderField.GetValue(queryCompiler);
            var parser =
                (IQueryParser) CreateQueryParserMethod.Invoke(queryCompiler, new object[] {nodeTypeProvider});
            var queryModel = parser.GetParsedQuery(query.Expression);
            var database = DataBaseField.GetValue(queryCompiler);
            var queryCompilationContextFactory =
                (IQueryCompilationContextFactory) QueryCompilationContextFactoryField.GetValue(database);
            var queryCompilationContext = queryCompilationContextFactory.Create(false);
            var modelVisitor = (RelationalQueryModelVisitor) queryCompilationContext.CreateQueryModelVisitor();
            modelVisitor.CreateQueryExecutor<TEntity>(queryModel);
            var sql = modelVisitor.Queries.First().ToString();

            return sql;
        } // End Function ToSql 


    } // End Class IQueryableExtensions1 


} // End Namespace Microsoft.EntityFrameworkCore 
