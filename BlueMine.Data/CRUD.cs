
using System.Linq;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using BlueMine.Redmine;



namespace BlueMine.Data
{


    public class CRUD
    {
        private readonly RedmineContext _context;


        public CRUD(RedmineContext context)
        {
            _context = context;
        }




        [Route("lols")]
        public string lolz(string uri)
        {
            var ls = _context.projects.ToList();

            // https://docs.microsoft.com/en-us/ef/core/querying/raw-sql
            // _context.projects.FromSql("select * from projects");


            var lol = (
                from projects in _context.projects
                from issues in _context.issues
                    .Where(issue => issue.ProjectId == projects.Id)
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
                join issue in _context.issues on project.Id equals issue.ProjectId into projectIssueJoin
                from projectIssue in projectIssueJoin.DefaultIfEmpty()
                join tracker in _context.trackers on projectIssue.TrackerId equals tracker.Id into
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

            return $"<html><body><h1>lol {ls.Count}</h1></body></html>";
        }



        public BlueMine.Data.GenericRecursor<BlueMine.Redmine.projects, long?> GetProjectTree(string uri)
        {
            var lsProjects = GetProjects(uri);
            return new BlueMine.Data.GenericRecursor<BlueMine.Redmine.projects, long?>(
                    lsProjects
                  , x => x.ParentId
                  , x => x.Id);
        }
        

        public List<BlueMine.Redmine.projects> GetProjects(string uri)
        {
            List<BlueMine.Redmine.projects> lsProjects = (
                from project in _context.projects
                orderby project.Name ascending
                select project
            ).AsNoTracking()
            .ToList()
            //.OrderBy(y => y.Text) // Order in .NET 
            .ToList()
            ;

            return lsProjects;
        }



        public List<SelectListItem> GetStati(string uri)
        {
            List<SelectListItem> lsStati = (
                from stati in _context.issue_statuses
                orderby stati.Position ascending

                select new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                     Value = stati.Id.ToString()
                    ,Text = stati.Name
                    //,Selected = stati.is_default
                }

            ).AsNoTracking()
            .ToList()
            //.OrderBy(y => y.Text) // Order in .NET 
            .ToList()
            ;

            return lsStati;
        }



        // [Route("ddt")]
        public List<SelectListItem> GetPriorities(string uri)
        {
            List<SelectListItem> lsPriorities = (
                from enumz in _context.enumerations
                where !enumz.ProjectId.HasValue
                orderby enumz.Position ascending

                select new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = enumz.Id.ToString()
                    ,
                    Text = enumz.Name
                    ,
                    Selected = enumz.IsDefault
                }

            ).AsNoTracking()
            .ToList()
            //.OrderBy(y => y.Text) // Order in .NET 
            .ToList()
            ;

            return lsPriorities;
        }


        // [Route("dds")]
        public List<SelectListItem> GetTrackers(string uri)
        {
            var trackerz = (
                from tracker in _context.trackers
                orderby tracker.Position ascending
                select new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = tracker.Id.ToString(),
                    Text = tracker.Name
                    //,Selected = tracker.DefaultStatusId
                }
            ).AsNoTracking()
            .ToList()
            .OrderBy(y => y.Text) // Order in .NET 
            .ToList()
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
