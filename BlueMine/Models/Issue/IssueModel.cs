
using System;
using System.Linq;
using BlueMine.Db;

// using Microsoft.AspNetCore.Mvc.Rendering;
// using Microsoft.AspNetCore.Mvc.ViewFeatures;
// using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;


namespace BlueMine.Models.Issue
{


    public class IssueModel
    {

        public IssueModel()
        { }


        public string Checked = "Ja";
        public string NotChecked = "Nein";


        // public T_issues Issue { get; set; }
        public T_issues Issue;
        public System.Collections.Generic.List<T_custom_values> CustomValues;


        public System.Collections.Generic.List<
            System.Collections.Generic.List<
                Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            >> CustomFields;
        
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
        > AssignedTo; // OK

        public System.Collections.Generic.List<
            Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
        > PercentComplete;


        public static IssueModel FromFactory(BlueMineRepository repository, int? issueId)
        {
            IssueModelFactory imf = new IssueModelFactory(repository);
            return imf.Create(issueId);
        }


    }


}
