
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;


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
        
        private readonly BlueMine.Db.BlueMineContext m_BlueMineContext;
        private readonly BlueMine.Data.CRUD m_Repo;
        private readonly BlueMine.Data.Dapper.CRUD m_DapperRepo;

        private readonly BlueMine.Db.BlueMineRepository m_repi;

        public ProjectController(BlueMine.Db.BlueMineContext dbContext
            , BlueMine.Db.BlueMineRepository repi
            )
        {
            this.m_BlueMineContext = dbContext;
            this.m_Repo = new BlueMine.Data.CRUD(dbContext);
            this.m_DapperRepo = new BlueMine.Data.Dapper.CRUD();
            this.m_repi = repi;
        }


        [Route("/TestRepo")]
        public System.Collections.Generic.List<SelectListItem> LuL()
        {
            var se1 = BlueMine.Data.SortExpression<Db.T_projects>.Create(x => x.name);
            var se2 = BlueMine.Data.SortExpression<Db.T_projects>.Create(x => x.updated_on, Data.SortDirection.Descending);

            // return this.m_repi.GetFilteredSorted<Db.T_projects>(null, se1, se2);
            // return this.m_repi.GetFilteredSorted<Db.T_projects>(null, x => x.name);

            // return this.m_repi.GetFilteredSorted<Db.T_projects>(x => x.parent_id == 6, x => x.name);
            // return this.m_repi.GetAll<Db.T_projects>();

            //return this.m_repi.GetAll<Db.T_projects>(x => x.name, y=> y.updated_on );

            return this.m_repi.GetAsSelectList<Db.T_trackers>(x => x.id.ToString(), y => y.name);
        }


        [Route("/project_dapper_Generic")]
        public IActionResult GenericProjectsWithDapper()
        {
            System.Collections.Generic.List<Db.T_projects> projects = 
                this.m_DapperRepo.GetProjects();


            // var x = (from proj in projects where proj.parent_id != null select proj);
            // var y = projects.Where(u => u.parent_id != null).OrderBy(z => z.id).ToList();

            // @Html.DisplayFor((from prop in Model where prop.parent_id != null select prop), "T_projects")

            // @Html.DisplayForModel()
            // @Html.EditorForModel()


            // return View(projects);

            Models.Project.ProjectModel pm = 
                new Models.Project.ProjectModel()
            {
                GenericTree = new BlueMine.Data.GenericRecursor<Db.T_projects, long?>(
                      projects
                    , x => x.parent_id
                    , x => x.id)
            };

            // pm.GenericTree.AddSort(SortTerm<Db.T_projects>.Create(x => x.id));
            // pm.GenericTree.AddSort(SortTerm<Db.T_projects>.Create(x => x.id, SortDirection.Descending));

            //pm.GenericTree.AddSort(
            //      SortTerm<Db.T_projects>.Create(x => x.name)
            //    , SortTerm<Db.T_projects>.Create(x => x.created_on)
            //    , SortTerm<Db.T_projects>.Create(x => x.id)
            //    , SortTerm<Db.T_projects>.Create(x => x.parent_id)
            //);

            // pm.GenericTree.AddSort(x => x.name, SortDirection.Ascending);
            // pm.GenericTree.AddSort(x => x.created_on, SortDirection.Ascending);
            // pm.GenericTree.AddSort(x => x.id, SortDirection.Ascending);
            // pm.GenericTree.AddSort(x => x.parent_id, SortDirection.Ascending);

            pm.GenericTree.AddSort(x => x.name);

            return View("GenericIndex", pm);
        } // End Action Index 
        

        [Route("/project")]
        public IActionResult ProjectsWithBlueEntityFramwork()
        {
            Models.Project.ProjectModel pm = 
                new Models.Project.ProjectModel()
            {
                GenericTree = this.m_Repo.GetProjectTree("")
            };

            pm.GenericTree.AddSort(x => x.name);

            return View("GenericIndex", pm);
        } // End Action Index 


        // Projects with dapper non-generic
        [Route("projects")]
        public IActionResult Index()
        {
            System.Collections.Generic.List<Db.T_projects> projects =
                this.m_DapperRepo.GetProjects();

            Models.Project.ProjectModel pm = new Models.Project.ProjectModel()
            {
                ProjectTree = new Models.Project.ProjectRecursor(projects)
            };

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


            Models.Issue.IssueModel im = Models.Issue.IssueModel.FromFactory(this.m_repi, null);
            
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
