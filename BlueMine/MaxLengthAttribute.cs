
namespace BlueMine
{



    class MaxLengthAttribute : System.Attribute
    {
        public long Length;
        public bool IsApplicable;


        public MaxLengthAttribute(long i)
            : this(i.ToString(System.Globalization.CultureInfo.InvariantCulture))
        {
            this.Length = i;
            this.IsApplicable = true;
        }

        public MaxLengthAttribute(string s)
        {
            if (s == null)
            {
                this.IsApplicable = false;
            }

            if ("max".Equals(s, System.StringComparison.OrdinalIgnoreCase))
            {
                this.IsApplicable = true;

                // sysname is a system-supplied user-defined data type 
                // that is functionally equivalent to nvarchar(128), 
                // except that it is not nullable.

                // varchar: from 1 through 8,000. max indicates that the maximum storage size is 2^31-1 bytes (2 GB). 
                //          The storage size is the actual length of the data entered + 2 bytes. 
                //          The ISO synonyms for varchar are charvarying or charactervarying.

                // nvarchar: 1 through 4,000. max indicates that the maximum storage size is 2^31-1 bytes (2 GB). 
                //           The storage size, in bytes, is two times the actual length of data entered + 2 bytes.
                //           The ISO synonyms for nvarchar are national char varying and national character varying.

                // text: Variable-length non-Unicode data in the code page of the server and with a 
                //       maximum string length of 2^31-1 (2,147,483,647) bytes.
                //       Depending on the character string, the storage size may be less than 2,147,483,647 bytes.
                // ntext: 1,073,741,823 unicode Variable-length Unicode data with a maximum string length
                //        Storage size, in bytes, is two times the string length that is entered.
                // image:  Variable-length binary data from 0 through 2^31 - 1(2,147,483,647) bytes.

                this.Length = 2147483648; // 2 GB
                this.Length = 4294967296; // 4 GB
            }
        }
    }


}
