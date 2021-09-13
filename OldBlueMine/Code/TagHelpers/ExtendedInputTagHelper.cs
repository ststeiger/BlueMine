
using System.Linq;


namespace BlueMine.TagHelpers
{


    [Microsoft.AspNetCore.Razor.TagHelpers.HtmlTargetElement("lalalalalalalalala")]
    public class ExtendedInputTagHelper : Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper
    {

        public ExtendedInputTagHelper(Microsoft.AspNetCore.Mvc.ViewFeatures.IHtmlGenerator generator)
            : base(generator) { }


        public override void Process(
            Microsoft.AspNetCore.Razor.TagHelpers.TagHelperContext context,
            Microsoft.AspNetCore.Razor.TagHelpers.TagHelperOutput output)
        {
            System.Reflection.PropertyInfo modelAttributesProperty =
                For.Metadata.GetType().GetProperty("Attributes");

            Microsoft.AspNetCore.Mvc.ModelBinding.ModelAttributes modelAttributes = 
                modelAttributesProperty.GetValue(For.Metadata) 
                    as Microsoft.AspNetCore.Mvc.ModelBinding.ModelAttributes
            ;


            object a = System.Linq.Enumerable.FirstOrDefault(modelAttributes.PropertyAttributes
                , m => m.GetType() == typeof(System.ComponentModel.ReadOnlyAttribute)
            );

            if (a is System.ComponentModel.ReadOnlyAttribute readOnlyAttribute
                && readOnlyAttribute.IsReadOnly == true)
            {
                Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute attribute = 
                    new Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("disabled", "disabled");
                output.Attributes.Add(attribute);
            }

            base.Process(context, output);
        } // End Sub Process 


    } // End Class ExtendedInputTagHelper 


}
