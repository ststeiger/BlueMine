
namespace BlueMine.RouteConstraints
{
    
    
    public class EnumRouteContraint 
        : Microsoft.AspNetCore.Routing.IRouteConstraint 
    {
        
        
        public EnumRouteContraint()
        { }


        bool Microsoft.AspNetCore.Routing.IRouteConstraint.Match(
            Microsoft.AspNetCore.Http.HttpContext httpContext
            , Microsoft.AspNetCore.Routing.IRouter route
            , string routeKey
            , Microsoft.AspNetCore.Routing.RouteValueDictionary values
            , Microsoft.AspNetCore.Routing.RouteDirection routeDirection)
        {
            // You can also try Enum.IsDefined,
            // but docs say nothing as to if it is case sensitive or not.

            // System.Type t = typeof(System.Drawing.KnownColor);
            System.Type t = System.Type.GetType("System.Drawing.KnownColor, System.Drawing.Primitives");

            string[] colors = System.Enum.GetNames(t);
            string key = System.Convert.ToString(values[routeKey]);
            
            foreach (string thisColor in colors)
            {
                if (System.StringComparer.InvariantCultureIgnoreCase.Equals(
                    thisColor, key))
                    return true;
            } // Next thisColor 
            
            return false;
        } // End Function IRouteConstraint.Match 
        
        
    } // End Class EnumRouteContraint 
    
    
} 
