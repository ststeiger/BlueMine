
namespace System.Drawing
{
    public class StringFormat
    {
        public static StringFormat GenericDefault
        {
            get
            {
                //IntPtr format;
                //int status = SafeNativeMethods.Gdip.GdipStringFormatGetGenericDefault(out format);

                //if (status != SafeNativeMethods.Gdip.Ok)
                //    throw SafeNativeMethods.Gdip.StatusException(status);

                //return new StringFormat(format);

                return new System.Drawing.StringFormat();
            }

            
        }
    }
}
