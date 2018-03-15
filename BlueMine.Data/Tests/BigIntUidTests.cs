
namespace BlueMine.Data.Tests
{


    [System.Diagnostics.DebuggerDisplay("Rand: {nameof(m_rand)}")]
    internal class BigIntUidTests
    {

        private class UidContainer
        {
            public string Number;
            public UInt128 UI;
            public System.Guid UID;
            public string NumberFromUid;


            public UidContainer(UInt128 ui)
            {
                this.UI = ui;

                this.Number = ui.ToString();
                this.UID = ui.ToGuid();
                this.NumberFromUid = UInt128.FromGuid(this.UID).ToString();
            } // End Constructor 

        } // End Class UidContainer 


        public static void Test2()
        {
            // UInt128.DivMod(5, 3);
            UInt128 five = new UInt128(5);
            UInt128 three = new UInt128(3);
            UInt128 product = five % three;


            UInt128 big = new UInt128("001125967647482471318463346559328037643");
            System.Guid bigUID = big.ToGuid();


            System.Console.WriteLine("", five, three, product, bigUID);

            UInt128 a = new UInt128(256) + 3;
            // a = UInt128.Square(a);

            System.Console.WriteLine(a);

            string g = new UInt128(1).ToGuid().ToString();
            //g = UInt128.MinValue.ToGuid().ToString();
            g = new UInt128(16777216 - 1).ToGuid().ToString();
            System.Console.WriteLine(g);
        } // End Sub Test 


        public static void Test()
        {
            BigRandom rand = new BigRandom();

            System.Collections.Generic.List<UidContainer> ls =
                new System.Collections.Generic.List<UidContainer>();

            for (int i = 0; i < 100; ++i)
            {
                UInt128 ui = rand.NextUInt128();
                ls.Add(new UidContainer(ui));
            } // Next i 

            ls.Sort(Compare);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.AppendLine(";WITH CTE AS ( ");

            int padLen = UInt128.MaxValue.ToString().Length;

            for (int i = 0; i < ls.Count; ++i)
            {
                if (i == 0)
                    sb.Append("              ");
                else
                    sb.Append("    UNION ALL ");

                sb.Append("SELECT CAST('");
                sb.Append(ls[i].UID.ToString());
                sb.Append("' AS uniqueidentifier) AS uid, '");

                sb.Append(ls[i].UI.ToString().PadLeft(padLen, '0'));
                sb.Append("' AS num, '");

                sb.Append(ls[i].NumberFromUid.PadLeft(padLen, '0'));
                sb.Append("' AS num2 ");

                sb.AppendLine(System.Environment.NewLine);
            } // Next i 


            sb.AppendLine(@"
) 
SELECT 
     * 
    ,ROW_NUMBER() OVER(ORDER BY uid) AS rnUid 
    ,ROW_NUMBER() OVER(ORDER BY num) AS rnNum 
FROM CTE 
ORDER BY uid 
-- ORDER BY num 
");



            string s = sb.ToString();
            sb.Clear();
            sb = null;

            System.Console.WriteLine(ls);
            System.Console.WriteLine(s);
        } // End Sub Test 


        private static int Compare(UidContainer a, UidContainer b)
        {
            System.Guid x = a.UID;
            System.Guid y = b.UID;

            return Compare(x, y);
        } // End Function Compare 


        private static int Compare(System.Guid x, System.Guid y)
        {
            const int NUM_BYTES_IN_GUID = 16;
            byte byte1, byte2;

            byte[] xBytes = new byte[NUM_BYTES_IN_GUID];
            byte[] yBytes = new byte[NUM_BYTES_IN_GUID];

            x.ToByteArray().CopyTo(xBytes, 0);
            y.ToByteArray().CopyTo(yBytes, 0);

            int[] byteOrder = new int[16] // 16 Bytes = 128 Bit 
                {10, 11, 12, 13, 14, 15, 8, 9, 6, 7, 4, 5, 0, 1, 2, 3};

            // Swap to the correct order to be compared
            for (int i = 0; i < NUM_BYTES_IN_GUID; i++)
            {
                byte1 = xBytes[byteOrder[i]];
                byte2 = yBytes[byteOrder[i]];

                if (byte1 != byte2)
                {
                    int num = (byte1 < byte2) ? -1 : 1;
                    xBytes = null;
                    yBytes = null;
                    byteOrder = null;
                    return num;
                } // End if (byte1 != byte2) 

            } // Next i 

            xBytes = null;
            yBytes = null;
            byteOrder = null;
            return 0;
        } // End Function Compare 


    } // End Class BigIntUidTests 


} // End Namespace BlueMine.Data.Tests 
