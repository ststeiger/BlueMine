
using System.Threading.Tasks;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Helpers;
using SixLabors.ImageSharp.Processing;


// https://msdn.microsoft.com/en-us/magazine/mt814808.aspx
// https://channel9.msdn.com/Events/Connect/2017/T125
namespace BlueMine
{


    public class SixLaborsImageHandler
        : ImageHandler
    {

        public override bool ImageFormatSupported(SaveFormat imageFormat)
        {
            if (imageFormat == SaveFormat.Png)
                return true;

            if (imageFormat == SaveFormat.Jpg)
                return true;

            if (imageFormat == SaveFormat.Gif)
                return true;

            if (imageFormat == SaveFormat.Bmp)
                return true;

            return false;
        }

    // public virtual async Task<SixLabors.ImageSharp.Image<SixLabors.ImageSharp.Rgba32>>
    public override async Task<object>
                LoadImageFromUrlAsync(string url)
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
                        if (response.IsSuccessStatusCode)
                        {

                            using (System.IO.Stream inputStream = await response.Content.ReadAsStreamAsync())
                            {
                                image = SixLabors.ImageSharp.Image.Load(inputStream);
                            } // End Using inputStream 

                        } // End if (response.IsSuccessStatusCode) 

                    } // End Using response 

                } // End Using httpClient 
            }
            catch
            {
                // Add error logging here
                throw;
            }

            return image;
        } // End Function LoadImageFromUrl 


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
        } // End Function GetEncoder 


        public override System.IO.Stream Crop(
            System.IO.Stream stream,
            SaveFormat saveFormat,
            int x, int y, int width, int height)
        {
            SixLabors.ImageSharp.Image<SixLabors.ImageSharp.Rgba32> img =
                SixLabors.ImageSharp.Image.Load(stream);

            System.IO.Stream outputStream = new System.IO.MemoryStream();

            img.Mutate(a => a.Crop(new SixLabors.Primitives.Rectangle(x, y, width, height)));

            img.Save(outputStream, GetEncoder(saveFormat));

            // NO, no seek, if we're respone.OutputStream
            if (outputStream.CanSeek)
                outputStream.Seek(0, System.IO.SeekOrigin.Begin);

            return outputStream;
        } // End Function Crop 


        public override System.IO.Stream ResizeImage(
           System.IO.Stream inputStream,
           System.IO.Stream outputStream,
           SaveFormat saveFormat,
           float ratio,
           System.Drawing.Size? maxSize)
        {
            SixLabors.ImageSharp.Image<SixLabors.ImageSharp.Rgba32> img =
                    SixLabors.ImageSharp.Image.Load(inputStream);
            SixLabors.Primitives.SizeF oldSizeSize = img.Size();

            if (maxSize.HasValue)
            {
                float ratioX = (float)maxSize.Value.Width / (float)img.Width;
                float ratioY = (float)maxSize.Value.Height / (float)img.Width;

                ratio = ratioX < ratioY ? ratioX : ratioY;
            } // End if (maxSize.HasValue) 

            SixLabors.Primitives.SizeF newSize = oldSizeSize * ratio;

            if (outputStream == null)
                outputStream = new System.IO.MemoryStream();


            // Compand: whether to compress or expand individual pixel color values
            img.Mutate(x => x.Resize((SixLabors.Primitives.Size)newSize, KnownResamplers.Bicubic, false));

            img.Save(outputStream, GetEncoder(saveFormat));


            // NO, no seek, if we're respone.OutputStream
            if (outputStream.CanSeek)
                outputStream.Seek(0, System.IO.SeekOrigin.Begin);

            return outputStream;
        } // End Function ResizeImage 


    } // End Class SixLaborsImageHandler


} // End Namespace BlueMine 
