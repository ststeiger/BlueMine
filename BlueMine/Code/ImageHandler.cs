
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Helpers;
using SixLabors.ImageSharp.Processing;


// https://msdn.microsoft.com/en-us/magazine/mt814808.aspx
// https://channel9.msdn.com/Events/Connect/2017/T125
namespace BlueMine
{


    // https://www.codeproject.com/Articles/1203408/Upload-Download-Files-in-ASP-NET-Core
    // https://www.danylkoweb.com/Blog/dynamically-resize-your-aspnet-mvc-images-I5
    public class ImageResult : Microsoft.AspNetCore.Mvc.ActionResult
    {
        
        protected System.IO.Stream m_stream;
        public System.IO.Stream ImageStream
        {
            get { return this.m_stream; }
            set { this.m_stream = value; }
        }


        protected string m_contentType;
        public string ContentType
        {
            get { return this.m_contentType; }
            set { this.m_contentType = value; }
        }


        protected bool m_dispose;
        public bool Dispose
        {
            get { return this.m_dispose; }
            set { this.m_dispose = value; }
        }


        public ImageResult()
            :this(null, null, true)
        { }


        public ImageResult(string contentType, System.IO.Stream imageStream)
            : this(contentType, imageStream, true)
        { }

        public ImageResult(string contentType, System.IO.Stream imageStream, bool dispose)
        {
            if (imageStream == null)
                throw new ArgumentNullException("imageStream");
            if (contentType == null)
                throw new ArgumentNullException("contentType");

            this.ImageStream = imageStream;
            this.ContentType = contentType;
        } // End Constructor 


        //
        // Zusammenfassung:
        //     Executes the result operation of the action method synchronously. This method
        //     is called by MVC to process the result of an action method.
        //
        // Parameter:
        //   context:
        //     The context in which the result is executed. The context information includes
        //     information about the action that was executed and request information.
        public override void ExecuteResult(Microsoft.AspNetCore.Mvc.ActionContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            Microsoft.AspNetCore.Http.HttpResponse response = context.HttpContext.Response;

            response.ContentType = this.ContentType;
            response.ContentLength = this.m_stream.Length;

            byte[] buffer = new byte[4096];
            int read = 0;
            while ((read = this.ImageStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                // response.OutputStream.Write(buffer, 0, read);
                response.Body.Write(buffer, 0, read);
            } // Whend 

            // response.End();
            if (this.m_dispose && this.m_stream != null)
                this.m_stream.Dispose();
        } // End Sub ExecuteResult 


        //
        // Zusammenfassung:
        //     Executes the result operation of the action method asynchronously. This method
        //     is called by MVC to process the result of an action method. The default implementation
        //     of this method calls the Microsoft.AspNetCore.Mvc.ActionResult.ExecuteResult(Microsoft.AspNetCore.Mvc.ActionContext)
        //     method and returns a completed task.
        //
        // Parameter:
        //   context:
        //     The context in which the result is executed. The context information includes
        //     information about the action that was executed and request information.
        //
        // Rückgabewerte:
        //     A task that represents the asynchronous execute operation.
        public async override Task ExecuteResultAsync(Microsoft.AspNetCore.Mvc.ActionContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            Microsoft.AspNetCore.Http.HttpResponse response = context.HttpContext.Response;

            response.ContentType = this.ContentType;
            response.ContentLength = this.m_stream.Length;

            byte[] buffer = new byte[4096];
            int read = 0;
            while ((read = await this.ImageStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                // response.OutputStream.Write(buffer, 0, read);
                await response.Body.WriteAsync(buffer, 0, read);
            } // Whend 

            // response.End();
            if (this.m_dispose && this.m_stream != null)
                this.m_stream.Dispose();
        } // End Sub ExecuteResultAsync 


    } // End Class ImageResult 


    public class ImageHandler
    {

        private async Task<SixLabors.ImageSharp.Image<SixLabors.ImageSharp.Rgba32>> 
            LoadImageFromUrl(string url)
        {
            SixLabors.ImageSharp.Image<SixLabors.ImageSharp.Rgba32> image = null;

            try
            {
                //Note: don't new up HttpClient in practice
                //This should be stored in a shared field in practice
                using (System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient())
                {
                    using (System.Net.Http.HttpResponseMessage response = await httpClient.GetAsync(url))
                    {
                        using (System.IO.Stream inputStream = await response.Content.ReadAsStreamAsync())
                        {
                            image = Image.Load(inputStream);
                        }
                    }
                }
            }

            catch
            {
                // Add error logging here
            }
            
            return image;
        }


        public static void TODO(string path)
        {

            SixLabors.ImageSharp.Image<SixLabors.ImageSharp.Rgba32> img =
              SixLabors.ImageSharp.Image.Load(path);

            img.Mutate(x => x.Grayscale());
            img.Mutate(x => x.BlackWhite());
            img.Mutate(x => x.Invert());

            img.Mutate(x => x.Flip(FlipType.Horizontal));

            img.Mutate(x => x.RotateFlip(RotateType.Rotate270, FlipType.None));

            float degrees = 30;
            img.Mutate(x => x.Rotate(degrees, KnownResamplers.Bicubic));


            float degreesX = 5, degreesY = 5;
            img.Mutate(x => x.Skew(degreesX, degreesY, KnownResamplers.Bicubic));

            
            img.Mutate(x => x.OilPaint());
            img.Mutate(x => x.Sepia());


            img.Mutate(x => x.Pixelate());


            SixLabors.Primitives.Rectangle bounds = 
                new SixLabors.Primitives.Rectangle(10, 10, img.Width / 2, img.Height / 2);
            img.Mutate(x => x.BackgroundColor(SixLabors.ImageSharp.PixelFormats.NamedColors<
                SixLabors.ImageSharp.Rgba32>.HotPink, bounds));

            img.Mutate(x => x.GaussianBlur());
            img.Mutate(x => x.GaussianSharpen());
            img.Mutate(x => x.DetectEdges());


            img.Mutate(x => x.Glow());
            img.Mutate(x => x.Vignette());



            SixLabors.ImageSharp.Dithering.IOrderedDither defaultDitherer = 
                new SixLabors.ImageSharp.Dithering.OrderedDither();
            img.Mutate(x => x.Dither(defaultDitherer));

            SixLabors.ImageSharp.Dithering.IErrorDiffuser ad = 
                new SixLabors.ImageSharp.Dithering.AtkinsonDiffuser();
            float threshold = 0.5f;
            img.Mutate(x => x.Dither(ad, threshold));


            SixLabors.Primitives.Rectangle thresholdBounds = 
                new SixLabors.Primitives.Rectangle(10, 10, img.Width / 2, img.Height / 2);
            float thresholdValue = 0.5f;
            img.Mutate(x => x.BinaryThreshold(thresholdValue, thresholdBounds));


            int value = 5;
            SixLabors.Primitives.Rectangle blurBounds = 
                new SixLabors.Primitives.Rectangle(10, 10, img.Width / 2, img.Height / 2);
            img.Mutate(x => x.BoxBlur(value, blurBounds));

            img.Mutate(x => x.ColorBlindness(ColorBlindness.Tritanopia));

        }


        public static void Crop(string path)
        {
            SixLabors.ImageSharp.Image<SixLabors.ImageSharp.Rgba32> img =
              SixLabors.ImageSharp.Image.Load(path);

            int width = 5;
            int height = 5;

            img.Mutate(x => x.Crop(new SixLabors.Primitives.Rectangle(0, 0, width, height)));
        }
        



        public static System.IO.Stream ResizeImage(string path)
        {
            System.IO.Stream outputStream = new System.IO.MemoryStream();

            float ratio = 2.0f;

            SixLabors.ImageSharp.Image<SixLabors.ImageSharp.Rgba32> img =
              SixLabors.ImageSharp.Image.Load(path);


            SixLabors.Primitives.SizeF newSize = img.Size() * ratio;


            /// Compand: whether to compress or expand individual pixel color values on processing.
            img.Mutate(x => x.Resize((SixLabors.Primitives.Size)newSize, KnownResamplers.Bicubic, false));

            //img.Save(null, new SixLabors.ImageSharp.Formats.Png.PngEncoder());
            //img.Save(null, new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder());
            //img.Save(null, new SixLabors.ImageSharp.Formats.Gif.GifEncoder());
            //img.Save(null, new SixLabors.ImageSharp.Formats.Bmp.BmpEncoder());

            img.Save(outputStream, new SixLabors.ImageSharp.Formats.Png.PngEncoder());
            outputStream.Seek(0, System.IO.SeekOrigin.Begin);

            return outputStream;
        }


    }


}
