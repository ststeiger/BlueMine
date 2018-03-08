
namespace BlueMine.Services
{


    public abstract class JsonSerializer
    {
        protected bool m_prettyPrint;
        protected System.Text.Encoding m_encoding;
        protected Newtonsoft.Json.JsonSerializer m_Serializer;


        public JsonSerializer()
            :this(System.Text.Encoding.UTF8, true)
        { }


        public abstract void Serialize(System.IO.TextWriter tw, object obj);


        public JsonSerializer(System.Text.Encoding enc, bool prettyPrint)
        {
            this.m_encoding = enc;
            this.m_prettyPrint = prettyPrint;
        }


        public string SerializeQuery(System.Data.IDbCommand cmd)
        {
            return "";
        }


        public string SerializeQuery<TEntity>(System.Linq.IQueryable<TEntity> cmd)
            where TEntity : class
        {
            return "";
        }


        public void Serialize(System.IO.TextWriter tw, object obj, bool prettyPrint)
        {
            this.m_prettyPrint = prettyPrint;
            this.Serialize(tw, obj);
        }
        

        public void Serialize(System.IO.Stream stream, object obj)
        {
            using (System.IO.TextWriter sw = new System.IO.StreamWriter(stream, this.m_encoding))
            {
                Serialize(sw, obj);
            }
        }


        public void Serialize(System.IO.Stream stream, object obj, bool prettyPrint)
        {
            this.m_prettyPrint = prettyPrint;
            Serialize(stream, obj);
        }
        

        public void Serialize(System.Text.StringBuilder sb, object obj)
        {
            using (System.IO.TextWriter tw = new System.IO.StringWriter(sb))
            {
                this.Serialize(tw, obj);
            }
        }


        public void Serialize(System.Text.StringBuilder sb, object obj, bool prettyPrint)
        {
            this.m_prettyPrint = prettyPrint;
            this.Serialize(sb, obj);
        }


        public string Serialize(object obj)
        {
            string retValue = null;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            Serialize(sb, obj);

            retValue = sb.ToString();
            sb.Clear();
            sb = null;

            return retValue;
        }


        public string Serialize(object obj, bool prettyPrint)
        {
            this.m_prettyPrint = prettyPrint;

            return Serialize(obj);
        }
        

        public void SerializeJsonp(System.IO.TextWriter tw, string callbackName, object obj)
        {
            string message = System.Web.HttpUtility.JavaScriptStringEncode(
            $"Callback-function named \"{callbackName}\" not defined, or not a function..."
           );
            
            tw.Write("typeof ");
            tw.Write(callbackName);
            tw.Write(" === 'function' ? ");
            tw.Write(callbackName);
            tw.Write("(");

            if (obj == null)
                tw.Write("null");
            else
                Serialize(tw, obj);

            tw.Write(") : alert('");
            tw.Write(message);
            tw.Write("'); \n");
        }


        public void SerializeJsonp(System.IO.TextWriter tw, string callbackName, object obj, bool prettyPrint)
        {
            this.m_prettyPrint = prettyPrint;
            SerializeJsonp(tw, callbackName, obj);
        }
        

        public void SerializeJsonp(System.IO.Stream stream, string callbackName, object obj)
        {
            using (System.IO.TextWriter sw = new System.IO.StreamWriter(stream, this.m_encoding))
            {
                SerializeJsonp(sw, callbackName, obj);
            }
        }


        public void SerializeJsonp(System.IO.Stream stream, string callbackName, object obj, bool prettyPrint)
        {
            this.m_prettyPrint = prettyPrint;
            SerializeJsonp(stream, callbackName, obj);
        }
        

        public string SerializeJsonp(string callbackName, object obj)
        {
            string retValue = null;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            using (System.IO.TextWriter tw = new System.IO.StringWriter(sb))
            {
                SerializeJsonp(tw, callbackName, obj);
            }


            retValue = sb.ToString();
            sb.Clear();
            sb = null;

            return retValue;
        }


        public string SerializeJsonp(string callbackName, object obj, bool prettyPrint)
        {
            this.m_prettyPrint = prettyPrint;
            return SerializeJsonp(callbackName, obj);
        }


    }


}
