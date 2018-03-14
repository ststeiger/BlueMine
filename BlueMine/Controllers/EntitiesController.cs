// http://www.c-sharpcorner.com/UploadFile/dacca2/attribute-routing-and-parameters-in-mvc5/

// https://www.hanselman.com/blog/AddingACustomInlineRouteConstraintInASPNETCore10.aspx
// https://www.tektutorialshub.com/route-constraints-asp-net-core/
// https://www.hanselman.com/blog/AddingACustomInlineRouteConstraintInASPNETCore10.aspx


using System;
using Microsoft.AspNetCore.Http.Extensions;
using WilderMinds.RssSyndication;
using Xml2CSharp;
using Item = WilderMinds.RssSyndication.Item;


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
        
        
        [Microsoft.AspNetCore.Mvc.HttpGet("WilderFeed")]
        public Microsoft.AspNetCore.Mvc.IActionResult WilderFeed()
        {

            Feed feed = new Feed()
            {
                Title = "Steve's Blog",
                Description = "My Rants and Raves",
                Link = new System.Uri("http://xy.com/feed"),
                Copyright = "© " + System.DateTime.Now.Year + " XY Technologies LLC®™, All rights reserved."
            };
            
            
            string license = @"<div>
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
            string ad =
                @"<hr/><div>If you liked this article, see Shawn's courses on <a href=""http://shawnw.me/psauthor"">Pluralsight</a>.</div>";
            
            
            foreach (Db.BlogStory entry in this.m_repo.GetStories())
            {
                Item item = new Item()
                {
                    Title = entry.Title,
                    Body = string.Concat(entry.Body, license, ad),
                    Link = new System.Uri(new System.Uri(Request.GetEncodedUrl()), entry.Slug),
                    Permalink = new System.Uri(new System.Uri(Request.GetEncodedUrl()), entry.Slug).ToString(),
                    PublishDate = entry.DatePublished,
                    Author = new Author() { Name = "Noobie Noob", Email = "noob@noobie.com" }
                };
                
                item.Categories.AddRange(entry.Categories.Split(','));
                
                feed.Items.Add(item);
            } // Next entry 
            
            return File(System.Text.Encoding.UTF8.GetBytes(feed.Serialize()), "text/xml;charset=utf-8");
        }
        
        
        
        public static string Num2Hex(ulong num, ulong @base)
        {
            string retValue = null;
            string latinBase = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if((int)@base > latinBase.Length)
                throw new System.ArgumentException("Base value not supported.");
            
            /* 
            // 0A3555D7-9986-4CAC-A295-1492A59BFF8B
            // 8 - 4 - 4 -4 - 12
            // {8, 13, 18, 23}
            
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            int i = 0;
            
            do
            {
                char c = latinBase[(int)(num % @base)];
                sb.Insert(0, c);
                num = num/@base;
                ++i;
                
                if (i == 8 || i == 13 || i == 18 || i == 23)
                {
                    sb.Insert(0, '-');
                    ++i;
                }
                
            } while( num > 0);
            
            retValue = sb.ToString();
            sb.Clear();
            sb = null;
            */
            
            
            char[] result = new char[36];
            // for (int i = 35; i > -1; --i)
            for(int i = 0; i < 36; ++i)
            {
                if (i == 8 || i == 13 || i == 18 || i == 23)
                {
                    result[i] = '-';
                    continue;
                }
                
                result[i] = latinBase[(int)(num % @base)];
                num = num/@base;
            }
            
            retValue = new String(result);
            return retValue;
        }
        
        
        
        [Microsoft.AspNetCore.Mvc.HttpGet("feed")]
        public Microsoft.AspNetCore.Mvc.IActionResult Feed()
        {
            string fn = System.IO.Path.Combine(this.m_env.WebRootPath, "Gravity-Assist.rss");
            Xml2CSharp.Rss rss2 = Tools.XML.Serialization.DeserializeXmlFromFile<Xml2CSharp.Rss>(fn);
            // System.Console.WriteLine(rss);
            
            
            
            Xml2CSharp.Rss rss = new Rss();
            
            rss.Channel = new Channel();
            rss.Channel.Author = "FooFoo";
            rss.Channel.Category = new Category();
            rss.Channel.Category.Text = "MyCategory";
            rss.Channel.Title = "MyChannel";
            rss.Channel.Subtitle = "How to channelize";
            rss.Channel.Description = "Describe";
            rss.Channel.Copyright = "2016 StS";
            rss.Channel.Generator = "BlueMine";
            rss.Channel.Image = new Image();
            rss.Channel.Image.Url = "http://google.com";
            rss.Channel.Image.Title = "Alt Text?";
            rss.Channel.Image.Link2 = "https.//google.com";
            {
            }
            
            rss.Channel.Items.Add(
                new Xml2CSharp.Item()
                {
                    PubDate = System.DateTime.Now, Title = "someTitle"
                    , Description = "", CommentRss = "", 
                }    
            );
            
            
            string xml = Tools.XML.Serialization.SerializeToXml(rss);
            // System.Console.WriteLine(xml);
            
            return new TextResult(xml, "application/rss+xml");
        } // End Action Feed 
        
        
        private static System.Type GetEntityType(string entity)
        {
            if (entity == null)
                return null;

            entity = entity.ToLowerInvariant();
            System.Type type = System.Type.GetType("BlueMine.Db.T_" + entity + ", BlueMine");
            return type;
        } // End Function GetEntityType 


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
        } // End Action RemoveEntity 


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
        } // End Action GetEntity 


    } // End Class EntitiesController 


} // End Namespace BlueMine.Controllers 
