
namespace ImageSharpPacked
{

    public class ImageSharpPackedMarker 
    {

        public static SixLabors.ImageSharp.Image<SixLabors.ImageSharp.Rgba32> FromStream(System.IO.Stream stream)
        {
            SixLabors.ImageSharp.Image<SixLabors.ImageSharp.Rgba32> img = SixLabors.ImageSharp.Image.Load(stream);
            return img;
        }

    }

}
