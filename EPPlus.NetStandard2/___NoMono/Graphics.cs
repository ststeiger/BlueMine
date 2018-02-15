using System;
using System.Collections.Generic;
using System.Text;

namespace System.Drawing
{


    public class Graphics
    {
        protected GraphicsUnit m_unit;
        protected System.Drawing.Image m_image;

        public Graphics()
        {
            m_unit = GraphicsUnit.Pixel;
        }

        public Graphics(System.Drawing.Image img)
            :this()
        {
            this.m_image = img;
        }


        public static Graphics FromImage(System.Drawing.Image img)
        {
            return new Graphics(img);
        }


        public GraphicsUnit PageUnit
        {
            get
            {
                return this.m_unit;
            }
            set
            {
                this.m_unit = value;
            }
        }


        public SizeF MeasureString(String text, Font font, int width, StringFormat format)
        {
            SixLabors.Fonts.FontFamily family = SixLabors.Fonts.SystemFonts.Find(font.Name);
            //font.Bold
            //font.Italic
            //font.Underline
            //font.Strikeout
            
            SixLabors.Fonts.Font fo = new SixLabors.Fonts.Font(family, font.Size);
            SixLabors.Fonts.RendererOptions ro = new SixLabors.Fonts.RendererOptions(fo);

            SixLabors.Primitives.SizeF result = SixLabors.Fonts.TextMeasurer.Measure(text, ro);

            return new System.Drawing.SizeF(result.Width, result.Height);
        }


    }


}
