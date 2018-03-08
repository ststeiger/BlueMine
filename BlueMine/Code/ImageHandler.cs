
using System.Threading.Tasks;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Helpers;
using SixLabors.ImageSharp.Processing;
using System.Linq;


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
            : this(null, null, true)
        {
        }


        public ImageResult(string contentType, System.IO.Stream imageStream)
            : this(contentType, imageStream, true)
        {
        }
        
        
        public ImageResult(foo_t a)
        {
        }
        
        
        public delegate void foo_t(Microsoft.AspNetCore.Http.HttpResponse response);
        
        
        
        

        public ImageResult(string contentType, System.IO.Stream imageStream, bool dispose)
        {
            if (imageStream == null)
                throw new System.ArgumentNullException(nameof(imageStream));
            if (contentType == null)
                throw new System.ArgumentNullException(nameof(contentType));

            this.ImageStream = imageStream;
            this.ContentType = contentType;
        } // End Constructor 


        public override void ExecuteResult(Microsoft.AspNetCore.Mvc.ActionContext context)
        {
            if (context == null)
                throw new System.ArgumentNullException(nameof(context));

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
            if (this.m_dispose)
                this.m_stream?.Dispose();
        } // End Sub ExecuteResult 


        public async override Task ExecuteResultAsync(
            Microsoft.AspNetCore.Mvc.ActionContext context)
        {
            if (context == null)
                throw new System.ArgumentNullException(nameof(context));

            Microsoft.AspNetCore.Http.HttpResponse response = context.HttpContext.Response;
            
            response.ContentType = this.ContentType;
            response.ContentLength = this.m_stream.Length;

            context.HttpContext.RequestServices
            
            
            byte[] buffer = new byte[4096];
            int read = 0;
            while ((read = await this.ImageStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                // response.OutputStream.Write(buffer, 0, read);
                await response.Body.WriteAsync(buffer, 0, read);
            } // Whend 

            // response.End();
            if (this.m_dispose)
                this.m_stream?.Dispose();
        } // End Sub ExecuteResultAsync 
    } // End Class ImageResult 


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
        
        public static string MimeType( this System.Enum value )
        {
            // get attributes  
            var field = value.GetType().GetField( value.ToString() );
            MimeTypeAttribute[] attributes = (MimeTypeAttribute[])
                field.GetCustomAttributes( typeof( MimeTypeAttribute ), false );
            
            if (attributes == null)
                return null;
            
            return attributes[0].MimeType;
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
    
    
    public class ImageHandler
    {
        
        
        private static SixLabors.ImageSharp.Formats.IImageEncoder
            GetEncoder(SaveFormat saveFormat)
        {
            SixLabors.ImageSharp.Formats.IImageEncoder enc = null;
            switch (saveFormat)
            {
                case SaveFormat.Png:
                    enc = new SixLabors.ImageSharp.Formats.Png.PngEncoder();
                    break;
                case SaveFormat.Jpg:
                    enc = new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder();
                    break;
                case SaveFormat.Gif:
                    enc = new SixLabors.ImageSharp.Formats.Gif.GifEncoder();
                    break;
                case SaveFormat.Bmp:
                    enc = new SixLabors.ImageSharp.Formats.Bmp.BmpEncoder();
                    break;
                default:
                    enc = new SixLabors.ImageSharp.Formats.Png.PngEncoder();
                    break;
            }

            return enc;
        }


        private static async Task<SixLabors.ImageSharp.Image<SixLabors.ImageSharp.Rgba32>>
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
                throw;
            }

            return image;
        }


        public static System.IO.Stream Crop(
            string path,
            SaveFormat saveFormat,
            int width,
            int height)
        {
            return Crop(path, saveFormat, 0, 0, width, height);
        }


        public static System.IO.Stream Crop(
            string path,
            SaveFormat saveFormat,
            int x,
            int y,
            int width,
            int height)
        {
            System.IO.Stream outputStream = null;

            using (System.IO.Stream imageStream = System.IO.File.OpenRead(path))
            {
                outputStream = Crop(imageStream, saveFormat, x, y, width, height);
            }

            return outputStream;
        }


        public static System.IO.Stream Crop(
            System.IO.Stream stream,
            SaveFormat saveFormat,
            int width,
            int height)
        {
            return Crop(stream, saveFormat, 0, 0, width, height);
        }


        public static System.IO.Stream Crop(
            byte[] img,
            SaveFormat saveFormat,
            int width,
            int height)
        {
            return Crop(img, saveFormat, 0, 0, width, height);
        }


        public static System.IO.Stream Crop(
            byte[] img,
            SaveFormat saveFormat,
            int x,
            int y,
            int width,
            int height)
        {
            System.IO.Stream outputStream = null;

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(img))
            {
                outputStream = Crop(ms, saveFormat, x, y, width, height);
            }

            return outputStream;
        }


        public static System.IO.Stream Crop(
            System.IO.Stream stream,
            SaveFormat saveFormat,
            int x, int y, int width, int height)
        {
            SixLabors.ImageSharp.Image<SixLabors.ImageSharp.Rgba32> img =
                SixLabors.ImageSharp.Image.Load(stream);

            return Crop(img, saveFormat, x, y, width, height);
        }


        private static System.IO.Stream Crop(
            SixLabors.ImageSharp.Image<SixLabors.ImageSharp.Rgba32> img,
            SaveFormat saveFormat,
            int x, int y, int width, int height)
        {
            System.IO.Stream outputStream = new System.IO.MemoryStream();

            img.Mutate(a => a.Crop(new SixLabors.Primitives.Rectangle(x, y, width, height)));

            img.Save(outputStream, GetEncoder(saveFormat));
            
            // NO, no seek, if we're respone.OutputStream
            if(outputStream.CanSeek) 
                outputStream.Seek(0, System.IO.SeekOrigin.Begin);
            
            return outputStream;
        }


        public static System.IO.Stream ResizeImage(
            byte[] img,
            SaveFormat saveFormat,
            float ratio)
        {
            System.IO.Stream retValue = null;

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(img))
            {
                retValue = ResizeImage(ms, saveFormat, ratio);
            }

            return retValue;
        }


        public static System.IO.Stream ResizeImage(
            string path,
            SaveFormat saveFormat,
            float ratio)
        {
            System.IO.Stream retValue = null;

            using (System.IO.Stream stream = System.IO.File.OpenRead(path))
            {
                retValue = ResizeImage(stream, saveFormat, ratio);
            }

            return retValue;
        }

        public static System.IO.Stream ResizeImage(
            System.IO.Stream inputStream,
            System.IO.Stream outputStream,
            SaveFormat saveFormat,
            System.Drawing.Size maxSize)
        {
            SixLabors.ImageSharp.Image<SixLabors.ImageSharp.Rgba32> img =
                SixLabors.ImageSharp.Image.Load(inputStream);

            SixLabors.Primitives.SizeF oldSizeSize = img.Size();

            float ratioX = (float) maxSize.Width / (float) img.Width;
            float ratioY = (float) maxSize.Height / (float) img.Width;

            float ratio = ratioX < ratioY ? ratioX : ratioY;
            SixLabors.Primitives.SizeF newSize = img.Size() * ratio;

            return ResizeImage(img, outputStream, saveFormat, newSize);
        }
        

        public static System.IO.Stream ResizeImage(
            System.IO.Stream stream,
            SaveFormat saveFormat,
            System.Drawing.Size maxSize)
        {
            return ResizeImage(stream, null, saveFormat, maxSize);
        }



        public static System.IO.Stream ResizeImage(
            System.IO.Stream inputStream,
            SaveFormat saveFormat,
            float ratio)
        {
            // returns filled outputStream 
            return ResizeImage(inputStream, null, saveFormat, ratio);
        }
        
        
        public static System.IO.Stream ResizeImage(
            System.IO.Stream inputStream,
            System.IO.Stream outputStream,
            SaveFormat saveFormat,
            float ratio)
        {
            SixLabors.ImageSharp.Image<SixLabors.ImageSharp.Rgba32> img =
                SixLabors.ImageSharp.Image.Load(inputStream);
            
            SixLabors.Primitives.SizeF newSize = img.Size() * ratio;
            
            return ResizeImage(img, outputStream, saveFormat, newSize);
        }
        
        
        private static System.IO.Stream ResizeImage(
            SixLabors.ImageSharp.Image<SixLabors.ImageSharp.Rgba32> img,
            System.IO.Stream outputStream,
            SaveFormat saveFormat,
            SixLabors.Primitives.SizeF newSize)
        {
            if (outputStream == null)
                outputStream = new System.IO.MemoryStream();
            
            // Compand: whether to compress or expand individual pixel color values
            img.Mutate(x => x.Resize((SixLabors.Primitives.Size) newSize, KnownResamplers.Bicubic, false));

            img.Save(outputStream, GetEncoder(saveFormat));
            
            // NO, no seek, if we're respone.OutputStream
            if(outputStream.CanSeek) 
                outputStream.Seek(0, System.IO.SeekOrigin.Begin);
            
            return outputStream;
        }


        private static class ImageFormat
        {
            public static SixLabors.ImageSharp.Formats.IImageEncoder Png
            {
                get { return new SixLabors.ImageSharp.Formats.Png.PngEncoder(); }
            }

            public static SixLabors.ImageSharp.Formats.IImageEncoder Jpg
            {
                get { return new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder(); }
            }

            public static SixLabors.ImageSharp.Formats.IImageEncoder Gif
            {
                get { return new SixLabors.ImageSharp.Formats.Gif.GifEncoder(); }
            }

            public static SixLabors.ImageSharp.Formats.IImageEncoder Bmp
            {
                get { return new SixLabors.ImageSharp.Formats.Bmp.BmpEncoder(); }
            }
        }


        private static void TODO(string path)
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
        
        
    }
    
    
}
