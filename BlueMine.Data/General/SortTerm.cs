
namespace BlueMine.Data
{


    public enum SortDirection : int 
    {
        Ascending = 1,
        Descending = -1
    };


    public class SortExpression<T>
    {
        public System.Linq.Expressions.Expression<System.Func<T, System.IComparable>> Sort;
        public SortDirection Direction;
        

        public SortExpression(System.Linq.Expressions.Expression<System.Func<T, System.IComparable>> sorter, SortDirection direction)
        {
            this.Sort = sorter;
            this.Direction = direction;
        } // End Constructor 


        public SortExpression(System.Linq.Expressions.Expression<System.Func<T, System.IComparable>> sorter)
            : this(sorter, SortDirection.Ascending)
        { } // End Constructor 


        public static SortExpression<T> Create<TKey>(System.Func<T, TKey> sorter, SortDirection direction)
        {
            System.Linq.Expressions.Expression<System.Func<T, System.IComparable>> expr
                = x => (System.IComparable)sorter(x);

            return new SortExpression<T>(expr, direction);
        } // End Function Create 
        

        public static SortExpression<T> Create<TKey>(System.Func<T, TKey> sorter)
        {
            return Create<TKey>(sorter, SortDirection.Ascending);
        } // End Function Create 

    }


    public class SortTerm<T> 
    {

        public System.Func<T, System.IComparable> Sort;
        public SortDirection Direction;


        public SortTerm(System.Func<T, System.IComparable> sorter, SortDirection direction)
        {
            this.Sort = sorter;
            this.Direction = direction;
        } // End Constructor 


        public SortTerm(System.Func<T, System.IComparable> sorter)
            : this(sorter, SortDirection.Ascending)
        { } // End Constructor 


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

        } // End Function Create 


        public static SortTerm<T> Create<TKey>(System.Func<T, TKey> sorter)
            // where TKey: System.IComparable
            // where TKey: struct, System.IComparable
        {
            return Create<TKey>(sorter, SortDirection.Ascending);
        } // End Function Create 


    } // End Class SortTerm<T>  


} // End Namespace BlueMine.Models.Project
