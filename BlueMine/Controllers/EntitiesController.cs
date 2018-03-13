// http://www.c-sharpcorner.com/UploadFile/dacca2/attribute-routing-and-parameters-in-mvc5/

// https://www.hanselman.com/blog/AddingACustomInlineRouteConstraintInASPNETCore10.aspx
// https://www.tektutorialshub.com/route-constraints-asp-net-core/
// https://www.hanselman.com/blog/AddingACustomInlineRouteConstraintInASPNETCore10.aspx


using Microsoft.AspNetCore.Http.Extensions;
using WilderMinds.RssSyndication;


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


        [Microsoft.AspNetCore.Mvc.HttpGet("feed")]
        public Microsoft.AspNetCore.Mvc.IActionResult Feed()
        {
            var feed = new Feed()
            {
                Title = "Shawn Wildermuth's Blog",
                Description = "My Favorite Rants and Raves",
                Link = new System.Uri("http://wildermuth.com/feed"),
                Copyright = "© 2016 Wilder Minds LLC"
            };

            var license = @"<div>
        <div style=""float: left;"">
          <a rel=""license"" href=""http://creativecommons.org/licenses/by-nc-nd/3.0/"">
            <img alt=""Creative Commons License"" style=""border-width: 0"" src=""http://i.creativecommons.org/l/by-nc-nd/3.0/88x31.png"" /></a></div>
        <div>
          This work by <a xmlns:cc=""http://creativecommons.org/ns#"" href=""http://wildermuth.com""
            property=""cc:attributionName"" rel=""cc:attributionURL"">Shawn Wildermuth</a> is
          licensed under a <a rel=""license"" href=""http://creativecommons.org/licenses/by-nc-nd/3.0/"">
            Creative Commons Attribution-NonCommercial-NoDerivs 3.0 Unported License</a>.<br />
          Based on a work at <a xmlns:dct=""http://purl.org/dc/terms/"" href=""http://wildermuth.com""
            rel=""dct:source"">wildermuth.com</a>.</div>
        </div>";
            var ad =
                @"<hr/><div>If you liked this article, see Shawn's courses on <a href=""http://shawnw.me/psauthor"">Pluralsight</a>.</div>";


            var entries = this.m_repo.GetStories();

            foreach (var entry in entries)
            {
                var item = new Item()
                {
                    Title = entry.Title,
                    Body = string.Concat(entry.Body, license, ad),
                    Link = new System.Uri(new System.Uri(Request.GetEncodedUrl()), entry.Slug),
                    Permalink = new System.Uri(new System.Uri(Request.GetEncodedUrl()), entry.Slug).ToString(),
                    PublishDate = entry.DatePublished,
                    Author = new Author() {Name = "Shawn Wildermuth", Email = "shawn@wildermuth.com"}
                };

                item.Categories.AddRange(entry.Categories.Split(','));


                feed.Items.Add(item);
            }

            return File(System.Text.Encoding.UTF8.GetBytes(feed.Serialize()), "text/xml");
        }


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
                new System.Collections.Generic.List<System.Type>()
                {
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