using System.Linq;
using BlueMine.Db;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;


namespace BlueMine.Models.Project
{
    public class ProjectModelFactory
    {
        protected BlueMineRepository m_repo;
        private readonly BlueMine.Data.Dapper.CRUD m_DapperRepo;

        public ProjectModelFactory(BlueMineRepository repository)
        {
            this.m_repo = repository;
            this.m_DapperRepo = new BlueMine.Data.Dapper.CRUD();
        }


        public ProjectModel Create(int? projectId)
        {
            ProjectModel pm = new ProjectModel();
            // GetRecursorWithDapper(pm);

            List<T_projects> lsProjects = this.m_repo.GetAll<T_projects>(x => x.name);

            pm.GenericTree = new BlueMine.Data.GenericRecursor<BlueMine.Db.T_projects, long?>(
                lsProjects
                , x => x.parent_id
                , x => x.id
            );

            pm.GenericTree.AddSort(x => x.name);

            // GetProjectTreeWithDapper(pm);
            pm.ProjectTree = new Models.Project.ProjectRecursor(lsProjects);

            return pm;
        }


        public static void GetRecursorWithDapper(Models.Project.ProjectModel pm)
        {
            var dapperRepo = new BlueMine.Data.Dapper.CRUD();

            System.Collections.Generic.List<Db.T_projects> projects = dapperRepo.GetProjects();

            pm.GenericTree = new BlueMine.Data.GenericRecursor<Db.T_projects, long?>(
                projects
                , x => x.parent_id
                , x => x.id
            );

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


            // @Html.DisplayFor((from prop in Model where prop.parent_id != null select prop), "T_projects")

            // @Html.DisplayForModel()
            // @Html.EditorForModel()
        }


        public static void GetProjectTreeWithDapper(Models.Project.ProjectModel pm)
        {
            var dapperRepo = new BlueMine.Data.Dapper.CRUD();

            System.Collections.Generic.List<Db.T_projects> projects =
                dapperRepo.GetProjects();

            pm.ProjectTree = new Models.Project.ProjectRecursor(projects);
        }

        public static void GetProjectInfo()
        {
            string sql = @"
SELECT projects.* FROM projects 
WHERE projects.identifier = @0  
ORDER BY projects.id ASC 
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
SELECT enabled_modules.name 
FROM enabled_modules 
WHERE enabled_modules.project_id = @0; 
";
            System.Console.WriteLine(sql);

            //, N'@0 int', @0 = 153   
        }
    }
}