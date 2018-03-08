
namespace BlueMine.Services
{


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

            ser.Serialize(tw, obj);
            ser = null;
        }


    }


}
