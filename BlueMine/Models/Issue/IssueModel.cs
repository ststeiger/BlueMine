
using System.Linq;
using BlueMine.Db;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BlueMine.Models.Issue
{


    public class IssueModel
    {

        public IssueModel()
        { }


        public System.Collections.Generic.List<
            Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
        > Trackers;

        public System.Collections.Generic.List<
            Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
        > Stati; // OK 

        public System.Collections.Generic.List<
            Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
        > Priorities; // OK

        public System.Collections.Generic.List<
            Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
        > Users; // OK


        public static IssueModel FromFactory(BlueMineRepository repository, int? issueId)
        {
            var iff = new IssueModelFactory(repository);
            return iff.Create(issueId);
        }


    }


}
