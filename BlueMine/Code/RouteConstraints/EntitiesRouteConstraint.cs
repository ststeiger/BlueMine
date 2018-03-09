
namespace BlueMine.RouteConstraints
{
    
    
    public class EntitiesRouteConstraint<TDbContext, TGenericRepository, TRepository> 
        : Microsoft.AspNetCore.Routing.IRouteConstraint 
        // we need TDbContext to define TGenericRepository
        where TDbContext: Microsoft.EntityFrameworkCore.DbContext
        // we need TGenericRepository to call our function
        where TGenericRepository: BlueMine.Db.GenericEntityFramworkRepository<TDbContext>
        where TRepository: TGenericRepository // we need TRepository for DI resolution 
        // TODO: Define interface for repository so EF can be dropped as dependency
    {
        
        private static object s_lock;
        private static System.Collections.Generic.HashSet<string> s_tables;
        
        
        static EntitiesRouteConstraint()
        {
            s_lock = new object();
        }
        
        
        public EntitiesRouteConstraint()
        { }
        
        
        // https://stackoverflow.com/questions/45667126/how-to-get-table-name-of-mapped-entity-in-entity-framework-core
        public void InitializeHashSet(System.IServiceProvider services)
        {
            lock (s_lock)
            {
                if (s_tables == null)
                {
                    TGenericRepository bmc = (TGenericRepository) 
                        services.GetService(typeof(TRepository));
                    
                    if (bmc == null)
                    {
                        // throw new System.NullReferenceException(nameof(bmc));
                        return;
                    } // End if (bmc == null)
                    
                    System.Collections.Generic.HashSet<string> hs = 
                        new System.Collections.Generic.HashSet<string>(
                        System.StringComparer.InvariantCultureIgnoreCase
                    );
                    
                    foreach (string table in bmc.ListTables())
                    {
                        hs.Add(table);
                    }
                    
                    s_tables = hs;
                } // End if (s_tables == null) 
                
            } // End lock (s_lock) 
            
        } // End Sub InitializeHashSet 
        
        
        bool Microsoft.AspNetCore.Routing.IRouteConstraint.Match(
              Microsoft.AspNetCore.Http.HttpContext httpContext
            , Microsoft.AspNetCore.Routing.IRouter route
            , string routeKey
            , Microsoft.AspNetCore.Routing.RouteValueDictionary values
            , Microsoft.AspNetCore.Routing.RouteDirection routeDirection)
        {
            if (routeDirection != Microsoft.AspNetCore.Routing.RouteDirection.IncomingRequest)
                return false;
            
            if (s_tables == null)
            {
                InitializeHashSet(httpContext.RequestServices);
            } // End if (s_tables == null)
            
            // object value = values[routeKey];
            
            object value;
            if (values.TryGetValue(routeKey, out value) && value != null)
            {
                return s_tables.Contains(value.ToString());
            }
            
            return false;
        } // End Function IRouteConstraint.Match
        
        
    } // End Function EntitiesRouteConstraint<T> 
    
    
}
