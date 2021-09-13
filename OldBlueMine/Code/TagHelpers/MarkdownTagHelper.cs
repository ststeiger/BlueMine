
using System.Threading.Tasks;


namespace BlueMine.Code.TagHelpers
{


    [Microsoft.AspNetCore.Razor.TagHelpers.HtmlTargetElement("markdown")]
    public class MarkdownTagHelper : Microsoft.AspNetCore.Razor.TagHelpers.TagHelper
    {
        /*
        // https://stackoverflow.com/questions/40850154/how-to-get-elements-defined-as-taghelper-content-in-taghelper-process
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string c = output.Content.GetContent();
            // c is empty; how to get content "bla bla"?
        }
        */


        public override async Task ProcessAsync(
            Microsoft.AspNetCore.Razor.TagHelpers.TagHelperContext context,
            Microsoft.AspNetCore.Razor.TagHelpers.TagHelperOutput output)
        {
            string c = (await output.GetChildContentAsync()).GetContent();
            // transform markdown in c
        }


    } // End Class MarkdownTagHelper  


}
