
using System.Threading.Tasks;


// https://msdn.microsoft.com/en-us/magazine/mt814808.aspx
// https://channel9.msdn.com/Events/Connect/2017/T125
namespace BlueMine
{


    // https://www.codeproject.com/Articles/1203408/Upload-Download-Files-in-ASP-NET-Core
    // https://www.danylkoweb.com/Blog/dynamically-resize-your-aspnet-mvc-images-I5
    public class ResizeImageResult : Microsoft.AspNetCore.Mvc.ActionResult
    {
        protected System.IO.Stream m_stream;

        public System.IO.Stream ImageStream
        {
            get { return this.m_stream; }
            set { this.m_stream = value; }
        }


        protected SaveFormat m_imageFormat;

        public SaveFormat ImageFormat
        {
            get { return this.m_imageFormat; }
            set { this.m_imageFormat = value; }
        }

        protected System.Drawing.Size m_maxSize;

        public System.Drawing.Size MaxSize
        {
            get { return this.m_maxSize; }
            set { this.m_maxSize = value; }
        }




        protected bool m_dispose;

        public bool Dispose
        {
            get { return this.m_dispose; }
            set { this.m_dispose = value; }
        }


        public ResizeImageResult()
            : this(SaveFormat.Png, null, new System.Drawing.Size(200, 200), true)
        {
        }


        public ResizeImageResult(SaveFormat format, string inputFile, System.Drawing.Size maxSize)
            : this(format, System.IO.File.OpenRead(inputFile), maxSize, true)
        {
            if (inputFile == null)
                throw new System.ArgumentNullException(nameof(inputFile));
        }


        public ResizeImageResult(SaveFormat format, byte[] inputFile, System.Drawing.Size maxSize)
            : this(format, new System.IO.MemoryStream(inputFile), maxSize, true)
        {
            if (inputFile == null)
                throw new System.ArgumentNullException(nameof(inputFile));
        }


        public ResizeImageResult(SaveFormat format, System.IO.Stream imageStream, System.Drawing.Size maxSize)
            : this(format, imageStream, maxSize, true)
        {
            
        }


        public ResizeImageResult(
            SaveFormat format,
            System.IO.Stream imageStream,
            System.Drawing.Size maxSize,
            bool dispose)
        {
            if (imageStream == null)
                throw new System.ArgumentNullException(nameof(imageStream));

            this.m_stream = imageStream;
            this.m_imageFormat = format;
            this.m_maxSize = maxSize;

            // 5'000²  ==> rgb<byte> = 72 MB,   rgb<int> = 286 MB
            // 2'500²  ==> 18 MB,  72 MB 
            // 3'000²  ==> 26 MB, 103 MB
            if (maxSize.Width * maxSize.Height > 9000000) // 3'000²
                throw new System.ArgumentNullException(nameof(maxSize), 
                    "image-resizer anti-DOS-limit hit: width * height cannot exceed 3'000² aka. 26MB/request.");
        } // End Constructor 


        public override void ExecuteResult(Microsoft.AspNetCore.Mvc.ActionContext context)
        {
            if (context == null)
                throw new System.ArgumentNullException(nameof(context));

            ImageHandler imageHandler = (ImageHandler)context.HttpContext.RequestServices
                .GetService(typeof(ImageHandler));
            
            Microsoft.AspNetCore.Http.HttpResponse response = context.HttpContext.Response;

            if (!imageHandler.ImageFormatSupported(this.m_imageFormat))
            {
                response.StatusCode = 500;
                throw new System.NotSupportedException("Image-format is not supported");
            }

            response.ContentType = this.ImageFormat.MimeType();

            // Fsck, this is not the length of the resized image...
            // response.ContentLength = this.m_stream.Length;

            imageHandler.ResizeImage(this.m_stream, response.Body, SaveFormat.Png, this.m_maxSize);

            //byte[] buffer = new byte[4096];
            //int read = 0;
            //while ((read = this.ImageStream.Read(buffer, 0, buffer.Length)) > 0)
            //{
            //    // response.OutputStream.Write(buffer, 0, read);
            //    response.Body.Write(buffer, 0, read);
            //} // Whend 

            // response.End();
            if (this.m_dispose)
                this.m_stream?.Dispose();
        } // End Sub ExecuteResult 


        public async override Task ExecuteResultAsync(
            Microsoft.AspNetCore.Mvc.ActionContext context)
        {
            ExecuteResult(context);

            //byte[] buffer = new byte[4096];
            //int read = 0;
            //while ((read = await this.ImageStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            //{
            //    // response.OutputStream.Write(buffer, 0, read);
            //    await response.Body.WriteAsync(buffer, 0, read);
            //} // Whend 

            await Task.CompletedTask;
        } // End Sub ExecuteResultAsync 


    } // End Class ResizeImageResult 


    public class MimeTypeAttribute : System.Attribute
    {

        public string MimeType;

        public MimeTypeAttribute(string desc)
        {
            this.MimeType = desc;
        }

    }


    public static class EnumerationExtension
    {

        public static TValue GetAttributeValue<TAttribute, TValue>(System.Reflection.MemberInfo mi
            , System.Func<TAttribute, TValue> getValue)
            where TAttribute : System.Attribute
        {
            TAttribute[] attributes = (TAttribute[])(object)mi.GetCustomAttributes(typeof(TAttribute), false);

            if (attributes == null)
                return default(TValue);

            return getValue(attributes[0]);
        }


        public static string MimeType(this System.Enum value)
        {
            System.Reflection.MemberInfo field = value.GetType().GetField(value.ToString());
            // System.Reflection.FieldInfo field = value.GetType().GetField(value.ToString());
            // System.Reflection.PropertyInfo field = null;
            // System.Reflection.MemberInfo field = null;

            // MimeTypeAttribute[] attributes = (MimeTypeAttribute[]) field.GetCustomAttributes(typeof(MimeTypeAttribute), false);
            // if (attributes == null) return null;
            // return attributes[0].MimeType;
            return GetAttributeValue<MimeTypeAttribute, string>(field, x => x.MimeType);
        }
    }

    // Same As Skia-Type
    public enum SaveFormat
    {
        [MimeType("image/unknown")]
        Unknown = 0,
        [MimeType("image/bmp")]
        Bmp = 1,
        [MimeType("image/gif")]
        Gif = 2,
        [MimeType("image/x-icon")]
        Ico = 3,
        [MimeType("image/jpeg")]
        Jpg = 4,
        [MimeType("image/png")]
        Png = 5,
        [MimeType("image/vnd.wap.wbmp")]
        Wbmp = 6,
        [MimeType("image/webp")]
        Webp = 7,
        [MimeType("image/ktx")]
        Ktx = 8
    }


}
