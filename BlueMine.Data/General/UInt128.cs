
// https://www.codeproject.com/Tips/785014/UInt-Division-Modulus

namespace BlueMine.Data 
{


    public class UInt128
        : System.IComparable
            , System.IComparable<UInt128>
            , System.Collections.Generic.IComparer<UInt128>
            , System.IEquatable<UInt128>
    {

        private ulong Low;
        private ulong High;


        /*
        private static uint int2uint_old(int a)
        {
            int sign = System.Math.Sign(a);
            uint val = (uint) System.Math.Abs(a);

            uint unsignedValue;
            if(sign > 0) // +a
                unsignedValue = unchecked(uint.MaxValue + val + 1);
            else // -a, a=0
                unsignedValue = unchecked(uint.MaxValue - val + 1);

            return unsignedValue;
        }


        private static uint int2uint(int a)
        {
            int sign = a < 0 ? -1 : (a > 0 ? 1 : 0);
            uint val = (uint) (sign == -1 ? -1 * a : a);
            
            uint unsignedValue;
            if(sign > 0) // +a
                unsignedValue = unchecked(uint.MaxValue + val + 1);
            else // -a, a=0
                unsignedValue = unchecked(uint.MaxValue - val + 1);

            return unsignedValue;
        }
        
        

        private static ulong long2ulong(long a)
        {
            int sign = a < 0 ? -1 : (a > 0 ? 1 : 0);
            ulong val = (ulong) (sign == -1 ? -1 * a : a);
            
            ulong unsignedValue;
            if(sign > 0) // +a
                unsignedValue = unchecked(ulong.MaxValue + val + 1);
            else // -a, a=0
                unsignedValue = unchecked(ulong.MaxValue - val + 1);

            return unsignedValue;
        }
        */
        
        
        //long2ulong
        
        public static long MapULongToLong(ulong ulongValue)
        {
            return unchecked((long)ulongValue + long.MinValue);
        }

        public static ulong MapLongToUlong(long longValue)
        {
            return unchecked((ulong)(longValue - long.MinValue));
        }
        
        
        
        public long LowSigned
        {
            get { return MapULongToLong(this.Low); }
        }
        
        
        public long HighSigned
        {
            get { return MapULongToLong(this.High); }
        }
        
        
        public static UInt128 FromSignedValues(long low, long high)
        {
            ulong ulow = MapLongToUlong(low);
            ulong uhigh = MapLongToUlong(high);
            
            return new UInt128(ulow, uhigh);
        }
        
        
        public UInt128(ulong low, ulong high)
        {
            this.Low = low;
            this.High = high;
        } // End Constructor 


        public UInt128(UInt128 number)
            : this(number.Low, number.High)
        { } // End Constructor 


        public UInt128(ulong low)
            : this(low, 0)
        { } //End Constructor 


        public UInt128(uint low)
            : this(low, 0)
        { } //End Constructor 


        public UInt128(int low)
            : this((uint)low, 0)
        { } //End Constructor 

        public UInt128(long low)
            : this((ulong)low, 0)
        { } //End Constructor 


        public UInt128()
            : this(0, 0)
        { } // End Constructor 


        private static int CharToInt(char ch, uint radix)
        {
            int n = -1;

            if (ch >= 'A' && ch <= 'Z')
            {
                if (((ch - 'A') + 10) < radix)
                {
                    n = (ch - 'A') + 10;
                }
                else
                {
                    throw new System.InvalidOperationException("Char c ∈ [A, Z] but radix < c");
                }
            }
            else if (ch >= 'a' && ch <= 'z')
            {
                if (((ch - 'a') + 10) < radix)
                {
                    n = (ch - 'a') + 10;
                }
                else
                {
                    throw new System.InvalidOperationException("Char c ∈ [a, z] but radix < c");
                }
            }
            else if (ch >= '0' && ch <= '9')
            {
                if ((ch - '0') < radix)
                {
                    n = (ch - '0');
                }
                else
                {
                    throw new System.InvalidOperationException("Char c ∈ [0, 9] but radix < c");
                }
            }
            else
            {
                throw new System.InvalidOperationException("Completely invalid character");
            }

            return n;
        } // End Function CharToInt 


        public UInt128(string sz, uint radix)
            : this(0, 0)
        {
            sz = sz.TrimStart(' ', '\t', '0');
            sz = sz.TrimEnd(' ', '\t');

            if (sz[sz.Length - 1] == '-')
            {
                throw new System.Exception("UInt128 doesn't allow negative numbers.");
            } // End if (sz[sz.Length-1] == '-')

            UInt128 value = new UInt128();

            uint j = 0;
            for (int i = sz.Length - 1; i > -1; --i)
            {
                int d = CharToInt(sz[i], radix);
                UInt128 digitValue = d * Power(radix, j);
                value += digitValue;
                j++;
            } // Next i 

            this.Low = value.Low;
            this.High = value.High;
        } // End Constructor 


        public UInt128(string sz)
            : this(sz, 10)
        {
        }


        public static UInt128 MaxValue
        {
            get { return new UInt128(ulong.MaxValue, ulong.MaxValue); }
        }


        public static UInt128 MinValue
        {
            get { return new UInt128(ulong.MinValue, ulong.MinValue); }
        }


        // http://stackoverflow.com/questions/11656241/how-to-print-uint128-t-number-using-gcc#answer-11659521
        public override string ToString()
        {
            uint[] buf = new uint[40];

            uint i, j, m = 39;
            for (i = 64; i-- > 0;)
            {
                int usi = (int)i;
                // UInt128 n = value;
                // int carry = !!(n & ((UInt128)1 << i));
                ulong carry = (this.High & (1UL << usi));
                carry = carry != 0 ? 1UL : 0UL; // ToBool
                carry = carry == 0 ? 1UL : 0UL; // ! 
                carry = carry == 0 ? 1UL : 0UL; // ! 

                for (j = 39; j-- > m + 1 || carry != 0;)
                {
                    ulong d = 2 * buf[j] + carry;
                    carry = d > 9 ? 1UL : 0UL;
                    buf[j] = carry != 0 ? (char)(d - 10) : (char)d;
                } // Next j 

                m = j;
            } // Next i 

            for (i = 64; i-- > 0;)
            {
                int usi = (int)i;
                ulong carry = (this.Low & (1UL << usi));
                carry = carry != 0 ? 1UL : 0UL; // ToBool
                carry = carry == 0 ? 1UL : 0UL; // ! 
                carry = carry == 0 ? 1UL : 0UL; // ! 

                for (j = 39; j-- > m + 1 || carry != 0;)
                {
                    ulong d = 2 * buf[j] + carry;
                    carry = d > 9 ? 1UL : 0UL;
                    buf[j] = carry != 0 ? (char)(d - 10) : (char)d;
                } // Next j 

                m = j;
            } // Next i 

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            bool hasFirstNonNull = false;
            for (int k = 0; k < buf.Length - 1; ++k)
            {

                if (hasFirstNonNull || buf[k] != 0)
                {
                    hasFirstNonNull = true;
                    sb.Append(buf[k].ToString());
                } // End if(hasFirstNonNull || buf[k] != 0)

            } // Next k 

            if (sb.Length == 0)
                sb.Append('0');

            string s = sb.ToString();
            sb.Length = 0;
            sb = null;
            return s;
        } // End Function ToString 


        public string ToAnyBase(ulong @base)
        {
            UInt128 num = new UInt128(this);

            string retValue = null;
            string latinBase = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if ((int)@base > latinBase.Length)
                throw new System.ArgumentException("Base value not supported.");

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            do
            {
                char c = latinBase[(int)(num % @base)];
                sb.Insert(0, c);
                num = num / @base;
            } while (num > 0);

            retValue = sb.ToString();
            sb.Clear();
            sb = null;

            return retValue;
        }


        public string ToLogicalGuidString(bool ascending)
        {
            ulong @base = 16;
            UInt128 num = new UInt128(this);

            string retValue = null;
            string latinBase = "0123456789ABCDEF";

            char[] result = new char[36];


            if (ascending)
            {
                for (int i = 35; i > -1; --i)
                {
                    if (i == 8 || i == 13 || i == 18 || i == 23)
                    {
                        result[i] = '-';
                        continue;
                    } // End if 

                    result[i] = latinBase[(int)(num % @base)];
                    num = num / @base;
                } // Next i 
            }
            else
            {
                string hexString = ToAnyBase(16);
                int count = 0;
                for (int i = 0; i < 36; ++i)
                {
                    if (i == 8 || i == 13 || i == 18 || i == 23)
                    {
                        result[i] = '-';
                        continue;
                    } // End if 

                    if (count < hexString.Length)
                        result[i] = hexString[count];
                    else
                        result[i] = '0';

                    count++;
                } // Next i 

            }

            ////for (int i = 35; i > -1; --i)
            //// for (int i = 0; i < 36; ++i)
            //{
            //    if (i == 8 || i == 13 || i == 18 || i == 23)
            //    {
            //        result[i] = '-';
            //        continue;
            //    }

            //    result[i] = latinBase[(int)(num % @base)];
            //    num = num / @base;
            //} // Next i 

            retValue = new string(result);
            result = null;

            return retValue;
        }


        public string ToLogicalGuidString()
        {
            return this.ToLogicalGuidString(true);
        }


        public System.Guid ToLogicalGuid(bool ascending)
        {
            return new System.Guid(this.ToLogicalGuidString(ascending));
        }


        public System.Guid ToLogicalGuid()
        {
            return this.ToLogicalGuid(true);
        }


        public byte[] ToByteArray()
        {
            byte[] bytes = new byte[16];
            byte[] upperBytes = System.BitConverter.GetBytes(this.High);
            byte[] lowerBytes = System.BitConverter.GetBytes(this.Low);

            // System.Array.Copy(upperBytes, 0, bytes, 0, 8);
            // System.Array.Copy(lowerBytes, 0, bytes, 8, 8);

            // Low to High - should be exactly as ToByteArrayLowToHighEnsured
            System.Array.Copy(lowerBytes, 0, bytes, 0, 8);
            System.Array.Copy(upperBytes, 0, bytes, 8, 8);

            upperBytes = null;
            lowerBytes = null;

            return bytes;
        }


        public byte[] ToByteArrayLowToHighEnsured()
        {
            UInt128 num = new UInt128(this);
            uint @base = 256;

            byte[] lowToHigh = new byte[16];
            int i = 0;
            do
            {
                lowToHigh[i] = (byte)(num % @base);
                num = num / @base;
                ++i;
            } while (num > 0);

            for (; i < 16; ++i)
            {
                lowToHigh[i] = 0;
            } // Next i 

            return lowToHigh;
        } // End Function ToByteArrayLowToHighEnsured 


        public byte[] ToGuidBytes()
        {
            // byte[] ba = this.ToByteArrayLowToHighEnsured();
            byte[] ba = this.ToByteArray(); // Fast 

            int[] guidByteOrder = new int[16] // 16 Bytes = 128 Bit 
               {10, 11, 12, 13, 14, 15,  8,  9,  6,  7,  4,  5,  0,  1,  2,  3};
            // {00, 01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14, 15}

            byte[] guidBytes = new byte[16];
            for (int i = 0; i < 16; ++i)
            {
                guidBytes[guidByteOrder[15 - i]] = ba[i];
            } // Next i 
            guidByteOrder = null;
            ba = null;

            return guidBytes;
        } // End Function ToGuidBytes 


        public System.Guid ToGuid()
        {
            return new System.Guid(this.ToGuidBytes());
        }
        

        public static UInt128 FromGuid(System.Guid uid)
        {
            byte[] ba = uid.ToByteArray();

            int[] guidByteOrder = new int[16] // 16 Bytes = 128 Bit 
               {10, 11, 12, 13, 14, 15,  8,  9,  6,  7,  4,  5,  0,  1,  2,  3};
            // {00, 01, 02, 03, 04, 05, 06, 07, 08, 09, 10, 11, 12, 13, 14, 15}

            // Low to High - should be exactly as ToByteArrayLowToHighEnsured
            byte[] bigintBytes = new byte[16];
            for (int i = 0; i < 16; ++i)
            {
                bigintBytes[i] = ba[guidByteOrder[15 - i]];
            } // Next i 

            guidByteOrder = null;
            ba = null;

            // byte[] upperBytes = new byte[8];
            // byte[] lowerBytes = new byte[8];
            // System.Array.Copy(bigintBytes, 0, lowerBytes, 0, 8);
            // System.Array.Copy(bigintBytes, 8, upperBytes, 0, 8);

            ulong lower = System.BitConverter.ToUInt64(bigintBytes, 0);
            ulong upper = System.BitConverter.ToUInt64(bigintBytes, 8);
            bigintBytes = null;

            UInt128 reconstruct = new UInt128(lower, upper);
            return reconstruct;
        } // End Function FromGuid 


        public static UInt128 FromGuid(string guid)
        {
            System.Guid uid = new System.Guid(guid);
            return FromGuid(uid);
        } // End Function FromGuid 


        public string ToIpV6()
        {
            string ipString = "";
            //we display in total 4 parts for every long

            ulong crtLong = this.Low;
            for (int i = 0; i < 4; ++i)
            {
                ipString = (crtLong & 0xFFFF).ToString("x04") + (ipString == string.Empty ? "" : ":" + ipString);
                crtLong = crtLong >> 16;
            } // Next j 

            crtLong = this.High;
            for (int i = 0; i < 4; ++i)
            {
                ipString = (crtLong & 0xFFFF).ToString("x04") + (ipString == string.Empty ? "" : ":" + ipString);
                crtLong = crtLong >> 16;
            } // Next j 
            return ipString;
        } // End Function ToIpV6 



        public static UInt128 operator +(UInt128 left, UInt128 right)
        {
            return Add(left, right);
        }

        public static UInt128 operator +(ulong left, UInt128 right)
        {
            return Add(left, right);
        }

        public static UInt128 operator +(uint left, UInt128 right)
        {
            return Add(left, right);
        }

        public static UInt128 operator +(UInt128 left, ulong right)
        {
            return Add(left, right);
        }

        public static UInt128 operator +(UInt128 left, uint right)
        {
            return Add(left, right);
        }


        public static UInt128 operator -(UInt128 left, UInt128 right)
        {
            return Subtract(left, right);
        }


        public static UInt128 operator -(ulong left, UInt128 right)
        {
            return Subtract(left, right);
        }


        public static UInt128 operator -(uint left, UInt128 right)
        {
            return Subtract(left, right);
        }


        public static UInt128 operator -(UInt128 left, ulong right)
        {
            return Subtract(left, right);
        }


        public static UInt128 operator -(UInt128 left, uint right)
        {
            return Subtract(left, right);
        }

        public static UInt128 operator *(UInt128 left, UInt128 right)
        {
            return Multiply(left, right);
        }


        public static UInt128 operator *(UInt128 left, ulong right)
        {
            return Multiply(left, right);
        }


        public static UInt128 operator *(ulong left, UInt128 right)
        {
            return Multiply(left, right);
        }

        public static UInt128 operator *(UInt128 left, uint right)
        {
            return Multiply(left, right);
        }

        public static UInt128 operator *(int left, UInt128 right)
        {
            return Multiply((uint)left, right);
        }

        public static UInt128 operator *(long left, UInt128 right)
        {
            return Multiply((ulong)left, right);
        }


        public static UInt128 operator *(uint left, UInt128 right)
        {
            return Multiply(left, right);
        }


        public static UInt128 operator /(UInt128 dividend, UInt128 divisor)
        {
            return Div(dividend, divisor);
        }

        public static UInt128 operator /(UInt128 dividend, ulong divisor)
        {
            return Div(dividend, divisor);
        }

        public static UInt128 operator /(UInt128 dividend, uint divisor)
        {
            return Div(dividend, divisor);
        }


        public static UInt128 operator %(UInt128 dividend, UInt128 divisor)
        {
            return Mod(dividend, divisor);
        }

        public static UInt128 operator %(UInt128 dividend, ulong divisor)
        {
            return Mod(dividend, divisor);
        }

        public static UInt128 operator %(UInt128 dividend, uint divisor)
        {
            return Mod(dividend, divisor);
        }


        public static UInt128 operator >>(UInt128 value, int shift)
        {
            return ShiftLeft(value, (uint)shift);
        }


        public static UInt128 operator <<(UInt128 value, int shift)
        {
            return ShiftRight(value, (uint)shift);
        }


        public static UInt128 operator &(UInt128 left, UInt128 right)
        {
            return And(left, right);
        }


        public static UInt128 operator |(UInt128 left, UInt128 right)
        {
            return Or(left, right);
        }


        public static UInt128 operator ^(UInt128 left, UInt128 right)
        {
            return XOR(left, right);
        }


        public static UInt128 operator ~(UInt128 left)
        {
            return Not(left);
        }


        public static bool operator ==(UInt128 lhs, UInt128 rhs)
        {
            return (lhs.High == rhs.High && lhs.Low == rhs.Low);
        }


        public static bool operator !=(UInt128 lhs, UInt128 rhs)
        {
            return !(lhs == rhs);
        }


        public static bool operator ==(UInt128 lhs, ulong rhs)
        {
            if (lhs.High != 0)
                return false;

            return (lhs.Low == rhs);
        }


        public static bool operator !=(UInt128 lhs, ulong rhs)
        {
            if (lhs.High != 0)
                return true;

            return (lhs.Low != rhs);
        }


        public static bool operator <(UInt128 lhs, UInt128 rhs)
        {
            if (lhs.High < rhs.High)
                return true;

            return (lhs.High == rhs.High && lhs.Low < rhs.Low);
        }


        public static bool operator >(UInt128 lhs, UInt128 rhs)
        {
            if (lhs.High > rhs.High)
                return true;

            return (lhs.High == rhs.High && lhs.Low > rhs.Low);
        }


        public static bool operator <=(UInt128 lhs, UInt128 rhs)
        {
            if (lhs.High < rhs.High)
                return true;

            if (lhs.High == rhs.High)
            {
                if (lhs.Low <= rhs.Low)
                    return true;
            }

            return false;
        }


        public static bool operator >=(UInt128 lhs, UInt128 rhs)
        {
            if (lhs.High > rhs.High)
                return true;

            if (lhs.High == rhs.High)
            {
                if (lhs.Low >= rhs.Low)
                    return true;
            } // End if (lhs.High == rhs.High)

            return false;
        }

        // User-defined conversion from UInt128 to int 
        public static explicit operator int(UInt128 num)
        {
            return (int)num.Low;
        }


        // User-defined conversion from UInt128 to byte 
        public static explicit operator byte(UInt128 num)
        {
            return (byte)num.Low;
        }


        // User-defined conversion from UInt128 to uint 
        public static explicit operator uint(UInt128 num)
        {
            return (uint)num.Low;
        }


        // User-defined conversion from UInt128 to long 
        public static explicit operator long(UInt128 num)
        {
            return (long)num.Low;
        }


        // User-defined conversion from UInt128 to ulong 
        public static explicit operator ulong(UInt128 num)
        {
            return num.Low;
        }


        // User-defined conversion from uint to UInt128 
        public static implicit operator UInt128(uint num)
        {
            return new UInt128(num);
        }


        // User-defined conversion from ulong to UInt128 
        public static implicit operator UInt128(ulong num)
        {
            return new UInt128(num);
        }


        // User-defined conversion from long to UInt128 
        public static explicit operator UInt128(long num)
        {
            return new UInt128((ulong)num);
        }


        // User-defined conversion from ulong to UInt128 
        public static explicit operator UInt128(int num)
        {
            return new UInt128((uint)num);
        }


        int CompareTo(object obj)
        {
            if (obj == null)
                return 1; // https://msdn.microsoft.com/en-us/library/system.icomparable.compareto(v=vs.110).aspx

            System.Type t = obj.GetType();

            if (object.ReferenceEquals(t, typeof(UInt128)))
            {
                UInt128 ui = (UInt128)obj;
                return compare128(this, ui);
            } // End if (object.ReferenceEquals(t, typeof(UInt128)))

            if (object.ReferenceEquals(t, typeof(System.DBNull)))
                return 1;

            ulong? lowerPart = obj as ulong?;
            if (!lowerPart.HasValue)
                return 1;

            UInt128 compareTarget = new UInt128(lowerPart.Value, 0);
            return compare128(this, compareTarget);
        } // End Function CompareTo(object obj)


        int System.IComparable.CompareTo(object obj)
        {
            return this.CompareTo(obj);
        }


        int CompareTo(UInt128 other)
        {
            return compare128(this, other);
        }


        int System.IComparable<UInt128>.CompareTo(UInt128 other)
        {
            return compare128(this, other);
        }



        int Compare(UInt128 x, UInt128 y)
        {
            return compare128(x, y);
        }


        int System.Collections.Generic.IComparer<UInt128>.Compare(UInt128 x, UInt128 y)
        {
            return compare128(x, y);
        }


        public bool Equals(UInt128 other)
        {
            return this.High == other.High && this.Low == other.Low;
        }


        bool System.IEquatable<UInt128>.Equals(UInt128 other)
        {
            return this.Equals(other);
        }




        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <returns>
        /// true if <paramref name="obj"/> and this instance are the same type and represent the same value; otherwise, false.
        /// </returns>
        /// <param name="obj">Another object to compare to. </param>
        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj))
                return false;

            return obj is UInt128 && this.Equals((UInt128)obj);
        }


        public override int GetHashCode()
        {
            // return 37 * this.High.GetHashCode() + this.Low.GetHashCode();
            return this.Low.GetHashCode() ^ this.High.GetHashCode();
        }


        public static UInt128 Add(UInt128 N, UInt128 M)
        {
            UInt128 A = new UInt128();
            add128(N, M, ref A);
            return A;
        }


        public static UInt128 Subtract(UInt128 N, UInt128 M)
        {
            UInt128 A = new UInt128();
            sub128(N, M, ref A);
            return A;
        }


        public static UInt128 Multiply(UInt128 N, UInt128 M)
        {
            UInt128 Ans = new UInt128();
            mult128(N, M, ref Ans);
            return Ans;
        }


        public static UInt128 Square(UInt128 R)
        {
            UInt128 Ans = new UInt128();

            sqr128(R, ref Ans);
            return Ans;
        }


        public UInt128 Power(UInt128 @base, uint power)
        {
            UInt128 num = new UInt128(1);

            for (int i = 0; i < power; ++i)
            {
                num = num * @base;
            }

            return num;
        }


        public static UInt128 Div(UInt128 M, UInt128 N)
        {
            UInt128 Q = new UInt128();
            div128(M, N, ref Q);
            return Q;
        }


        public static UInt128 Mod(UInt128 M, UInt128 N)
        {
            UInt128 R = new UInt128();
            mod128(M, N, ref R);
            return R;
        }


        public static System.ValueTuple<UInt128, UInt128> DivMod(UInt128 M, UInt128 N)
        {
            UInt128 Q = new UInt128();
            UInt128 R = new UInt128();
            bindivmod128(M, N, ref Q, ref R);

            return (Q, R);
        }



        public static UInt128 Not(UInt128 N)
        {
            UInt128 A = new UInt128();
            not128(N, ref A);
            return A;
        }


        public static UInt128 Or(UInt128 N1, UInt128 N2)
        {
            UInt128 A = new UInt128();
            or128(N1, N2, ref A);
            return A;
        }


        public static UInt128 And(UInt128 N1, UInt128 N2)
        {
            UInt128 A = new UInt128();
            and128(N1, N2, ref A);
            return A;
        }


        public static UInt128 XOR(UInt128 N1, UInt128 N2)
        {
            UInt128 A = new UInt128();
            xor128(N1, N2, ref A);
            return A;
        }


        public static UInt128 ShiftLeft(UInt128 N, uint S)
        {
            UInt128 A = new UInt128();
            shiftleft128(N, S, ref A);
            return A;
        }


        public static UInt128 ShiftRight(UInt128 N, uint S)
        {
            UInt128 A = new UInt128();
            shiftright128(N, S, ref A);
            return A;
        }


        private static void inc128(UInt128 N, ref UInt128 A)
        {
            A.Low = (N.Low + 1);
            A.High = N.High + (((N.Low ^ A.Low) & N.Low) >> 63);
        }


        private static void dec128(UInt128 N, ref UInt128 A)
        {
            A.Low = N.Low - 1;
            A.High = N.High - (((A.Low ^ N.Low) & A.Low) >> 63);
        }


        private static void add128(UInt128 N, UInt128 M, ref UInt128 A)
        {
            ulong C = (((N.Low & M.Low) & 1) + (N.Low >> 1) + (M.Low >> 1)) >> 63;
            A.High = N.High + M.High + C;
            A.Low = N.Low + M.Low;
        }


        private static void sub128(UInt128 N, UInt128 M, ref UInt128 A)
        {
            A.Low = N.Low - M.Low;
            ulong C = (((A.Low & M.Low) & 1) + (M.Low >> 1) + (A.Low >> 1)) >> 63;
            A.High = N.High - (M.High + C);
        }


        private static void shiftleft128(UInt128 N, uint S, ref UInt128 A)
        {
            ulong M1, M2;
            S &= 127;

            M1 = ((((S + 127) | S) & 64) >> 6) - 1UL;
            M2 = (S >> 6) - 1UL;
            S &= 63;
            A.High = (N.Low << (int)S) & (~M2);
            A.Low = (N.Low << (int)S) & M2;
            A.High |= ((N.High << (int)S) | ((N.Low >> (64 - (int)S)) & M1)) & M2;

            /*
                S &= 127;

                if(S != 0)
                {
                    if(S > 64)
                    {
                        A.High = N.Low << (S - 64);
                        A.Low = 0;
                    }
                    else if(S < 64)
                    {
                        A.High = (N.High << S) | (N.Low >> (64 - S));
                        A.Low = N.Low << S;
                    }
                    else
                    {
                        A.High = N.Low;
                        A.Low = 0;
                    }
                }
                else
                {
                    A.High = N.High;
                    A.Low = N.Low;
                }
                //*/
        }


        // https://en.wikipedia.org/wiki/C_data_types
        // llu unsigned long long (int) = ULONG
        // http://www.keil.com/forum/16968/default-type-when-only-unsigned-is-stated/
        // unsigned: In general, 'C' assumes that everything is an int unless specifically stated otherwise.
        // typedef unsigned long size_t;
        // The actual type of size_t is platform-dependent; a common mistake is to assume size_t is the same as unsigned int, which can lead to programming errors,2 particularly as 64-bit architectures become more prevalent.
        private static void shiftright128(UInt128 N, uint S, ref UInt128 A)
        {
            ulong M1, M2;
            S &= 127;

            M1 = ((((S + 127) | S) & 64) >> 6) - 1UL;
            M2 = (S >> 6) - 1UL;
            S &= 63;
            A.Low = (N.High >> (int)S) & (~M2);
            A.High = (N.High >> (int)S) & M2;
            A.Low |= ((N.Low >> (int)S) | ((N.High << (64 - (int)S)) & M1)) & M2;

            /*
            S &= 127;

            if(S != 0)
            {
                if(S > 64)
                {
                    A.High = N.High >> (S - 64);
                    A.Low = 0;
                }
                else if(S < 64)
                {
                    A.Low = (N.Low >> S) | (N.High << (64 - S));
                    A.High = N.High >> S;
                }
                else
                {
                    A.Low = N.High;
                    A.High = 0;
                }
            }
            else
            {
                A.High = N.High;
                A.Low = N.Low;
            }
            //*/
        }


        private static void not128(UInt128 N, ref UInt128 A)
        {
            A.High = ~N.High;
            A.Low = ~N.Low;
        }


        private static void or128(UInt128 N1, UInt128 N2, ref UInt128 A)
        {
            A.High = N1.High | N2.High;
            A.Low = N1.Low | N2.Low;
        }


        private static void and128(UInt128 N1, UInt128 N2, ref UInt128 A)
        {
            A.High = N1.High & N2.High;
            A.Low = N1.Low & N2.Low;
        }


        private static void xor128(UInt128 N1, UInt128 N2, ref UInt128 A)
        {
            A.High = N1.High ^ N2.High;
            A.Low = N1.Low ^ N2.Low;
        }



        private static ulong nlz128(UInt128 N)
        {
            return (N.High == 0) ? nlz64(N.Low) + 64 : nlz64(N.High);
        }


        private static ulong nlz64(ulong N)
        {
            ulong I;
            ulong C;

            I = ~N;
            C = ((I ^ (I + 1)) & I) >> 63;

            I = (N >> 32) + 0xffffffff;
            I = ((I & 0x100000000) ^ 0x100000000) >> 27;
            C += I; N <<= (int)I;

            I = (N >> 48) + 0xffff;
            I = ((I & 0x10000) ^ 0x10000) >> 12;
            C += I; N <<= (int)I;

            I = (N >> 56) + 0xff;
            I = ((I & 0x100) ^ 0x100) >> 5;
            C += I; N <<= (int)I;

            I = (N >> 60) + 0xf;
            I = ((I & 0x10) ^ 0x10) >> 2;
            C += I; N <<= (int)I;

            I = (N >> 62) + 3;
            I = ((I & 4) ^ 4) >> 1;
            C += I; N <<= (int)I;

            C += (N >> 63) ^ 1;

            return C;
        }


        private static ulong ntz128(UInt128 N)
        {
            return (N.Low == 0) ? ntz64(N.High) + 64 : ntz64(N.Low);
        }


        private static ulong ntz64(ulong N)
        {
            ulong I = ~N;
            ulong C = ((I ^ (I + 1)) & I) >> 63;

            I = (N & 0xffffffff) + 0xffffffff;
            I = ((I & 0x100000000) ^ 0x100000000) >> 27;
            C += I; N >>= (int)I;

            I = (N & 0xffff) + 0xffff;
            I = ((I & 0x10000) ^ 0x10000) >> 12;
            C += I; N >>= (int)I;

            I = (N & 0xff) + 0xff;
            I = ((I & 0x100) ^ 0x100) >> 5;
            C += I; N >>= (int)I;

            I = (N & 0xf) + 0xf;
            I = ((I & 0x10) ^ 0x10) >> 2;
            C += I; N >>= (int)I;

            I = (N & 3) + 3;
            I = ((I & 4) ^ 4) >> 1;
            C += I; N >>= (int)I;

            C += ((N & 1) ^ 1);

            return C;
        }


        private static ulong popcnt128(UInt128 N)
        {
            return popcnt64(N.High) + popcnt64(N.Low);
        }


        private static ulong popcnt64(ulong V)
        {
            // http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetParallel
            V -= ((V >> 1) & 0x5555555555555555);
            V = (V & 0x3333333333333333) + ((V >> 2) & 0x3333333333333333);
            return ((V + (V >> 4) & 0xF0F0F0F0F0F0F0F) * 0x101010101010101) >> 56;
        }


        public static int compare128(UInt128 N1, UInt128 N2)
        {
            return (((N1.High > N2.High) || ((N1.High == N2.High) && (N1.Low > N2.Low))) ? 1 : 0)
                 - (((N1.High < N2.High) || ((N1.High == N2.High) && (N1.Low < N2.Low))) ? 1 : 0);
        }


        // End Of Bitwise


        // MultSqr
        private static void mult64to128(ulong u, ulong v, ref ulong h, ref ulong l)
        {
            ulong u1 = (u & 0xffffffff);
            ulong v1 = (v & 0xffffffff);
            ulong t = (u1 * v1);
            ulong w3 = (t & 0xffffffff);
            ulong k = (t >> 32);

            u >>= 32;
            t = (u * v1) + k;
            k = (t & 0xffffffff);
            ulong w1 = (t >> 32);

            v >>= 32;
            t = (u1 * v) + k;
            k = (t >> 32);

            h = (u * v) + w1 + k;
            l = (t << 32) + w3;
        }


        private static void mult128(UInt128 N, UInt128 M, ref UInt128 Ans)
        {
            //PRINTVAR(N.High);
            //PRINTVAR(N.Low);
            //PRINTVAR(M.High);
            //PRINTVAR(M.Low);
            mult64to128(N.Low, M.Low, ref Ans.High, ref Ans.Low);
            //PRINTVAR(Ans.High);
            //PRINTVAR(Ans.Low);
            Ans.High += (N.High * M.Low) + (N.Low * M.High);
        }


        private static void mult128to256(UInt128 N, UInt128 M, ref UInt128 H, ref UInt128 L)
        {
            mult64to128(N.High, M.High, ref H.High, ref H.Low);
            mult64to128(N.Low, M.Low, ref L.High, ref L.Low);

            UInt128 T = new UInt128();
            mult64to128(N.High, M.Low, ref T.High, ref T.Low);
            L.High += T.Low;
            if (L.High < T.Low)  // if L.High overflowed
            {
                inc128(H, ref H);
            }
            H.Low += T.High;
            if (H.Low < T.High)  // if H.Low overflowed
            {
                ++H.High;
            }

            mult64to128(N.Low, M.High, ref T.High, ref T.Low);
            L.High += T.Low;
            if (L.High < T.Low)  // if L.High overflowed
            {
                inc128(H, ref H);
            }
            H.Low += T.High;
            if (H.Low < T.High)  // if H.Low overflowed
            {
                ++H.High;
            }
        }


        private static void sqr64to128(ulong r, ref ulong h, ref ulong l)
        {
            ulong r1 = (r & 0xffffffff);
            ulong t = (r1 * r1);
            ulong w3 = (t & 0xffffffff);
            ulong k = (t >> 32);

            r >>= 32;
            ulong m = (r * r1);
            t = m + k;
            ulong w2 = (t & 0xffffffff);
            ulong w1 = (t >> 32);

            t = m + w2;
            k = (t >> 32);
            h = (r * r) + w1 + k;
            l = (t << 32) + w3;
        }


        private static void sqr128(UInt128 R, ref UInt128 Ans)
        {
            sqr64to128(R.Low, ref Ans.High, ref Ans.Low);
            Ans.High += (R.High * R.Low) << 1;
        }


        private static void sqr128to256(UInt128 R, ref UInt128 H, ref UInt128 L)
        {
            sqr64to128(R.High, ref H.High, ref H.Low);
            sqr64to128(R.Low, ref L.High, ref L.Low);

            UInt128 T = new UInt128();
            mult64to128(R.High, R.Low, ref T.High, ref T.Low);

            H.High += (T.High >> 63);
            T.High = (T.High << 1) | (T.Low >> 63);  // Shift Left 1 bit
            T.Low <<= 1;

            L.High += T.Low;
            if (L.High < T.Low)  // if L.High overflowed
            {
                inc128(H, ref H);
            }

            H.Low += T.High;
            if (H.Low < T.High)  // if H.Low overflowed
            {
                ++H.High;
            }
        }



        // divmod 


        private static void div128(UInt128 M, UInt128 N, ref UInt128 Q)
        {
            UInt128 R = new UInt128();
            divmod128(M, N, ref Q, ref R);
        }


        private static void mod128(UInt128 M, UInt128 N, ref UInt128 R)
        {
            UInt128 Q = new UInt128();
            divmod128(M, N, ref Q, ref R);
        }


        private static void divmod128(UInt128 M, UInt128 N, ref UInt128 Q, ref UInt128 R)
        {
            ulong Nlz, Mlz, Ntz;
            int C;

            Nlz = nlz128(N);
            Mlz = nlz128(M);
            Ntz = ntz128(N);

            if (Nlz == 128)
            {
                throw new System.Exception("0");
            }
            else if ((M.High | N.High) == 0)
            {
                Q.High = R.High = 0;
                Q.Low = M.Low / N.Low;
                R.Low = M.Low % N.Low;
                return;
            }
            else if (Nlz == 127)
            {
                Q = M;
                R.High = R.Low = 0;
                return;
            }
            else if ((Ntz + Nlz) == 127)
            {
                shiftright128(M, (uint)Ntz, ref Q);
                dec128(N, ref N);
                and128(N, M, ref R);
                return;
            }

            C = compare128(M, N);
            if (C < 0)
            {
                Q.High = Q.Low = 0;
                R = M;
                return;
            }
            else if (C == 0)
            {
                Q.High = R.High = R.Low = 0;
                Q.Low = 1;
                return;
            }

            if ((Nlz - Mlz) > 5)
            {
                divmod128by128(M, N, ref Q, ref R);
            }
            else
            {
                bindivmod128(M, N, ref Q, ref R);
            }
        }


        private static void divmod128by128(UInt128 M, UInt128 N, ref UInt128 Q, ref UInt128 R)
        {
            if (N.High == 0)
            {
                if (M.High < N.Low)
                {
                    divmod128by64(M.High, M.Low, N.Low, ref Q.Low, ref R.Low);
                    Q.High = 0;
                    R.High = 0;
                    return;
                }
                else
                {
                    Q.High = M.High / N.Low;
                    R.High = M.High % N.Low;
                    divmod128by64(R.High, M.Low, N.Low, ref Q.Low, ref R.Low);
                    R.High = 0;
                    return;
                }
            }
            else
            {
                ulong n = nlz64(N.High);

                UInt128 v1 = new UInt128();
                shiftleft128(N, (uint)n, ref v1);

                UInt128 u1 = new UInt128();
                shiftright128(M, 1, ref u1);

                UInt128 q1 = new UInt128();
                div128by64(u1.High, u1.Low, v1.High, ref q1.Low);
                q1.High = 0;
                shiftright128(q1, (uint)(63 - n), ref q1);

                if ((q1.High | q1.Low) != 0)
                {
                    dec128(q1, ref q1);
                }

                Q.High = q1.High;
                Q.Low = q1.Low;
                mult128(q1, N, ref q1);
                sub128(M, q1, ref R);

                if (compare128(R, N) >= 0)
                {
                    inc128(Q, ref Q);
                    sub128(R, N, ref R);
                }

                return;
            }
        }


        private static void div128by64(ulong u1, ulong u0, ulong v, ref ulong q)
        {
            const ulong b = 1UL << 32;
            ulong un1, un0, vn1, vn0, q1, q0, un32, un21, un10, rhat, vs, left, right;
            ulong s;

            s = nlz64(v);
            vs = v << (int)s;
            vn1 = vs >> 32;
            vn0 = vs & 0xffffffff;


            if (s > 0)
            {
                un32 = (u1 << (int)s) | (u0 >> (64 - (int)s));
                un10 = u0 << (int)s;
            }
            else
            {
                un32 = u1;
                un10 = u0;
            }


            un1 = un10 >> 32;
            un0 = un10 & 0xffffffff;

            q1 = un32 / vn1;
            rhat = un32 % vn1;

            left = q1 * vn0;
            right = (rhat << 32) | un1;

            again1:
            if ((q1 >= b) || (left > right))
            {
                --q1;
                rhat += vn1;
                if (rhat < b)
                {
                    left -= vn0;
                    right = (rhat << 32) | un1;
                    goto again1;
                }
            }

            un21 = (un32 << 32) + (un1 - (q1 * vs));

            q0 = un21 / vn1;
            rhat = un21 % vn1;

            left = q0 * vn0;
            right = (rhat << 32) | un0;
            again2:
            if ((q0 >= b) || (left > right))
            {
                --q0;
                rhat += vn1;
                if (rhat < b)
                {
                    left -= vn0;
                    right = (rhat << 32) | un0;
                    goto again2;
                }
            }

            q = (q1 << 32) | q0;
        }


        private static void divmod128by64(ulong u1, ulong u0, ulong v, ref ulong q, ref ulong r)
        {
            const ulong b = 1UL << 32;
            ulong un1, un0, vn1, vn0, q1, q0, un32, un21, un10, rhat, left, right;
            ulong s;

            s = nlz64(v);
            v <<= (int)s;
            vn1 = v >> 32;
            vn0 = v & 0xffffffff;

            if (s > 0)
            {
                un32 = (u1 << (int)s) | (u0 >> (64 - (int)s));
                un10 = u0 << (int)s;
            }
            else
            {
                un32 = u1;
                un10 = u0;
            }

            un1 = un10 >> 32;
            un0 = un10 & 0xffffffff;

            q1 = un32 / vn1;
            rhat = un32 % vn1;

            left = q1 * vn0;
            right = (rhat << 32) + un1;
            again1:
            if ((q1 >= b) || (left > right))
            {
                --q1;
                rhat += vn1;
                if (rhat < b)
                {
                    left -= vn0;
                    right = (rhat << 32) | un1;
                    goto again1;
                }
            }

            un21 = (un32 << 32) + (un1 - (q1 * v));

            q0 = un21 / vn1;
            rhat = un21 % vn1;

            left = q0 * vn0;
            right = (rhat << 32) | un0;
            again2:
            if ((q0 >= b) || (left > right))
            {
                --q0;
                rhat += vn1;
                if (rhat < b)
                {
                    left -= vn0;
                    right = (rhat << 32) | un0;
                    goto again2;
                }
            }

            r = ((un21 << 32) + (un0 - (q0 * v))) >> (int)s;
            q = (q1 << 32) | q0;
        }


        private static void bindivmod128(UInt128 M, UInt128 N, ref UInt128 Q, ref UInt128 R)
        {
            Q.High = Q.Low = 0;
            ulong Shift = nlz128(N) - nlz128(M);
            shiftleft128(N, (uint)Shift, ref N);

            do
            {
                shiftleft128(Q, 1, ref Q);
                if (compare128(M, N) >= 0)
                {
                    sub128(M, N, ref M);
                    Q.Low |= 1;
                }

                shiftright128(N, 1, ref N);
            } while (Shift-- != 0);

            R = M;
        }


    }


}
