
namespace BlueMine
{


    public static class tex
    {


        public static long ToUnixTicks(this System.DateTime d)
        {
            if (d.Kind == System.DateTimeKind.Utc)
            {
                return (long)(d -
                    new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)
                ).TotalSeconds;
            }

            return (long)(System.TimeZoneInfo.ConvertTimeToUtc(d) -
                new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)
            ).TotalSeconds;
        }


        public static string ToUnixTicksString(this System.DateTime d)
        {
            long ut = ToUnixTicks(d);
            return ut.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }


        public static System.DateTime ToUnixTicks(this long unixTicks)
        {
            return ToUnixTicks(unixTicks, false);
        }


        public static System.DateTime ToUnixTicks(this long unixTicks, bool asUtc)
        {
            System.DateTime dt = new System.DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dt = dt.AddSeconds(unixTicks);

            if (asUtc)
                return dt;

            return System.TimeZoneInfo.ConvertTimeFromUtc(dt, System.TimeZoneInfo.Local);
        }


    }


}
