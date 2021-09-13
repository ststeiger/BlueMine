﻿
namespace BlueMine.TagHelpers
{
    
    
    // Also, check out radioChecbox.ts
    // <checkboxes id="mytrackers" name="trackerz" asp-for="Issue.tracker_id" asp-items="Model.Trackers"/>
    [Microsoft.AspNetCore.Razor.TagHelpers.HtmlTargetElement("checkboxes")]
    public class CheckBoxTagHelper 
        : Microsoft.AspNetCore.Razor.TagHelpers.TagHelper
    {
        
        [Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeName("asp-for")]
        public Microsoft.AspNetCore.Mvc.ViewFeatures.ModelExpression AspFor { get; set; }

        [Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeName("asp-items")]
        public System.Collections.Generic.IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> Items { get; set; }
        
        [Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeName("id")]
        public string Id { get; set; }
        
        [Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeName("name")]
        public string Name { get; set; }
        
        
        public CheckBoxTagHelper()
        { } // EndConstructor 
        
        
        // https://tahirnaushad.com/2017/08/27/asp-net-core-2-0-mvc-custom-tag-helpers/
        public override void Process(
            Microsoft.AspNetCore.Razor.TagHelpers.TagHelperContext context,
            Microsoft.AspNetCore.Razor.TagHelpers.TagHelperOutput output)
        {
            output.TagName = null;
            // output.TagName = "pre";
            output.TagMode = Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag;
            
            System.Text.StringBuilder sb = new System.Text.StringBuilder();


            string id = this.Id;
            string name = this.Name ?? id;
            string checkedValue = (AspFor == null) ? null : (string)
                System.Convert.ChangeType(AspFor.Model, typeof(string), System.Globalization.CultureInfo.InvariantCulture);


            int i = 0;
            foreach (Microsoft.AspNetCore.Mvc.Rendering.SelectListItem item in Items)
            {
                i++;
                sb.AppendLine("<label>");
                
                sb.Append("<input type=\"checkbox\"");
                
                //sb.Append(" id=\"issue_custom_field_values_");
                sb.Append(" id=\"");
                sb.Append(System.Web.HttpUtility.HtmlAttributeEncode(id));
                sb.Append("_");
                sb.Append(i);
                sb.Append("\"");
                
                
                //sb.Append(" name=\"issue[custom_field_values][3]\"");
                sb.Append(" name=\"");
                sb.Append(System.Web.HttpUtility.HtmlAttributeEncode(name));
                sb.Append("_");
                sb.Append(i);
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
        } // End Sub Process 


    } // End Class CheckBoxTagHelper


}
