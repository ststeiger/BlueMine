
using System.Linq;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using BlueMine.Db;


namespace BlueMine.Data
{
    
    
    public class CRUD
    {
        private readonly BlueMineContext _context;
        
        
        public CRUD(BlueMineContext context)
        {
            _context = context;
        }
        
        
        public string TestSqlGeneration(string uri)
        {
            var ls = _context.projects.ToList();

            // https://docs.microsoft.com/en-us/ef/core/querying/raw-sql
            // _context.projects.FromSql("select * from projects");


            var lol = (
                from projects in _context.projects
                from issues in _context.issues
                    .Where(issue => issue.project_id == projects.id)
                select issues
            ).AsNoTracking().ToList();

            /*
            var customer = new issues()
            {
                Subject = "",
                Description = "",
                DueDate = System.DateTime.Today
            };
            // _context.issues.Add(customer);
            _context.SaveChanges();
            */

            var lol2 = (
                from project in _context.projects
                join issue in _context.issues on project.id equals issue.project_id into projectIssueJoin
                from projectIssue in projectIssueJoin.DefaultIfEmpty()
                join tracker in _context.trackers on projectIssue.tracker_id equals tracker.id into
                    projectIssueTrackerJoin
                from projectIssueTracker in projectIssueTrackerJoin.DefaultIfEmpty()
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

            return $"<html><body><span>{sql}</span></body></html>";
        }
        
        
        public BlueMine.Data.GenericRecursor<BlueMine.Db.T_projects, long?> GetProjectTree(string uri)
        {
            var lsProjects = GetProjects(uri);
            return new BlueMine.Data.GenericRecursor<BlueMine.Db.T_projects, long?>(
                    lsProjects
                  , x => x.parent_id
                  , x => x.id);
        }
        

        public List<BlueMine.Db.T_projects> GetProjects(string uri)
        {
            List<BlueMine.Db.T_projects> lsProjects = (
                from project in _context.projects
                // orderby project.name ascending
                select project
            ).AsNoTracking()
            .ToList()
            .OrderBy(y => y.name).ToList() // Order in .NET 
            ;
            
            return lsProjects;
        }



        public List<SelectListItem> GetStati(string uri)
        {
            List<SelectListItem> lsStati = (
                from stati in _context.issue_statuses
                orderby stati.position ascending

                select new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                     Value = stati.id.ToString()
                    ,Text = stati.name
                    //,Selected = stati.is_default
                }

            ).AsNoTracking()
            .ToList()
            //.OrderBy(y => y.Text).ToList() // Order in .NET 
            ;
            
            return lsStati;
        }
        
        
        public List<SelectListItem> GetPriorities(string uri)
        {
            List<SelectListItem> lsPriorities = (
                from enumz in _context.enumerations
                where !enumz.project_id.HasValue 
                && enumz.type == "IssuePriority"
                orderby enumz.position ascending
                
                
                select new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                     Value = enumz.id.ToString()
                    ,Text = enumz.name
                    ,Selected = enumz.is_default
                }

            ).AsNoTracking()
            .ToList()
            //.OrderBy(y => y.Text).ToList() // Order in .NET 
            ;

            return lsPriorities;
        }
        
        
        public List<SelectListItem> GetTrackers(string uri)
        {
            var trackerz = (
                from tracker in _context.trackers
                orderby tracker.position ascending
                select new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = tracker.id.ToString(),
                    Text = tracker.name
                    // ,Selected = tracker.default_status_id
                }
            ).AsNoTracking()
            .ToList()
            //.OrderBy(y => y.Text).ToList() // Order in .NET 
            ;

            //trackerz.Sort(
            //    delegate(SelectListItem x, SelectListItem y){

            //        if (x == null && y == null)
            //            return 0;

            //        if (x == null || y == null)
            //        {
            //            // return (int)direction * (x == null ? -1 : 1); // NULL-Values at top when ASC, bottom when DESC
            //            // return (int)direction * (x == null ? 1 : -1); // NULL-Values at bottom when ASC, top when DESC

            //            // return (x == null ? -1 : 1); // NULL-Values at top, indep. of search dir
            //            return (x == null ? 1 : -1); // NULL-Values at bottom, indep. of search dir
            //        }

            //        if (x.Text == null && y.Text == null)
            //            return 0;

            //        if (x.Text == null || y.Text == null)
            //        {
            //            // return (int)direction * (x.Text == null ? -1 : 1); // NULL-Values at top when ASC, bottom when DESC
            //            // return (int)direction * (x.Text == null ? 1 : -1); // NULL-Values at bottom when ASC, top when DESC

            //            // return (x.Text == null ? -1 : 1); // NULL-Values at top, indep. of search dir
            //            return (x.Text == null ? 1 : -1); // NULL-Values at bottom, indep. of search dir
            //        }

            //        return x.Text.CompareTo(y.Text);
            //    }
            //);
            
            
            /*
            List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> regions =
                new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>()
                {
                    new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                    {
                        Value = null,
                        Text = " ",
                        Disabled = false,
                        Selected = false,
                        Group = new SelectListGroup()
                        {
                            Name = "foo",
                            Disabled = false
                        }
                    }
                };
            */
            
            return trackerz;
        }
        
        
    }
    
    
}
