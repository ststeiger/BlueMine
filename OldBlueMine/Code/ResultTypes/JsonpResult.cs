
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;


namespace BlueMine
{


    public class JsonpResult
        : Microsoft.AspNetCore.Mvc.ActionResult
    {
        protected object m_data;

        public object Data
        {
            get { return m_data; }
            set { m_data = value; }
        }

        protected bool m_prettyPrint;

        public bool PrettyPrint
        {
            get { return m_prettyPrint; }
            set { m_prettyPrint = value; }
        }


        public JsonpResult()
            : this(null)
        { }


        public JsonpResult(object data)
            :this(data, true)
        { }

        public JsonpResult(object data, bool prettyPrint)
        {
            this.m_data = data;
            this.m_prettyPrint = prettyPrint;
        }


        public override void ExecuteResult(Microsoft.AspNetCore.Mvc.ActionContext context)
        {
            Microsoft.AspNetCore.Http.HttpResponse response = context.HttpContext.Response;

            //if (this.m_data == null)
            //{
            //    response.StatusCode = StatusCodes.Status500InternalServerError;
            //    return;
            //}
            
            response.StatusCode = StatusCodes.Status200OK;
            response.ContentType = "application/javascript; charset=utf-8";

            Services.JsonSerializer ser = (Services.JsonSerializer)context.HttpContext
                .RequestServices.GetService(typeof(Services.JsonSerializer));
            ser.SerializeJsonp(context.HttpContext.Response.Body, "callback"
                , this.m_data, this.m_prettyPrint);
        }


        public async override Task ExecuteResultAsync(
        Microsoft.AspNetCore.Mvc.ActionContext context)
        {
            ExecuteResult(context);
            await Task.CompletedTask;
        } // End Sub ExecuteResultAsync 


    }


}
