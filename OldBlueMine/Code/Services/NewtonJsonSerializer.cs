
namespace BlueMine.Services
{


    
    /// <summary>
    /// Converts property names to lower camel case.
    /// </summary>
    internal class LowerCamelCaseContractResolver 
        : Newtonsoft.Json.Serialization.DefaultContractResolver
    {
        /// <summary>
        /// Resolves the name of the property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>
        /// Name of the property.
        /// </returns>
        protected override string ResolvePropertyName(string propertyName)
        {
            var propName = propertyName.ToCharArray();
            var firstChar = char.ToLower(propName[0]);

            return firstChar + propertyName.Substring(1);
        } // End Function ResolvePropertyName 
        
        
    } // End Class LowerCamelCaseContractResolver 

    
    
    
    
    /// <summary>
    /// Converts property names to lower camel case.
    /// </summary>
    internal class UpperCamelCaseContractResolver 
        : Newtonsoft.Json.Serialization.DefaultContractResolver
    {
        /// <summary>
        /// Resolves the name of the property.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>
        /// Name of the property.
        /// </returns>
        protected override string ResolvePropertyName(string propertyName)
        {
            var propName = propertyName.ToCharArray();
            var firstChar = char.ToUpper(propName[0]);
            
            return firstChar + propertyName.Substring(1);
        } // End Function ResolvePropertyName 
        
        
    } // End Class UpperCamelCaseContractResolver 

    
    
    
    public class SnakeCaseContractResolver 
        : Newtonsoft.Json.Serialization.DefaultContractResolver
    {
        
        
        protected override string ResolvePropertyName(string propertyName)
        {
            return GetSnakeCase(propertyName);
        } // End Function ResolvePropertyName 
        
        
        [System.Runtime.CompilerServices.MethodImpl(
            System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)
        ]
        private string GetSnakeCase(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            string buffer = "";

            for (int i = 0; i < input.Length; i++)
            {
                bool isLast = (i == input.Length - 1);
                bool isSecondFromLast = (i == input.Length - 2);

                char curr = input[i];
                char next = !isLast ? input[i + 1] : '\0';
                char afterNext = !isSecondFromLast && !isLast ? input[i + 2] : '\0';

                buffer += char.ToLower(curr);

                if (!char.IsDigit(curr) && curr != '_' && char.IsUpper(next))
                {
                    if (char.IsUpper(curr))
                    {
                        if (!isLast && !isSecondFromLast && !char.IsUpper(afterNext))
                            buffer += "_";
                    }
                    else
                        buffer += "_";
                } // End if (!char.IsDigit(curr) && curr != '_' && char.IsUpper(next))
                
                if (!char.IsDigit(curr) && char.IsDigit(next))
                    buffer += "_";
                if (char.IsDigit(curr) && !char.IsDigit(next) && !isLast)
                    buffer += "_";
            } // Next i
            
            return buffer;
        } // End Function GetSnakeCase 
        
        
    } // End Class 
    
    
    
    
    
    public class NewtonJsonSerializer
        : JsonSerializer 
    {


        public NewtonJsonSerializer()
            :this(System.Text.Encoding.UTF8, true)
        { }


        public NewtonJsonSerializer(System.Text.Encoding enc, bool prettyPrint)
            :base(enc, prettyPrint)
        { }

        
        public override void Serialize(System.IO.TextWriter tw, object obj)
        {
            Newtonsoft.Json.JsonSerializer ser = new Newtonsoft.Json.JsonSerializer();
            
            // Safari can't handle ISO-parsing ...
            ser.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
            ser.Formatting = this.m_prettyPrint ? Newtonsoft.Json.Formatting.Indented : Newtonsoft.Json.Formatting.None;
            
            // ser.ContractResolver = new UpperCamelCaseContractResolver();
            // ser.ContractResolver = new LowerCamelCaseContractResolver();
            // ser.ContractResolver = new CamelCasePropertyNamesContractResolver(); // prop to lower camel case
            // ser.ContractResolver = new SnakeCaseContractResolver(); // prop_to_snake_case
            
            // DefaultContractResolver contractResolver = new DefaultContractResolver
            // { NamingStrategy = new SnakeCaseNamingStrategy() };
            
            ser.Serialize(tw, obj);
            ser = null;
        } // End Function Serialize 
        
        
    } // End Class NewtonJsonSerializer 
    
    
} // End Namespace BlueMine.Services 
