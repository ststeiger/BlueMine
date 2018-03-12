
// http://www.c-sharpcorner.com/UploadFile/dacca2/attribute-routing-and-parameters-in-mvc5/

// https://www.hanselman.com/blog/AddingACustomInlineRouteConstraintInASPNETCore10.aspx
// https://www.tektutorialshub.com/route-constraints-asp-net-core/
// https://www.hanselman.com/blog/AddingACustomInlineRouteConstraintInASPNETCore10.aspx

namespace BlueMine.Controllers
{
    
    
    // [Route("api/[controller]")]
    public class EntitiesController 
        : Microsoft.AspNetCore.Mvc.Controller
    {
        
        private readonly BlueMine.Db.BlueMineRepository m_repo;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment m_env;
        
        
        public EntitiesController(BlueMine.Db.BlueMineRepository repo 
            , Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this.m_repo = repo;
            this.m_env = env;
        } // End Constructor 


        private static System.Type GetEntityType(string entity)
        {
            if (entity == null)
                return null;

            entity = entity.ToLowerInvariant();
            System.Type type = System.Type.GetType("BlueMine.Db.T_" + entity + ", BlueMine");
            return type;
        }


        [Microsoft.AspNetCore.Mvc.HttpGet("API/{entity:BlueMineTable}/Remove/{id:int}")]
        public JsonpResult RemoveEntity(string entity, int? id)
        {
            System.Type type = GetEntityType(entity);
            if (type == null)
                return new JsonpResult(null);

            if (id.HasValue)
            {
                this.m_repo.RemoveById(type, id.Value);
                return new JsonpResult(1);
            } // End if (id.HasValue) 

            return new JsonpResult(null);
        }


        // http://localhost:55337/API/issues_history
        [Microsoft.AspNetCore.Mvc.HttpGet("API/{entity:BlueMineTable}/{id:int?}")]
        public JsonpResult GetEntity(string entity, int? id)
        {
            System.Type type = GetEntityType(entity);
            if (type == null)
                return new JsonpResult(null);
            
            System.Collections.Generic.List<System.Type> lsProhibited = 
                new System.Collections.Generic.List<System.Type>() {
                typeof(BlueMine.Db.T_settings)
            };
            
            if (lsProhibited.Contains(type))
                return new JsonpResult(null);
            
            if (id.HasValue)
            {
                object objectById = this.m_repo.FindById(type, id.Value);
                return new JsonpResult(objectById);
            } // End if (id.HasValue) 
            
            object ls = this.m_repo.GetAll(type);
            return new JsonpResult(ls);
        } // End Function GetEntity 
        
        
    } // End Class EntitiesRouteConstraint<T> 
    
    
} // End Namespace BlueMine.Controllers 
