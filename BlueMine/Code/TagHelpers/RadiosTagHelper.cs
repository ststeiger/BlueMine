
namespace BlueMine.TagHelpers
{


    [Microsoft.AspNetCore.Razor.TagHelpers.HtmlTargetElement("radios")]
    public class RadiosTagHelper 
        : Microsoft.AspNetCore.Razor.TagHelpers.TagHelper
    {

        // https://www.jerriepelser.com/blog/accessing-request-object-inside-tag-helper-aspnet-core/
        [Microsoft.AspNetCore.Mvc.ViewFeatures.ViewContext]
        public Microsoft.AspNetCore.Mvc.Rendering.ViewContext ViewContext { get; set; }

        public Microsoft.AspNetCore.Mvc.Infrastructure.IActionContextAccessor ActionContextAccessor;



        [Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeName("asp-for")]
        public Microsoft.AspNetCore.Mvc.ViewFeatures.ModelExpression AspFor { get; set; }

        // [Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeName("asp-items")]
        // public Microsoft.AspNetCore.Mvc.ViewFeatures.ModelExpression AspItems { get; set; }
        
        [Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeName("asp-items")]
        public System.Collections.Generic.IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Items { get; set; }
        
        [Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeName("id")]
        public string Id { get; set; }
        
        [Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeName("name")]
        public string Name { get; set; }
        
        // [Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeName("data-lol")]
        // public string Fsck { get; set; }
        
        
        
        public RadiosTagHelper(
            Microsoft.AspNetCore.Mvc.Infrastructure.IActionContextAccessor actionContextAccessor
        )
        {
            this.ActionContextAccessor = actionContextAccessor;
            // this.ActionContextAccessor.ActionContext.ModelState;
            // ViewContext.HttpContext
            // ViewContext.ModelState
        }

        // https://tahirnaushad.com/2017/08/27/asp-net-core-2-0-mvc-custom-tag-helpers/
        public override void Process(
            Microsoft.AspNetCore.Razor.TagHelpers.TagHelperContext context,
            Microsoft.AspNetCore.Razor.TagHelpers.TagHelperOutput output)
        {
            // context.AllAttributes["maxlength"];

            // System.Console.WriteLine(this.ActionContextAccessor.ActionContext.ModelState);
            // System.Console.WriteLine(this.ViewContext.ModelState);
            // System.Console.WriteLine(AspFor, AspItems);


            //foreach (string k in this.ViewContext.ModelState.Keys)
            //{
            //    System.Console.WriteLine(k);
            //}


            // output.TagName = "details";
            // output.TagMode = TagMode.StartTagAndEndTag;


            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //sb.AppendFormat("<summary>{0}</summary>", this.AspItems);
            //sb.AppendFormat("<em>{0}</em>", this.AspFor);
            //sb.AppendFormat("<p>{0}</p>", this.AspItems);
            //sb.AppendFormat("<ul>");

            //output.PreContent.SetHtmlContent(sb.ToString());

            //output.PostContent.SetHtmlContent("</ul>");

            output.TagName = null;
            // output.TagName = "pre";
            output.TagMode = Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag;

            // output.PostContent.SetContent("<h1>this gets HTML encoded<h1>");
            // output.PostContent.SetHtmlContent("<h1>Hello World</h1>");


            System.Text.StringBuilder sb = new System.Text.StringBuilder();


            //System.Collections.Generic.List<
            //    Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            //    > items = (
            //    System.Collections.Generic.List<
            //    Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            //    >)AspItems.Model;
            
            
            string id = this.Id;
            string name = this.Name ?? id;
            
            string checkedValue = (string) 
                System.Convert.ChangeType(AspFor.Model, typeof(string), System.Globalization.CultureInfo.InvariantCulture);
            
            int i = 0;
            //foreach (Microsoft.AspNetCore.Mvc.Rendering.SelectListItem item in items)
            foreach (Microsoft.AspNetCore.Mvc.Rendering.SelectListItem item in Items)
            {
                i++;
                sb.AppendLine("<label>");

                sb.Append("<input type=\"radio\"");
                //sb.Append(" id=\"issue_custom_field_values_");
                sb.Append(" id=\"");
                sb.Append(System.Web.HttpUtility.HtmlAttributeEncode(id));
                sb.Append("_");
                sb.Append(i);
                sb.Append("\"");
                
                //sb.Append(" name =\"issue[custom_field_values][3]\"");
                sb.Append(" name=\"");
                sb.Append(System.Web.HttpUtility.HtmlAttributeEncode(name));
                sb.Append("\"");
                
                sb.Append(" value=\"");
                sb.Append(System.Web.HttpUtility.HtmlAttributeEncode(item.Value));
                sb.Append("\"");
                if (string.Equals(checkedValue, item.Value, System.StringComparison.InvariantCultureIgnoreCase))
                    sb.Append(" checked=\"checked\" ");
                sb.Append(">");

                sb.AppendLine(System.Web.HttpUtility.HtmlEncode(item.Text));
                sb.AppendLine("</label>");
            } // Next item 

            output.PostContent.SetHtmlContent(sb.ToString());
            //output.PostContent.SetContent("");
            //output.PreElement.SetHtmlContent("<h3>Hello</h3>");
        } // End Sub Process 


    } // End Class RadiosTagHelper


}
