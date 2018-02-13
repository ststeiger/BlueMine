
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

// For more information on enabling MVC for empty projects, 
// visit https://go.microsoft.com/fwlink/?LinkID=397860 

using Dapper;
using BlueMine.Models;
using BlueMine.Redmine;
using Microsoft.AspNetCore.Mvc;


// https://blogs.msdn.microsoft.com/webdev/2013/10/17/attribute-routing-in-asp-net-mvc-5/
// https://stackoverflow.com/questions/1263635/route-all-controller-actions-to-a-single-method-then-display-a-view-according-t
// http://httpjunkie.com/2014/430/custom-routing-in-mvc5-with-attributes/
namespace BlueMine.Controllers
{


    // template: "{controller=Home}/{action=Index}/{id?}");
    //[Route("[controller]/[action]/{id?}")]
    //[Route("[controller]/{a?}/{id?}")]
    //[Route("[controller]/{*uri}")]

    public class ProjectController : Controller
    {

        private readonly RedmineContext _context;
        
        public ProjectController(RedmineContext context)
        {
            _context = context;
        }
        
        
        
        
        // GET: /<controller>/
        [Route("projects")]
        public IActionResult Index()
        {
            System.Collections.Generic.List<Db.T_projects> projects = null;
            
            string sql = "SELECT * FROM projects ";
            // string sql = "SELECT * FROM projects WHERE id = @projid";
            
            using (System.Data.Common.DbConnection connection = SqlFactory.GetConnection())
            {
                projects = connection.Query<Db.T_projects>(sql).ToList();
            }
            
            
            // var x = (from proj in projects where proj.parent_id != null select proj);
            
            // var y = projects.Where(u => u.parent_id != null).OrderBy(z => z.id).ToList();
            // @Html.DisplayFor((from prop in Model where prop.parent_id != null select prop), "T_projects")
            
            
            // @Html.DisplayForModel()
            // @Html.EditorForModel()
            
            
            // return View(projects);
            
            return View(
                new Models.Project.ProjectModel()
                {
                    ProjectTree = new Models.Project.ProjectRecursor(null, projects)
                }
            );
        } // End Action Index 
        
        
        
        [Route("lols")]
        public IActionResult lolz(string uri)
        {
            var ls = _context.projects.ToList();

            var lol = (
                from projects in _context.projects
                from issues in _context.issues
                    .Where(issue => issue.ProjectId == projects.Id)
                select issues
            ).ToList();
            
            
            
            var lol2 = (
                from project in _context.projects
                join issue in _context.issues on project.Id equals issue.ProjectId into projectIssueJoin
                
                from projectIssue in projectIssueJoin.DefaultIfEmpty()
                join tracker in _context.trackers on projectIssue.TrackerId equals tracker.Id into projectIssueTrackerJoin 
                from projectIssueTracker in projectIssueTrackerJoin .DefaultIfEmpty()
                
                select project
                
                /*from mappings in tmpMapp.DefaultIfEmpty()
                from groups in tmpGroups.DefaultIfEmpty()
                select new
                {
                    UserId = users.BE_ID
                    ,UserName = users.BE_User
                    ,UserGroupId = mappings.BEBG_BG
                    ,GroupName = groups.Name
                }
                */
            );
            ; //.ToList();
            
            // string sql = lol2.ToString();
            // string sql = BlueMine.Models.IQueryableExtensions.ToSql(lol2);
            // string sql = BlueMine.Models.IQueryableExtensions1.ToSql(lol2);
            string sql = lol2.ToSql();
            System.Console.WriteLine(sql);
            
            return this.Content($"<html><body><h1>lol {ls.Count}</h1></body></html>", "text/html");
        }
        
        
        
        
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
            // return this.Content($"<html><body><h1>New issue for project {uri}</h1></body></html>", "text/html");
            return View("NewItem1");
        }


        [Route("projects/{uri}/issues/gantt")]
        public IActionResult GanttForProject(string uri)
        {
            return this.Content($"<html><body><h1>Gantt-Chart for issues in project {uri}</h1></body></html>", "text/html");
        }
        
        [Route("projects/{uri}/issues/calendar")]
        public IActionResult CalendarForProject(string uri)
        {
            return this.Content($"<html><body><h1>Calendar for issues in project {uri}</h1></body></html>", "text/html");
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
        
        
        public void Demo()
        {
            /*
            using (System.Data.Common.DbConnection connection = SqlFactory.GetConnection())
            {
                string sqlInvoices = "SELECT * FROM projects ";
                string sqlInvoice = "SELECT * FROM projects WHERE id = @projid";
                
                System.Collections.Generic.List<Db.T_projects> projects =
                    connection.Query<Db.T_projects>(sqlInvoices).ToList();

                System.Console.WriteLine(projects);

                var invoice = connection.QueryFirstOrDefault<Db.T_projects>(sqlInvoice, new {projid = 1});
                System.Console.WriteLine(invoice);

                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@projid", 2
                    , System.Data.DbType.Int32
                    , System.Data.ParameterDirection.Input);

                System.Collections.Generic.List<Db.T_projects> invoice2 =
                    connection.Query<Db.T_projects>(sqlInvoice, parameter).ToList();
                System.Console.WriteLine(invoice2);


                using (var transaction = connection.BeginTransaction())
                {
                    int affRows = connection.Execute("sql",
                        new {Kind = 1, Code = "Single_Insert_1"},
                        commandType: System.Data.CommandType.StoredProcedure,
                        transaction: transaction);

                    transaction.Commit();
                }

                // dapper will iterate for you
                List<Db.T_projects> projectList = new List<Db.T_projects>();
                string processQuery = "INSERT INTO PROJECT_LOGS VALUES (@A, @B)";
                connection.Execute(processQuery, projectList);


                var affectedRows = connection.Execute(
                    "sp_something"
                    , new {Param1 = "Single_Insert_1"}
                    , commandType: System.Data.CommandType.StoredProcedure
                );
                
                
                
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@Kind", 123
                    , System.Data.DbType.Int32
                    , System.Data.ParameterDirection.Input);

                parameter.Add("@Code", "Many_Insert_0"
                    , System.Data.DbType.String
                    , System.Data.ParameterDirection.Input);

                parameter.Add("@RowCount"
                    , dbType: System.Data.DbType.Int32
                    , direction: System.Data.ParameterDirection.ReturnValue);

                connection.Query<Db.T_projects>(sqlInvoices, parameter).ToList();
                
            }
            */
            
        } // End Sub Demo 
        
        
    } // End Class ProjectController 
    
    
} // End Namespace BlueMine.Controllers 
