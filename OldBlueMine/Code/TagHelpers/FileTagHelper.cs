
namespace BlueMine.Code.TagHelpers
{


    // <file filename = "polyfill.txt" />
    [Microsoft.AspNetCore.Razor.TagHelpers.HtmlTargetElement("file")]
    public class FileTagHelper 
        : Microsoft.AspNetCore.Razor.TagHelpers.TagHelper
    {
        private Microsoft.AspNetCore.Hosting.IHostingEnvironment _env;

        [Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeName("filename")]
        public string Filename { get; set; } = "default.txt";


        public FileTagHelper(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            _env = env;
        }


        public override void Process(
            Microsoft.AspNetCore.Razor.TagHelpers.TagHelperContext context
            , Microsoft.AspNetCore.Razor.TagHelpers.TagHelperOutput output)
        {
            output.TagMode = Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing;
            output.TagName = null;
            // output.TagName = "pre";

            output.Content.SetContent(
                System.IO.File.ReadAllText(
                    System.IO.Path.Combine(_env.WebRootPath, this.Filename)
                )
            );
        }


    }



}
