
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;


namespace BlueMine
{


    public class TextResult
        : Microsoft.AspNetCore.Mvc.ActionResult
    {
        protected string m_text;

        public string Text
        {
            get { return m_text; }
            set { m_text = value; }
        }

        protected string m_MimeType;

        public string MimeType
        {
            get { return m_MimeType; }
            set { m_MimeType = value; }
        }


        protected System.Text.Encoding m_enc;

        public System.Text.Encoding Encoding
        {
            get { return m_enc; }
            set { m_enc = value; }
        }


        public TextResult()
            : this(null)
        { }


        public TextResult(string text)
            : this(text, "text/plain", System.Text.Encoding.UTF8)
        { }

        public TextResult(string text, string mimeType)
            : this(text, mimeType, System.Text.Encoding.UTF8)
        { }

        public TextResult(string text, string mimeType, System.Text.Encoding enc)
        {
            this.m_text = text;
            this.m_MimeType = mimeType;
            this.m_enc = enc;
        }


        public override void ExecuteResult(Microsoft.AspNetCore.Mvc.ActionContext context)
        {
            Microsoft.AspNetCore.Http.HttpResponse response = context.HttpContext.Response;

            //if (this.m_data == null)
            //{
            //    response.StatusCode = StatusCodes.Status500InternalServerError;
            //    return;
            //}

            byte[] data = this.m_enc.GetBytes(this.m_text);

            response.StatusCode = StatusCodes.Status200OK;
            response.ContentLength = data.Length;
            response.ContentType = this.m_MimeType + "; charset=" + this.Encoding.WebName;
            response.Body.Write(data, 0, data.Length);
        }


        public async override Task ExecuteResultAsync(
        Microsoft.AspNetCore.Mvc.ActionContext context)
        {
            ExecuteResult(context);
            await Task.CompletedTask;
        } // End Sub ExecuteResultAsync 


    }


}
