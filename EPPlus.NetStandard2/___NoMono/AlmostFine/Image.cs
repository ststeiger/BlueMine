
namespace System.Drawing
{


    public class Image
        :IDisposable 
    {

        protected SixLabors.ImageSharp.Image<SixLabors.ImageSharp.Rgba32> m_sixImage;

        public Image(SixLabors.ImageSharp.Image<SixLabors.ImageSharp.Rgba32> sixImage)
        {
            this.m_sixImage = sixImage;
        }

        public Image(int w, int h)
        {
            this.m_sixImage = new SixLabors.ImageSharp.Image<SixLabors.ImageSharp.Rgba32>(w, h);
        }



        public static Image FromStream(System.IO.Stream stream)
        {
            SixLabors.ImageSharp.Image<SixLabors.ImageSharp.Rgba32> img = 
                SixLabors.ImageSharp.Image.Load(stream);

            return new Image(img);
        } // End Function FromStream 


        public static Image FromFile(String filename)
        {
            if (!System.IO.File.Exists(filename))
            {
                throw new System.IO.FileNotFoundException(filename);
            }
            
            filename = System.IO.Path.GetFullPath(filename);
            
            using (System.IO.FileStream fs = System.IO.File.OpenRead(filename))
            {
                return FromStream(fs);
            }

        } // End Function FromFile 


        public void Save(string filename)
        {
            Save(filename, RawFormat);
        } // End Sub Save 


        public void Save(string filename, System.Drawing.Imaging.ImageFormat format)
        {
            if (format == null)
                throw new ArgumentNullException("format");

            using (var fs = System.IO.File.OpenWrite(filename))
            {
                this.m_sixImage.Save(fs, format.Encoder);
            }
        } // End Sub Save 


        public void Save(System.IO.Stream stream, System.Drawing.Imaging.ImageFormat format)
        {
            if (format == null)
                throw new ArgumentNullException("format");

            this.m_sixImage.Save(stream, format.Encoder);
        } // End Sub Save 


        public int Width
        {
            get
            {
                return this.m_sixImage.Width;
            }
        }


        public int Height
        {
            get
            {
                return this.m_sixImage.Height;
            }
        }


        
        /// <devdoc>
        ///    Gets the horizontal resolution, in
        ///    pixels-per-inch, of this <see cref='System.Drawing.Image'/>.
        /// </devdoc>
        public float HorizontalResolution
        {
            get
            {
                return (float)this.m_sixImage.MetaData.HorizontalResolution;
            }
        }

        
        /// <devdoc>
        ///    Gets the vertical resolution, in
        ///    pixels-per-inch, of this <see cref='System.Drawing.Image'/>.
        /// </devdoc>
        public float VerticalResolution
        {
            get
            {
                return (float)this.m_sixImage.MetaData.VerticalResolution;
            }
        }
        

        public System.Drawing.Imaging.ImageFormat RawFormat
        {
            get
            {
                Guid guid = new Guid("{b96b3caf-0728-11d3-9d7b-0000f81ef32e}"); // PNG

                return new System.Drawing.Imaging.ImageFormat(guid);
            }
        }






        #region IDisposable Support
        private bool disposedValue = false; // Dient zur Erkennung redundanter Aufrufe.

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: verwalteten Zustand (verwaltete Objekte) entsorgen.
                    this.m_sixImage.Dispose();
                }

                // TODO: nicht verwaltete Ressourcen (nicht verwaltete Objekte) freigeben und Finalizer weiter unten überschreiben.
                // TODO: große Felder auf Null setzen.

                disposedValue = true;
            }
        }

        // TODO: Finalizer nur überschreiben, wenn Dispose(bool disposing) weiter oben Code für die Freigabe nicht verwalteter Ressourcen enthält.
        // ~Image() {
        //   // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in Dispose(bool disposing) weiter oben ein.
        //   Dispose(false);
        // }

        // Dieser Code wird hinzugefügt, um das Dispose-Muster richtig zu implementieren.
        void IDisposable.Dispose()
        {
            // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in Dispose(bool disposing) weiter oben ein.
            Dispose(true);
            // TODO: Auskommentierung der folgenden Zeile aufheben, wenn der Finalizer weiter oben überschrieben wird.
            // GC.SuppressFinalize(this);
        }
        #endregion

        public virtual void Dispose()
        {
            ((IDisposable)this).Dispose();
        }


    } // End Class Image 


}
