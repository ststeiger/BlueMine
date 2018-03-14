
// https://www.codeproject.com/Tips/785014/UInt-Division-Modulus

namespace iCaramba
{

    public class TestClass 
    {


        public static void Test()
        {
            System.Collections.Generic.List<System.Guid> ls = new System.Collections.Generic.List<System.Guid>();
            for(int i = 0; i < 100; ++i)
                ls.Add(System.Guid.NewGuid());

            ls.Sort(Compare);
        }
        

        public static int Compare(System.Guid x, System.Guid y)
        {
            const int NUM_BYTES_IN_GUID = 16;
            byte byte1, byte2;

            byte[] xBytes = new byte[NUM_BYTES_IN_GUID];
            byte[] yBytes = new byte[NUM_BYTES_IN_GUID];

            x.ToByteArray().CopyTo(xBytes, 0);
            y.ToByteArray().CopyTo(yBytes, 0);

            int[] byteOrder = new int[16] // 16 Bytes = 128 Bit 
                {10, 11, 12, 13, 14, 15, 8, 9, 6, 7, 4, 5, 0, 1, 2, 3};


            //Swap to the correct order to be compared
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
                }
            } // Next i 

            xBytes = null;
            yBytes = null;
            byteOrder = null;
            return 0;
        }

    }


    public class SqlGuid
        : System.IComparable
        , System.IComparable<SqlGuid>
        , System.Collections.Generic.IComparer<SqlGuid>
        , System.IEquatable<SqlGuid>
    {
        private const int NUM_BYTES_IN_GUID = 16;

        // Comparison orders.
        private static readonly int[] m_byteOrder = new int[16] // 16 Bytes = 128 Bit 
        {10, 11, 12, 13, 14, 15, 8, 9, 6, 7, 4, 5, 0, 1, 2, 3};

        private byte[] m_bytes; // the SqlGuid is null if m_value is null


        public SqlGuid(byte[] guidBytes)
        {
            if (guidBytes == null || guidBytes.Length != NUM_BYTES_IN_GUID)
                throw new System.ArgumentException("Invalid array size");

            m_bytes = new byte[NUM_BYTES_IN_GUID];
            guidBytes.CopyTo(m_bytes, 0);
        }
        

        public SqlGuid(System.Guid g)
        {
            m_bytes = g.ToByteArray();
        }


        public byte[] ToByteArray()
        {
            byte[] ret = new byte[NUM_BYTES_IN_GUID];
            m_bytes.CopyTo(ret, 0);
            return ret;
        }

        int CompareTo(object obj)
        {
            if (obj == null)
                return 1; // https://msdn.microsoft.com/en-us/library/system.icomparable.compareto(v=vs.110).aspx

            System.Type t = obj.GetType();

            if (object.ReferenceEquals(t, typeof(System.DBNull)))
                return 1;

            if (object.ReferenceEquals(t, typeof(SqlGuid)))
            {
                SqlGuid ui = (SqlGuid)obj;
                return this.Compare(this, ui);
            } // End if (object.ReferenceEquals(t, typeof(UInt128)))

            return 1;
        } // End Function CompareTo(object obj)


        int System.IComparable.CompareTo(object obj)
        {
            return this.CompareTo(obj);
        }


        int CompareTo(SqlGuid other)
        {
            return this.Compare(this, other);
        }


        int System.IComparable<SqlGuid>.CompareTo(SqlGuid other)
        {
            return this.Compare(this, other);
        }
        

        enum EComparison : int
        {
            LT = -1, // itemA precedes itemB in the sort order.
            EQ = 0, // itemA occurs in the same position as itemB in the sort order.
            GT = 1 // itemA follows itemB in the sort order.
        }


        public int Compare(SqlGuid x, SqlGuid y)
        {
            byte byte1, byte2;

            //Swap to the correct order to be compared
            for (int i = 0; i < NUM_BYTES_IN_GUID; i++)
            {
                byte1 = x.m_bytes[m_byteOrder[i]];
                byte2 = y.m_bytes[m_byteOrder[i]];
                if (byte1 != byte2)
                    return (byte1 < byte2) ?  (int) EComparison.LT : (int) EComparison.GT;
            } // Next i 

            return (int) EComparison.EQ;
        }
        

        int System.Collections.Generic.IComparer<SqlGuid>.Compare(SqlGuid x, SqlGuid y)
        {
            return this.Compare(x, y);
        }


        public bool Equals(SqlGuid other)
        {
            return Compare(this, other) == 0;
        }


        bool System.IEquatable<SqlGuid>.Equals(SqlGuid other)
        {
            return this.Equals(other);
        }


    }



    public class UInt128
        : System.IComparable
        , System.IComparable<UInt128>
        , System.Collections.Generic.IComparer<UInt128>
        , System.IEquatable<UInt128>
    {
        
        private ulong Low;
        private ulong High;
        

        public UInt128(ulong high, ulong low)
        {
            this.Low = low;
            this.High = high;
        } // End Constructor 


        public UInt128(UInt128 number)
            :this(number.High, number.Low)
        { } // End Constructor 


        public UInt128(ulong low)
            : this(0, low)
        { } //


        public UInt128(uint low)
            : this(0, low)
        { } //End Constructor 

        public UInt128()
            : this(0, 0)
        { } // End Constructor 

        // https://github.com/eteran/cpp-utilities/blob/master/uint128.h
        public UInt128(string sz, uint radix) 
            : this(0, 0)
        {

            // do we have at least one character?
            if (sz != string.Empty)
            {
                bool minus = false;

                // auto i = sz.begin();
                int i = 0;

                // check for minus sign, i suppose technically this should only apply
                // to base 10, but who says that -0x1 should be invalid?
                if (sz[i] == '-')
                {
                    ++i;
                    minus = true;
                }

                // check if there is radix changing prefix (0 or 0x)
                //if(i != sz.end()) {
                if (i != sz.Length)
                {
                    if (sz[i] == '0')
                    {
                        radix = 8;
                        ++i;
                        //if(i != sz.end()) {
                        if (i != sz.Length)
                        {
                            if (sz[i] == 'x')
                            {
                                radix = 16;
                                ++i;
                            }
                        }
                    }

                    //while(i != sz.end()) {
                    while (i != sz.Length)
                    {
                        uint n;
                        char ch = sz[i];

                        if (ch >= 'A' && ch <= 'Z')
                        {
                            if (((ch - 'A') + 10) < radix)
                            {
                                n = ((uint)ch - 'A') + 10;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else if (ch >= 'a' && ch <= 'z')
                        {
                            if (((ch - 'a') + 10) < radix)
                            {
                                n = ((uint)ch - 'a') + 10;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else if (ch >= '0' && ch <= '9')
                        {
                            if ((ch - '0') < radix)
                            {
                                n = ((uint)ch - '0');
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            // completely invalid character
                            break;
                        }

                        // // (*this) *= radix;
                        // this = this * (UInt128)radix;
                        UInt128 copy1 = this * (UInt128)radix;
                        this.Low = copy1.Low;
                        this.High = copy1.High;
                        
                        // //(*this) += n;
                        // this = this + n;
                        UInt128 copy2 = this * n;
                        this.Low = copy2.Low;
                        this.High = copy2.High;

                        ++i;
                    }
                }

                // if this was a negative number, do that two's compliment madness :-P
                if (minus)
                {
                    //*this = -*this;
                    // this = -this;
                    throw new System.Exception("UInt doesn't allow negative numbers.");
                }
            }

        } // End UInt128 - constructor 


        public UInt128(string sz)
            : this(sz, 10)
        { }


        public static UInt128 MaxValue
        {
            get
            {
                return new UInt128(ulong.MaxValue, ulong.MaxValue);
            }
        }


        public static UInt128 MinValue
        {
            get
            {
                return new UInt128(ulong.MinValue, ulong.MinValue);
            }
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



        public string ToGuidString(bool ascending)
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
                    }

                    result[i] = latinBase[(int)(num % @base)];
                    num = num / @base;
                }
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
                    }

                    if(count < hexString.Length)
                        result[i] = hexString[count];
                    else
                        result[i] = '0';

                    count++;
                }

                
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
            return retValue;
        }

        public string ToGuidString()
        {
            return this.ToGuidString(true);
        }


        public System.Guid ToGuid(bool ascending)
        {
            return new System.Guid(this.ToGuidString(ascending));
        }


        public System.Guid ToGuid()
        {
            return this.ToGuid(true);
        }



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

            UInt128 compareTarget = new UInt128(0, lowerPart.Value);
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

        // ?
        public static UInt128 Square(UInt128 R)
        {
            UInt128 Ans = new UInt128();

            sqr128(R, ref Ans);
            return Ans;
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
