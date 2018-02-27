
namespace BlueMine.Data
{


    public enum SortDirection : int 
    {
        Ascending = 1,
        Descending = -1
    };


    public class SortTerm<T> 
    {

        public System.Func<T, System.IComparable> Sort;
        public SortDirection Direction;


        public SortTerm(System.Func<T, System.IComparable> sorter, SortDirection direction)
        {
            this.Sort = sorter;
            this.Direction = direction;
        }


        public SortTerm(System.Func<T, System.IComparable> sorter)
            : this(sorter, SortDirection.Ascending)
        { }


        public static SortTerm<T> Create<TKey>(System.Func<T, TKey> sorter, SortDirection direction)
            // where TKey: System.IComparable
            // where TKey: struct, System.IComparable
        {
            // InvalidCastException: Unable to cast object of type 
            // 'System.Func2[Db.T_projects,System.Int64]' to type 
            // 'System.Func2[Db.T_projects,System.IComparable]'.
            // What you try is a co-variant conversion.
            // This is not supported for value - types(like Int64). 
            // string is a reference type, so it works.

            // return new SortTerm<T>((System.Func<T, System.IComparable>)(object)sorter, direction);

            return new SortTerm<T>(
                delegate (T x) 
                {
                    TKey ret = sorter(x);
                    
                    return (System.IComparable)ret;
                }, direction
            );

        } // End Constructor 


        public static SortTerm<T> Create<TKey>(System.Func<T, TKey> sorter)
            // where TKey: System.IComparable
            // where TKey: struct, System.IComparable
        {
            return Create<TKey>(sorter, SortDirection.Ascending);
        } // End Constructor 


    } // End Class SortTerm<T>  


} // End Namespace BlueMine.Models.Project
