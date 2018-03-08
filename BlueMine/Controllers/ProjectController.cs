
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using BlueMine.Models.Project;




// For more information on enabling MVC for empty projects, 
// visit https://go.microsoft.com/fwlink/?LinkID=397860 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


// https://blogs.msdn.microsoft.com/dotnet/2017/08/14/announcing-entity-framework-core-2-0/
// https://blogs.msdn.microsoft.com/webdev/2013/10/17/attribute-routing-in-asp-net-mvc-5/
// https://stackoverflow.com/questions/1263635/route-all-controller-actions-to-a-single-method-then-display-a-view-according-t
// http://httpjunkie.com/2014/430/custom-routing-in-mvc5-with-attributes/
namespace BlueMine.Controllers
{
    
    
    // template: "{controller=Home}/{action=Index}/{id?}");
    // [Route("[controller]/[action]/{id?}")]
    // [Route("[controller]/{a?}/{id?}")]
    // [Route("[controller]/{*uri}")]

    public class ProjectController : Controller
    {
        private readonly BlueMine.Db.BlueMineRepository m_repo;
        protected Microsoft.AspNetCore.Hosting.IHostingEnvironment m_env;


        public ProjectController(BlueMine.Db.BlueMineRepository repo
            // ,Db.BlueMineContext context
            , Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            this.m_repo = repo;
            this.m_env = env;


            // var crud = new BlueMine.Data.CRUD(context);
            // crud.TestSqlGeneration("");

        }


        [HttpGet("/TestImage")]
        public async Task<IActionResult> GetTestImage()
        {
            string dir = System.IO.Path.Combine(this.m_env.WebRootPath, "images");
            string file = System.IO.Path.Combine(dir, "Waterfall.png");

            System.IO.Stream image = System.IO.File.OpenRead(file);
            return File(image, "image/jpeg");
        }

        [HttpGet("/TestImage1")]
        public ImageResult GetTestImage1()
        {
            string dir = System.IO.Path.Combine(this.m_env.WebRootPath, "images");
            string file = System.IO.Path.Combine(dir, "Waterfall.png");
            return new ImageResult("image/jpeg", System.IO.File.OpenRead(file));
        }


        [HttpGet("Image/{id}")]
        public IActionResult Image(int? id)
        {
            if (id == null)
                return this.NotFound();
            else
            {
                string dir = System.IO.Path.Combine(this.m_env.WebRootPath, "images");
                string file = System.IO.Path.Combine(dir, "Waterfall.png");

                byte[] imagen = System.IO.File.ReadAllBytes(file);
                return File(imagen, "image/jpeg");
            }
        }


        [HttpGet("Resize/Image/{id}")]
        public ImageResult ResizeImageImage(int? id)
        {
            string dir = System.IO.Path.Combine(this.m_env.WebRootPath, "images");
            string file = System.IO.Path.Combine(dir, "Waterfall.png");
            
            return new ImageResult(delegate(Microsoft.AspNetCore.Http.HttpContext context)
            {
                var a = context.RequestServices.GetService(typeof(string))();
                
                context.Response.ContentType = SaveFormat.Png.MimeType(); // this.ContentType;
                ImageHandler.ResizeImage(System.IO.File.OpenRead(file), 
                    context.Response.Body, SaveFormat.Png
                    , new System.Drawing.Size(20, 20));
                // response.ContentLength = this.m_stream.Length;

                // void foo(string file, SaveFormat format, System.Drawing.Size maxSize)
                
            });
            
            // ireturn new ImageResult("image/png", ImageHandler.ResizeImage(file, SaveFormat.Png, 2.0f));
        }


        [Route("/TestRepo")]
        public System.Collections.Generic.List<SelectListItem> LuL()
        {
            var se1 = BlueMine.Data.SortExpression<Db.T_projects>.Create(x => x.name);
            var se2 = BlueMine.Data.SortExpression<Db.T_projects>.Create(x => x.updated_on,
                Data.SortDirection.Descending);

            // return this.m_repo.GetFilteredSorted<Db.T_projects>(null, se1, se2);
            // return this.m_repo.GetFilteredSorted<Db.T_projects>(null, x => x.name);

            // return this.m_repo.GetFilteredSorted<Db.T_projects>(x => x.parent_id == 6, x => x.name);
            // return this.m_repo.GetAll<Db.T_projects>();

            //return this.m_repo.GetAll<Db.T_projects>(x => x.name, y=> y.updated_on );
            
            return this.m_repo.GetAsSelectList<Db.T_trackers>(x => x.id.ToString(), y => y.name);
        }
        
        
        [Route("/project")]
        public IActionResult ProjectsGeneric()
        {
            // Generic tree 
            Models.Project.ProjectModelFactory fac = new ProjectModelFactory(this.m_repo);
            Models.Project.ProjectModel pm = fac.Create(null);

            return View("GenericIndex", pm);
        } // End Action Index 


        [Route("projects")]
        public IActionResult Index()
        {
            // Non-generic tree 
            Models.Project.ProjectModelFactory fac = new ProjectModelFactory(this.m_repo);
            Models.Project.ProjectModel pm = fac.Create(null);

            return View(pm);
        } // End Action Index 


        [Route("projects/{uri}")]
        public IActionResult SpecificProject(string uri)
        {
            return this.Content($"<html><body><h1>Project {uri}</h1></body></html>", "text/html");
        }


        [Route("projects/{uri}/issues")]
        public IActionResult SpecificIssues(string uri)
        {
            return this.Content($"<html><body><h1>Issues for project {uri}</h1></body></html>", "text/html");
        }


        [Route("projects/{uri}/issues/{id:int}")]
        public IActionResult IssueForProject(string uri, int id)
        {
            return this.Content($"<html><body><h1>Issue {id} for project {uri}</h1></body></html>", "text/html");
        }


        [Route("projects/{uri}/issues/new")]
        public IActionResult NewIssueForProject(string uri)
        {
            // return this.Content($"<html><body><h1>New issue for project {uri}</h1></body></html>", "text/html");
            return View("NewItem");
        }


        // http://localhost:55337/projects/abc/issues/new
        // http://localhost:55337/projects/abc/issues/new1
        [Route("projects/{uri}/issues/new1")]
        public IActionResult NewIssue1ForProject(string uri)
        {
            // https://docs.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/intro
            // https://docs.microsoft.com/en-us/aspnet/core/mvc/views/working-with-forms
            // @addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
            // @addTagHelper *, AuthoringTagHelpers

            // @model List<SelectListItem>
            // @model BlueMine.Models.Issue.IssueModel


            Models.Issue.IssueModel im = Models.Issue.IssueModel.FromFactory(this.m_repo, null);

            // return this.Content($"<html><body><h1>New issue for project {uri}</h1></body></html>", "text/html");
            return View("NewItem1", im);
        }


        [Route("projects/{uri}/issues/gantt")]
        public IActionResult GanttForProject(string uri)
        {
            return this.Content($"<html><body><h1>Gantt-Chart for issues in project {uri}</h1></body></html>",
                "text/html");
        }


        [Route("projects/{uri}/issues/calendar")]
        public IActionResult CalendarForProject(string uri)
        {
            return this.Content($"<html><body><h1>Calendar for issues in project {uri}</h1></body></html>",
                "text/html");
        }


        [Route("projects/{uri}/activity")]
        public IActionResult IssuesActivityForProject(string uri)
        {
            return this.Content($"<html><body><h1>Activity for project {uri}</h1></body></html>", "text/html");
        }


        [Route("projects/{uri}/files")]
        public IActionResult FilesForProject(string uri)
        {
            return this.Content($"<html><body><h1>Files for project {uri}</h1></body></html>", "text/html");
        }


        [Route("projects/{uri}/settings")]
        public IActionResult SettingsForProject(string uri)
        {
            return this.Content($"<html><body><h1>Settings for project {uri}</h1></body></html>", "text/html");
        }


        [Route("projects/{uri}/news")]
        public IActionResult NewsForProject(string uri)
        {
            return this.Content($"<html><body><h1>News for project {uri}</h1></body></html>", "text/html");
        }


        [Route("projects/{uri}/wiki")]
        public IActionResult WikiForProject(string uri)
        {
            return this.Content($"<html><body><h1>Wiki for project {uri}</h1></body></html>", "text/html");
        }


        [Route("projects/{uri}/boards")]
        public IActionResult BoardsForProject(string uri)
        {
            return this.Content($"<html><body><h1>Boards for project {uri}</h1></body></html>", "text/html");
        }


        [Route("projects/{uri}/boards/{id:int}")]
        public IActionResult SpecificBoardForProject(string uri, int id)
        {
            return this.Content($"<html><body><h1>Board {id} for project {uri}</h1></body></html>", "text/html");
        }


        [Route("projects/{uri}/documents")]
        public IActionResult DocumentsForProject(string uri)
        {
            return this.Content($"<html><body><h1>Documents for project {uri}</h1></body></html>", "text/html");
        }
        
        
    } // End Class ProjectController 
    
    
} // End Namespace BlueMine.Controllers 
