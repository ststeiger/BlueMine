
using System.Linq;
using BlueMine.Db;


namespace BlueMine.Models.Project
{
    
    
    public class IssueModel
    {

        protected BlueMineContext m_contex;

        public IssueModel()
            :this(null, null)
        { }
        
        
        public IssueModel(BlueMineContext contex)
            :this(contex, null)
        { }
        
        
        public IssueModel(BlueMineContext contex, int? issueId)
        {
            this.m_contex = contex;

            if (issueId.HasValue)
            {
                var y = (
                    from user in m_contex.users
                    where user.status == 1
                          && user.type == "User"
                    orderby user.firstname, user.last_login_on
                    select new
                    {
                        Id = user.id, 
                        Text = user.firstname + " " + user.lastname
                    }
                ).Take(1);
            }

            // this.issue.estimated_hours
        }
        
        
        public System.Collections.Generic.List<
            Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
        > Trackers;
        
        public System.Collections.Generic.List<
            Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
        > Stati;
        
        public System.Collections.Generic.List<
            Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
        > Priorities;
        
        public System.Collections.Generic.List<
            Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
        > Users;
        
    }


    public class ProjectModel
    {
        public ProjectRecursor ProjectTree;
        public BlueMine.Data.GenericRecursor<Db.T_projects, long?> GenericTree;


        public static void GetProjectInfo()
        {
            string sql = @"
SELECT  [projects].* FROM [projects] 
WHERE [projects].[identifier] = @0  
ORDER BY [projects].[id] ASC 
OFFSET 0 ROWS FETCH NEXT 1 ROWS ONLY
"; //, N'@0 nvarchar(4000)', @0 = N'v4vwsff'

            System.Console.WriteLine(sql);
        }


        public static void GetBoards(int project_id)
        {
            System.Type.GetTypeFromProgID("");
            System.Activator.CreateInstance(null);
            string sql = @"
SELECT * FROM boards WHERE project_id = 64
";

            System.Console.WriteLine(sql);
        }


        public static void GetEnabledModules(int project_id)
        {
            string sql = @"
SELECT ""enabled_modules"".""name"" 
FROM ""enabled_modules"" 
WHERE ""enabled_modules"".""project_id"" = @0; 
";
            System.Console.WriteLine(sql);

            //, N'@0 int', @0 = 153   
        }
    }
}