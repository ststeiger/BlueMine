
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;


namespace BlueMine.Trash
{


    internal class ImageFormat
    {
        public SixLabors.ImageSharp.Formats.IImageEncoder Png
        {
            get { return new SixLabors.ImageSharp.Formats.Png.PngEncoder(); }
        }

        public SixLabors.ImageSharp.Formats.IImageEncoder Jpg
        {
            get { return new SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder(); }
        }

        public SixLabors.ImageSharp.Formats.IImageEncoder Gif
        {
            get { return new SixLabors.ImageSharp.Formats.Gif.GifEncoder(); }
        }

        public SixLabors.ImageSharp.Formats.IImageEncoder Bmp
        {
            get { return new SixLabors.ImageSharp.Formats.Bmp.BmpEncoder(); }
        }


        private void TODO(string path)
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
