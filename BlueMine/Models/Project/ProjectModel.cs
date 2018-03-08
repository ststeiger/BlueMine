
using System.Linq;
using BlueMine.Db;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BlueMine.Models.Project
{


    public class ProjectModel
    {
        public ProjectRecursor ProjectTree;
        public BlueMine.Data.GenericRecursor<Db.T_projects, long?> GenericTree;
        
        
        public static ProjectModel FromFactory(BlueMineRepository repository, int? projectId)
        {
            ProjectModelFactory pmf = new ProjectModelFactory(repository);
            return pmf.Create(projectId);
        }
        
        
    }
    
    
}
