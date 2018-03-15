
namespace BlueMine.Data
{


    public class BigRandom
    {

        protected System.Random m_rand;


        public BigRandom()
        {
            this.m_rand = new System.Random();
        }


        public double NextDouble()
        {
            return this.m_rand.NextDouble();
        }


        public byte[] NextBytes(int length)
        {
            byte[] buf = new byte[length];
            this.m_rand.NextBytes(buf);
            return buf;
        }


        public int NextInt()
        {
            byte[] buf = new byte[4];
            this.m_rand.NextBytes(buf);
            int res = System.BitConverter.ToInt32(buf, 0);
            buf = null;
            return res;
        }


        public int NextInt(int min, int max)
        {
            int intRand = this.NextInt();

            return (System.Math.Abs(intRand % (max - min)) + min);
        }


        public uint NextUInt()
        {
            byte[] buf = new byte[4];
            this.m_rand.NextBytes(buf);
            uint res = System.BitConverter.ToUInt32(buf, 0);
            buf = null;

            return res;
        }


        public uint NextUInt(uint min, uint max)
        {
            uint uintRand = this.NextUInt();

            return (uintRand % (max - min) + min);
        }


        public long NextLong()
        {
            byte[] buf = new byte[8];
            this.m_rand.NextBytes(buf);
            long res = System.BitConverter.ToInt64(buf, 0);
            buf = null;
            return res;
        }


        public long NextLong(long min, long max)
        {
            long longRand = this.NextLong();
            return (System.Math.Abs(longRand % (max - min)) + min);
        }


        public ulong NextULong()
        {
            byte[] buf = new byte[8];
            this.m_rand.NextBytes(buf);
            ulong res = System.BitConverter.ToUInt64(buf, 0);
            buf = null;
            return res;
        }


        public ulong NextULong(ulong min, ulong max)
        {
            ulong ulongRand = this.NextULong();

            return (ulongRand % (max - min) + min);
        }


        public UInt128 NextUInt128()
        {
            ulong low = NextULong();
            ulong high = NextULong();

            return new UInt128(low, high);
        }


        public UInt128 NextUInt128(UInt128 min, UInt128 max)
        {
            UInt128 uintRand = this.NextUInt128();

            return (uintRand % (max - min) + min);
        }


    }


}
