
namespace System.Drawing.Imaging 
{


    public sealed class ImageFormat
    {
        private System.Guid m_currentFormat;

        public ImageFormat(System.Guid imageFormat)
        {
            m_currentFormat = imageFormat;
        }

        // Format IDs
        // private static ImageFormat undefined = new ImageFormat(new Guid("{b96b3ca9-0728-11d3-9d7b-0000f81ef32e}"));
        private static ImageFormat memoryBMP = new ImageFormat(new Guid("{b96b3caa-0728-11d3-9d7b-0000f81ef32e}"));
        private static ImageFormat bmp = new ImageFormat(new Guid("{b96b3cab-0728-11d3-9d7b-0000f81ef32e}"));
        private static ImageFormat emf = new ImageFormat(new Guid("{b96b3cac-0728-11d3-9d7b-0000f81ef32e}"));
        private static ImageFormat wmf = new ImageFormat(new Guid("{b96b3cad-0728-11d3-9d7b-0000f81ef32e}"));
        private static ImageFormat jpeg = new ImageFormat(new Guid("{b96b3cae-0728-11d3-9d7b-0000f81ef32e}"));
        private static ImageFormat png = new ImageFormat(new Guid("{b96b3caf-0728-11d3-9d7b-0000f81ef32e}"));
        private static ImageFormat gif = new ImageFormat(new Guid("{b96b3cb0-0728-11d3-9d7b-0000f81ef32e}"));
        private static ImageFormat tiff = new ImageFormat(new Guid("{b96b3cb1-0728-11d3-9d7b-0000f81ef32e}"));
        private static ImageFormat exif = new ImageFormat(new Guid("{b96b3cb2-0728-11d3-9d7b-0000f81ef32e}"));
        private static ImageFormat photoCD = new ImageFormat(new Guid("{b96b3cb3-0728-11d3-9d7b-0000f81ef32e}"));
        private static ImageFormat flashPIX = new ImageFormat(new Guid("{b96b3cb4-0728-11d3-9d7b-0000f81ef32e}"));
        private static ImageFormat icon = new ImageFormat(new Guid("{b96b3cb5-0728-11d3-9d7b-0000f81ef32e}"));


        private static System.Collections.Generic.Dictionary<string, System.Guid> s_string_to_guid= StringToGuidMap();
        private static System.Collections.Generic.Dictionary<System.Guid, string> s_guid_to_string = GuidToStringMap();
        private static System.Collections.Generic.Dictionary<System.Guid, System.Type> s_guid_to_encoderType = GuidToEncoderMap();

        private static System.Collections.Generic.Dictionary<string, System.Guid> 
            StringToGuidMap()
        {
            System.Collections.Generic.Dictionary<string, System.Guid> dict =
                new Collections.Generic.Dictionary<string, System.Guid>
                (System.StringComparer.OrdinalIgnoreCase)
            ;

            dict.Add("undefined", new Guid("{b96b3ca9-0728-11d3-9d7b-0000f81ef32e}"));
            dict.Add("memoryBMP", new Guid("{b96b3caa-0728-11d3-9d7b-0000f81ef32e}"));
            dict.Add("bmp", new Guid("{b96b3cab-0728-11d3-9d7b-0000f81ef32e}"));
            dict.Add("emf", new Guid("{b96b3cac-0728-11d3-9d7b-0000f81ef32e}"));
            dict.Add("wmf", new Guid("{b96b3cad-0728-11d3-9d7b-0000f81ef32e}"));
            dict.Add("jpeg", new Guid("{b96b3cae-0728-11d3-9d7b-0000f81ef32e}"));
            dict.Add("png", new Guid("{b96b3caf-0728-11d3-9d7b-0000f81ef32e}"));
            dict.Add("gif", new Guid("{b96b3cb0-0728-11d3-9d7b-0000f81ef32e}"));
            dict.Add("tiff", new Guid("{b96b3cb1-0728-11d3-9d7b-0000f81ef32e}"));
            dict.Add("exif", new Guid("{b96b3cb2-0728-11d3-9d7b-0000f81ef32e}"));
            dict.Add("photoCD", new Guid("{b96b3cb3-0728-11d3-9d7b-0000f81ef32e}"));
            dict.Add("flashPIX", new Guid("{b96b3cb4-0728-11d3-9d7b-0000f81ef32e}"));
            dict.Add("icon", new Guid("{b96b3cb5-0728-11d3-9d7b-0000f81ef32e}"));

            return dict;
        }


        

        private static System.Collections.Generic.Dictionary<System.Guid, string> 
            GuidToStringMap()
        {
            System.Collections.Generic.Dictionary<System.Guid, string> dict2 =
                new Collections.Generic.Dictionary<System.Guid, string>();

            dict2.Add(new Guid("{b96b3ca9-0728-11d3-9d7b-0000f81ef32e}"), "undefined");
            dict2.Add(new Guid("{b96b3caa-0728-11d3-9d7b-0000f81ef32e}"), "memoryBMP");
            dict2.Add(new Guid("{b96b3cab-0728-11d3-9d7b-0000f81ef32e}"), "bmp");
            dict2.Add(new Guid("{b96b3cac-0728-11d3-9d7b-0000f81ef32e}"), "emf");
            dict2.Add(new Guid("{b96b3cad-0728-11d3-9d7b-0000f81ef32e}"), "wmf");
            dict2.Add(new Guid("{b96b3cae-0728-11d3-9d7b-0000f81ef32e}"), "jpeg");
            dict2.Add(new Guid("{b96b3caf-0728-11d3-9d7b-0000f81ef32e}"), "png");
            dict2.Add(new Guid("{b96b3cb0-0728-11d3-9d7b-0000f81ef32e}"), "gif");
            dict2.Add(new Guid("{b96b3cb1-0728-11d3-9d7b-0000f81ef32e}"), "tiff");
            dict2.Add(new Guid("{b96b3cb2-0728-11d3-9d7b-0000f81ef32e}"), "exif");
            dict2.Add(new Guid("{b96b3cb3-0728-11d3-9d7b-0000f81ef32e}"), "photoCD");
            dict2.Add(new Guid("{b96b3cb4-0728-11d3-9d7b-0000f81ef32e}"), "flashPIX");
            dict2.Add(new Guid("{b96b3cb5-0728-11d3-9d7b-0000f81ef32e}"), "icon");

            return dict2;
        }


        private static System.Collections.Generic.Dictionary<System.Guid, System.Type>
            GuidToEncoderMap()
        {
            System.Collections.Generic.Dictionary<System.Guid, System.Type> dict3 =
                new Collections.Generic.Dictionary<Guid, System.Type>();

            dict3.Add(new Guid("{b96b3ca9-0728-11d3-9d7b-0000f81ef32e}"), null);
            dict3.Add(new Guid("{b96b3caa-0728-11d3-9d7b-0000f81ef32e}"), typeof(SixLabors.ImageSharp.Formats.Bmp.BmpEncoder));
            dict3.Add(new Guid("{b96b3cab-0728-11d3-9d7b-0000f81ef32e}"), typeof(SixLabors.ImageSharp.Formats.Bmp.BmpEncoder));
            dict3.Add(new Guid("{b96b3cac-0728-11d3-9d7b-0000f81ef32e}"), null); // emf
            dict3.Add(new Guid("{b96b3cad-0728-11d3-9d7b-0000f81ef32e}"), null); // wmf
            dict3.Add(new Guid("{b96b3cae-0728-11d3-9d7b-0000f81ef32e}"), typeof(SixLabors.ImageSharp.Formats.Jpeg.JpegEncoder));
            dict3.Add(new Guid("{b96b3caf-0728-11d3-9d7b-0000f81ef32e}"), typeof(SixLabors.ImageSharp.Formats.Png.PngEncoder));
            dict3.Add(new Guid("{b96b3cb0-0728-11d3-9d7b-0000f81ef32e}"), typeof(SixLabors.ImageSharp.Formats.Gif.GifEncoder));
            dict3.Add(new Guid("{b96b3cb1-0728-11d3-9d7b-0000f81ef32e}"), null); // tiff
            dict3.Add(new Guid("{b96b3cb2-0728-11d3-9d7b-0000f81ef32e}"), null); // exif
            dict3.Add(new Guid("{b96b3cb3-0728-11d3-9d7b-0000f81ef32e}"), null); // photoCD
            dict3.Add(new Guid("{b96b3cb4-0728-11d3-9d7b-0000f81ef32e}"), null); // flashPIX
            dict3.Add(new Guid("{b96b3cb5-0728-11d3-9d7b-0000f81ef32e}"), null); // icon

            return dict3;
        }


        /// <devdoc>
        ///    Specifies a memory bitmap image format.
        /// </devdoc>
        public SixLabors.ImageSharp.Formats.IImageEncoder Encoder
        {
            get {
                return (SixLabors.ImageSharp.Formats.IImageEncoder)
                    Activator.CreateInstance(s_guid_to_encoderType[this.m_currentFormat]);
            }
        }



        /// <devdoc>
        ///    Specifies a memory bitmap image format.
        /// </devdoc>
        public static ImageFormat MemoryBmp
        {
            get { return memoryBMP; }
        }


        /// <devdoc>
        ///    Specifies the bitmap image format.
        /// </devdoc>
        public static ImageFormat Bmp
        {
            get { return bmp; }
        }


        /// <devdoc>
        ///    Specifies the enhanced Windows metafile
        ///    image format.
        /// </devdoc>
        public static ImageFormat Emf
        {
            get { return emf; }
        }


        /// <devdoc>
        ///    Specifies the Windows metafile image
        ///    format.
        /// </devdoc>
        public static ImageFormat Wmf
        {
            get { return wmf; }
        }


        /// <devdoc>
        ///    Specifies the GIF image format.
        /// </devdoc>
        public static ImageFormat Gif
        {
            get { return gif; }
        }


        /// <devdoc>
        ///    Specifies the JPEG image format.
        /// </devdoc>
        public static ImageFormat Jpeg
        {
            get { return jpeg; }
        }


        /// <devdoc>
        ///    <para>
        ///       Specifies the W3C PNG image format.
        ///    </para>
        /// </devdoc>
        public static ImageFormat Png
        {
            get { return png; }
        }


        /// <devdoc>
        ///    Specifies the Tag Image File
        ///    Format (TIFF) image format.
        /// </devdoc>
        public static ImageFormat Tiff
        {
            get { return tiff; }
        }


        /// <devdoc>
        ///    Specifies the Exchangable Image Format
        ///    (EXIF).
        /// </devdoc>
        public static ImageFormat Exif
        {
            get { return exif; }
        }


        /// <devdoc>
        ///    <para>
        ///       Specifies the Windows icon image format.
        ///    </para>
        /// </devdoc>
        public static ImageFormat Icon
        {
            get { return icon; }
        }


    }


}
