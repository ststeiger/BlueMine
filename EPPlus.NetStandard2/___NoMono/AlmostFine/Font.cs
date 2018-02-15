
namespace System.Drawing 
{
    
    public class Font
    {
        protected string m_familyName;
        protected float m_size;
        protected FontStyle m_style;

        public Font(string familyName, float emSize, FontStyle style)
        {
            this.m_familyName = familyName;
            this.m_size = emSize;
            this.m_style = style;

            // Initialize(familyName, emSize, style, GraphicsUnit.Point, SafeNativeMethods.DEFAULT_CHARSET, IsVerticalName(familyName));
        }

        /*
        public Font(Font prototype, FontStyle newStyle)
        {
            // Copy over the originalFontName because it won't get initialized
            // this.originalFontName = prototype.OriginalFontName;
            // Initialize(prototype.FontFamily, prototype.Size, newStyle, prototype.Unit, SafeNativeMethods.DEFAULT_CHARSET, false);
        }

        public Font(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit)
        {
            // Initialize(family, emSize, style, unit, SafeNativeMethods.DEFAULT_CHARSET, false);
        }

        public Font(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet)
        {
            // Initialize(family, emSize, style, unit, gdiCharSet, false);
        }

        public Font(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet, bool gdiVerticalFont)
        {
            // Initialize(family, emSize, style, unit, gdiCharSet, gdiVerticalFont);
        }

        public Font(string familyName, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet)
        {
            // Initialize(familyName, emSize, style, unit, gdiCharSet, IsVerticalName(familyName));
        }


        public Font(string familyName, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet, bool gdiVerticalFont)
        {
            //if (float.IsNaN(emSize) || float.IsInfinity(emSize) || emSize <= 0)
            //{
            //    throw new ArgumentException(SR.GetString(SR.InvalidBoundArgument, "emSize", emSize, 0, "System.Single.MaxValue"), "emSize");
            //}

            //Initialize(familyName, emSize, style, unit, gdiCharSet, gdiVerticalFont);
        }


        public Font(FontFamily family, float emSize, FontStyle style)
        {
            // Initialize(family, emSize, style, GraphicsUnit.Point, SafeNativeMethods.DEFAULT_CHARSET, false);
        }

        
        public Font(FontFamily family, float emSize, GraphicsUnit unit)
        {
            // Initialize(family, emSize, FontStyle.Regular, unit, SafeNativeMethods.DEFAULT_CHARSET, false);
        }

        
        public Font(FontFamily family, float emSize)
        {
            // Initialize(family, emSize, FontStyle.Regular, GraphicsUnit.Point, SafeNativeMethods.DEFAULT_CHARSET, false);
        }


        public Font(string familyName, float emSize, FontStyle style, GraphicsUnit unit)
        {
            // Initialize(familyName, emSize, style, unit, SafeNativeMethods.DEFAULT_CHARSET, IsVerticalName(familyName));
        }

        
        public Font(string familyName, float emSize, FontStyle style)
        {
            // Initialize(familyName, emSize, style, GraphicsUnit.Point, SafeNativeMethods.DEFAULT_CHARSET, IsVerticalName(familyName));
        }


        public Font(string familyName, float emSize, GraphicsUnit unit)
        {
            // Initialize(familyName, emSize, FontStyle.Regular, unit, SafeNativeMethods.DEFAULT_CHARSET, IsVerticalName(familyName));
        }
        

        public Font(string familyName, float emSize)
        {
            // Initialize(familyName, emSize, FontStyle.Regular, GraphicsUnit.Point, SafeNativeMethods.DEFAULT_CHARSET, IsVerticalName(familyName));
        }
        */


        public string Name
        {
            get
            {
                //return this.FontFamily.Name;
                return this.m_familyName;
            }
        }

        public FontStyle Style
        {
            get
            {
                return this.m_style;
            }
        }

        // Return value is in Unit (the unit the font was created in)
        public float Size
        {
            get
            {
                return this.m_size;
            }
        }

        public bool Strikeout
        {
            get
            {
                return (Style & FontStyle.Strikeout) != 0;
            }
        }
        public bool Underline
        {
            get
            {
                return (Style & FontStyle.Underline) != 0;
            }
        }

        public bool Bold
        {
            get
            {
                return (Style & FontStyle.Bold) != 0;
            }
        }

        public bool Italic
        {
            get
            {
                return (Style & FontStyle.Italic) != 0;
            }
        }


    }


}
