
using System.Threading.Tasks;



// https://msdn.microsoft.com/en-us/magazine/mt814808.aspx
// https://channel9.msdn.com/Events/Connect/2017/T125
namespace BlueMine
{


    public abstract class ImageHandler
    {

        public abstract bool ImageFormatSupported(SaveFormat imageFormat);

        public abstract Task<object> LoadImageFromUrlAsync(string url);
        

        public abstract System.IO.Stream Crop(
            System.IO.Stream stream,
            SaveFormat saveFormat,
            int x, int y, int width, int height);


        public abstract System.IO.Stream ResizeImage(
            System.IO.Stream inputStream,
            System.IO.Stream outputStream,
            SaveFormat saveFormat,
            float ratio, System.Drawing.Size? maxSize);


        public async Task<byte[]> LoadFileFromUrl(string url)
        {
            byte[] retValue = null;

            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {

                using (System.Net.Http.HttpResponseMessage response = await client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        retValue = await response.Content.ReadAsByteArrayAsync();
                    } // End if (response.IsSuccessStatusCode) 

                } // End Using response 

            } // End Using client 

            return retValue;
        } // End Function LoadFileFromUrl 


        public virtual System.IO.Stream Crop(
            string path,
            SaveFormat saveFormat,
            int width,
            int height)
        {
            return Crop(path, saveFormat, 0, 0, width, height);
        }


        public virtual System.IO.Stream Crop(
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


        public virtual System.IO.Stream Crop(
            System.IO.Stream stream,
            SaveFormat saveFormat,
            int width,
            int height)
        {
            return Crop(stream, saveFormat, 0, 0, width, height);
        }


        public virtual System.IO.Stream Crop(
            byte[] img,
            SaveFormat saveFormat,
            int width,
            int height)
        {
            return Crop(img, saveFormat, 0, 0, width, height);
        }


        public virtual System.IO.Stream Crop(
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
        

        public virtual System.IO.Stream ResizeImage(
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


        public virtual System.IO.Stream ResizeImage(
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
        // HERE

        public virtual System.IO.Stream ResizeImage(
            System.IO.Stream stream,
            SaveFormat saveFormat,
            System.Drawing.Size maxSize)
        {
            return ResizeImage(stream, null, saveFormat, maxSize);
        }



        public virtual System.IO.Stream ResizeImage(
            System.IO.Stream inputStream,
            SaveFormat saveFormat,
            float ratio)
        {
            // returns filled outputStream 
            return ResizeImage(inputStream, null, saveFormat, ratio);
        }


        public virtual System.IO.Stream ResizeImage(
            System.IO.Stream inputStream,
            System.IO.Stream outputStream,
            SaveFormat saveFormat,
            System.Drawing.Size maxSize)
        {
            return ResizeImage(inputStream, outputStream, saveFormat, 0, maxSize);
        }



        public virtual System.IO.Stream ResizeImage(
            System.IO.Stream inputStream,
            System.IO.Stream outputStream,
            SaveFormat saveFormat,
            float ratio)
        {
            return ResizeImage(inputStream, outputStream, saveFormat, ratio, null);
        }

    }


}