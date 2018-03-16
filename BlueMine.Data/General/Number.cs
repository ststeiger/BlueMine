
namespace BlueMine.Data
{


    public static class Number
    {


        private static object GetConstValue(System.Type t, string propertyName)
        {
            System.Reflection.FieldInfo pi = t.GetField(propertyName, System.Reflection.BindingFlags.Static
                | System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.NonPublic
                );

            return pi.GetValue(null);
        } // End Function GetConstValue 


        private static object GetMinValue(System.Type t)
        {
            return GetConstValue(t, "MinValue");
        } // End Function GetMinValue 


        private static object GetMaxValue(System.Type t)
        {
            return GetConstValue(t, "MaxValue");
        } // End Function GetMaxValue 



        private static object UnsignedToSigned(object value, System.Type t)
        {
            if (object.ReferenceEquals(t, typeof(System.UInt64)))
                return UnsignedToSigned((System.UInt64)value);
            else if (object.ReferenceEquals(t, typeof(System.UInt32)))
                return UnsignedToSigned((System.UInt32)value);
            else if (object.ReferenceEquals(t, typeof(System.UInt16)))
                return UnsignedToSigned((System.UInt16)value);
            else if (object.ReferenceEquals(t, typeof(System.Byte)))
                return UnsignedToSigned((System.Byte)value);

            throw new System.NotImplementedException($"UnsignedToSigned for type {t.Name} is not implemented.");
        }


        public static object UnsignedToSigned(object value)
        {
            if (value == null)
                throw new System.ArgumentNullException(nameof(value), "Parameter cannot be NULL.");

            System.Type t = value.GetType();

            return UnsignedToSigned(value, t);
        }


        public static T UnsignedToSigned<T>(object value)
            where T:System.IComparable 
        {
            if (value == null)
                throw new System.ArgumentNullException(nameof(value), "Parameter cannot be NULL.");

            System.Type t = value.GetType();
            System.Type tRet = typeof(T);

            int sizeRet = System.Runtime.InteropServices.Marshal.SizeOf(tRet);
            int sizeValue = System.Runtime.InteropServices.Marshal.SizeOf(t);

            if (sizeRet != sizeValue)
            {
                throw new System.NotSupportedException($"Type mismatch: {tRet.Name} is not the matching signed type for {t.Name}");
            }

            System.IComparable minValue = (System.IComparable)GetMinValue(t);
            
            System.IComparable minValueRet = (System.IComparable)GetMinValue(tRet);
            if (minValueRet.CompareTo(System.Convert.ChangeType(0, tRet)) == 0)
            {
                throw new System.NotSupportedException($"Type mismatch: {tRet.Name} is not a signed type.");
            }

            // If we already have an signed type
            // Type mismatch already prevented 
            if (minValue.CompareTo(System.Convert.ChangeType(0, t)) != 0)
            {
                return (T)value;
            }

            return (T)UnsignedToSigned(value, t);
        }

        private static object SignedToUnsigned(object value, System.Type t)
        {
            if (object.ReferenceEquals(t, typeof(System.Int64)))
                return SignedToUnsigned((System.Int64)value);
            else if (object.ReferenceEquals(t, typeof(System.Int32)))
                return SignedToUnsigned((System.Int32)value);
            else if (object.ReferenceEquals(t, typeof(System.Int16)))
                return SignedToUnsigned((System.Int16)value);
            else if (object.ReferenceEquals(t, typeof(System.SByte)))
                return SignedToUnsigned((System.SByte)value);

            throw new System.NotImplementedException("SignedToUnsigned for type " + t.FullName + " is not implemented.");
        }


        public static object SignedToUnsigned(object value)
        {
            if (value == null)
                throw new System.ArgumentNullException(nameof(value), "Parameter cannot be NULL.");

            System.Type t = value.GetType();
            return SignedToUnsigned(value, t);
        }


        public static T SignedToUnsigned<T>(object value)
            where T : System.IComparable
        {
            if (value == null)
                throw new System.ArgumentNullException(nameof(value), "Parameter cannot be NULL.");

            System.Type t = value.GetType();
            System.Type tRet = typeof(T);

            int sizeRet = System.Runtime.InteropServices.Marshal.SizeOf(tRet);
            int sizeValue = System.Runtime.InteropServices.Marshal.SizeOf(t);

            if (sizeRet != sizeValue)
            {
                throw new System.NotSupportedException($"Type mismatch: {tRet.Name} is not the matching unsigned type for {t.Name}");
            }

            System.IComparable minValue = (System.IComparable)GetMinValue(t);
            
            System.IComparable minValueRet = (System.IComparable)GetMinValue(tRet);
            if (minValueRet.CompareTo(System.Convert.ChangeType(0, tRet)) != 0)
            {
                throw new System.NotSupportedException($"Type mismatch: {tRet.Name} is not an unsigned type.");
            }

            // If we already have an unsigned type
            // Type mismatch already prevented 
            if (minValue.CompareTo(System.Convert.ChangeType(0, t)) == 0)
            {
                return (T)value;
            }

            return (T)SignedToUnsigned(value, t);
        }


        public static System.Int64 UnsignedToSigned(System.UInt64 uintValue)
        {
            return unchecked((System.Int64)uintValue + System.Int64.MinValue);
        }


        public static System.UInt64 SignedToUnsigned(System.Int64 intValue)
        {
            return unchecked((System.UInt64)(intValue - System.Int64.MinValue));
        }


        public static System.Int32 UnsignedToSigned(System.UInt32 uintValue)
        {
            return unchecked((System.Int32)uintValue + System.Int32.MinValue);
        }


        public static System.UInt32 SignedToUnsigned(System.Int32 intValue)
        {
            return unchecked((System.UInt32)(intValue - System.Int32.MinValue));
        }



        public static System.Int16 UnsignedToSigned(System.UInt16 uintValue)
        {
            return (System.Int16) unchecked((System.Int16)uintValue + System.Int16.MinValue);
        }


        public static System.UInt16 SignedToUnsigned(System.Int16 intValue)
        {
            return unchecked((System.UInt16)(intValue - System.Int16.MinValue));
        }



        public static sbyte UnsignedToSigned(byte ulongValue)
        {
            return (sbyte) unchecked((sbyte)ulongValue + sbyte.MinValue);
        }


        public static byte SignedToUnsigned(sbyte longValue)
        {
            return unchecked((byte)(longValue - sbyte.MinValue));
        }

    }



    public static class Number<T>
        where T:System.IComparable
    {

        private static object GetConstValue(System.Type t, string propertyName)
        {
            System.Reflection.FieldInfo pi = t.GetField(propertyName, System.Reflection.BindingFlags.Static
                | System.Reflection.BindingFlags.Public
                | System.Reflection.BindingFlags.NonPublic
                );

            return pi.GetValue(null);
        } // End Function GetConstValue 


        private static T GetMinValue<T>()
        {
            return (T)GetConstValue(typeof(T), "MinValue");
        } // End Function GetMinValue 


        private static T GetMaxValue<T>()
        {
            return (T)GetConstValue(typeof(T), "MaxValue");
        } // End Function GetMaxValue 


        private static System.Func<T1, T1, T1> CompileAdd<T1>()
        {
            // Declare the parameters
            System.Linq.Expressions.ParameterExpression paramA =
                System.Linq.Expressions.Expression.Parameter(typeof(T1), "a");

            System.Linq.Expressions.ParameterExpression paramB =
                System.Linq.Expressions.Expression.Parameter(typeof(T1), "b");

            // Add the parameters
            System.Linq.Expressions.BinaryExpression body =
                System.Linq.Expressions.Expression.Add(paramA, paramB);

            // Compile it
            System.Func<T1, T1, T1> add =
                System.Linq.Expressions.Expression.Lambda<System.Func<T1, T1, T1>>
                (body, paramA, paramB).Compile();

            return add;
        } // End Function CompileAdd 


        private static System.Func<T1, T1, T1> CompileSubtract<T1>()
        {
            // Declare the parameters
            System.Linq.Expressions.ParameterExpression paramA =
                System.Linq.Expressions.Expression.Parameter(typeof(T1), "a");

            System.Linq.Expressions.ParameterExpression paramB =
                System.Linq.Expressions.Expression.Parameter(typeof(T1), "b");

            // Subtract the parameters
            System.Linq.Expressions.BinaryExpression body =
                System.Linq.Expressions.Expression.Subtract(paramA, paramB);

            // Compile it
            System.Func<T1, T1, T1> subtract =
                System.Linq.Expressions.Expression.Lambda<System.Func<T1, T1, T1>>
                (body, paramA, paramB).Compile();

            return subtract;
        } // End Function CompileSubtract 


        public readonly static T MinValue = GetMinValue<T>();
        public readonly static T MaxValue = GetMaxValue<T>();
        public readonly static System.Func<T, T, T> Add = CompileAdd<T>();
        public readonly static System.Func<T, T, T> Subtract = CompileSubtract<T>();


        private static System.Collections.Generic.Dictionary<System.Type, System.Type>
            TypeMapSignedToUnsigned()
        {
            System.Collections.Generic.Dictionary<System.Type, System.Type> dict
                = new System.Collections.Generic.Dictionary<System.Type, System.Type>();

            dict.Add(typeof(System.SByte), typeof(System.Byte));
            dict.Add(typeof(System.Int16), typeof(System.UInt16));
            dict.Add(typeof(System.Int32), typeof(System.UInt32));
            dict.Add(typeof(System.Int64), typeof(System.UInt64));

            return dict;
        }


        private static System.Collections.Generic.Dictionary<System.Type, System.Type>
            TypeMapUnsignedToSigned()
        {
            System.Collections.Generic.Dictionary<System.Type, System.Type> dict
                = new System.Collections.Generic.Dictionary<System.Type, System.Type>();

            dict.Add(typeof(System.Byte), typeof(System.SByte));
            dict.Add(typeof(System.UInt16), typeof(System.Int16));
            dict.Add(typeof(System.UInt32), typeof(System.Int32));
            dict.Add(typeof(System.UInt64), typeof(System.Int64));

            return dict;
        }


        private static System.Collections.Generic.Dictionary<System.Type, System.Type> MapSignedUnsigned
                = TypeMapSignedToUnsigned();

        private static System.Collections.Generic.Dictionary<System.Type, System.Type> MapUnsignedSigned
                = TypeMapUnsignedToSigned();
        
        public static bool IsSigned = MinValue.CompareTo(default(T)) == 0 ? false : true;
        public static bool IsUnsigned = MinValue.CompareTo(default(T)) == 0 ? true : false;

        
        public static System.Type SignedType
        {
            get
            {
                if (IsSigned)
                    return typeof(T);

                return MapUnsignedSigned[typeof(T)];
            }
        }

        public static System.Type UnsignedType
        {
            get
            {
                if (IsUnsigned)
                    return typeof(T);

                return MapSignedUnsigned[typeof(T)];
            }
        }


        public static T2 ToUnsigned<T2>(T input)
            where T2: System.IComparable 
        {
            return Number.SignedToUnsigned<T2>(input);


            if (IsUnsigned)
                return (T2) (object) input;

            // T is Signed 
            // t is unsigned type for T 
            System.Type t = MapSignedUnsigned[typeof(T)];

            // TUnsigned SignedToUnsigned<TSigned, TUnsigned>(TSigned longValue)
            // return SignedToUnsigned<T, t> (input);
            System.Reflection.MethodInfo method = typeof(Number<T>).GetMethod("SignedToUnsigned"
                , System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic
            );

            System.Reflection.MethodInfo genericMethod = method.MakeGenericMethod(typeof(T), t);

            return (T2) genericMethod.Invoke(null, new object[] { input });
        }


        public static T2 ToSigned<T2>(T input)
            where T2 : System.IComparable
        {
            return Number.UnsignedToSigned<T2>(input);



            if (IsSigned)
                return (T2) (object) input;

            // T is Unsigned 
            // t is signed type for T 
            System.Type t = MapUnsignedSigned[typeof(T)];
            // TSigned UnsignedToSigned<TUnsigned, TSigned>(TUnsigned ulongValue)
            // return UnsignedToSigned<T, t> (input);
            System.Reflection.MethodInfo method = typeof(Number<T>).GetMethod("UnsignedToSigned"
                , System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic
            );

            System.Reflection.MethodInfo genericMethod = method.MakeGenericMethod(typeof(T), t);

            return (T2)genericMethod.Invoke(null, new object[] { input });
        }


        private static TSigned UnsignedToSigned<TUnsigned, TSigned>(TUnsigned ulongValue)
            where TUnsigned: System.IComparable
            where TSigned: System.IComparable
        {
            TSigned signed = default(TSigned);
            unchecked
            {
                signed = Number<TSigned>.Add((TSigned)(dynamic)ulongValue, Number<TSigned>.MinValue);
            }

            return signed;
        } // End Function UnsignedToSigned 


        private static TUnsigned SignedToUnsigned<TSigned, TUnsigned>(TSigned longValue)
            where TUnsigned : System.IComparable
            where TSigned : System.IComparable
        {
            TUnsigned unsigned = default(TUnsigned);
            unchecked
            {
                unsigned = (TUnsigned)(dynamic)Number<TSigned>
                    .Subtract(longValue, Number<TSigned>.MinValue);
            }

            return unsigned;
        } // End Function SignedToUnsigned 


    }


}
