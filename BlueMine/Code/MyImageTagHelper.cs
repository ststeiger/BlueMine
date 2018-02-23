
// namespace BlueMine.Code

using Microsoft.AspNetCore.Razor.TagHelpers;


namespace AuthoringTagHelpers.TagHelpers
{
    
    
    // [HtmlTargetElement(Attributes = "bold")]
    [HtmlTargetElement(tag : "image")]
    public class MyImageTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string src = output.Attributes["src"].Value as string;
            output.Attributes.SetAttribute("src", src + "?&v=123");            
// output.Attributes.SetAttribute(SrcAttributeName, _fileVersionProvider.AddFileVersionToPath(Src));
            
                
                
            /*
            output.Attributes.RemoveAll("bold");
            output.PreContent.SetHtmlContent("<strong>");
            output.PostContent.SetHtmlContent("</strong>");
            */
        }
    }
    
    
}