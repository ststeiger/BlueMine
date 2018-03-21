﻿
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
        > Users; // OK


        public static IssueModel FromFactory(BlueMineRepository repository, int? issueId)
        {
            IssueModelFactory imf = new IssueModelFactory(repository);
            return imf.Create(issueId);
        }


    }


}
