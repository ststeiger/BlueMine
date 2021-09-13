
using System;
using System.Collections.Generic;


namespace BlueMine.Code
{

    public class Range<T> 
        : System.IComparable<Range<T>>
        where T : System.IComparable
    {
        public T From;
        public T To;


        public Range<T> Lower;
        public Range<T> Higher;
        


        public Range(T point)
        {
            From = point;
            To = point;
        }

        public Range(T from, T to)
        {
            From = from;
            To = to;
        }

        public int CompareTo(Range<T> other)
        {
            return From.CompareTo(other.From);
        }

        //int IComparable<Range<T>>.CompareTo(Range<T> other)
        //{

        //    return From.CompareTo(other.From);
        //}
    }


    public class BetterTree
    {
    }
}
