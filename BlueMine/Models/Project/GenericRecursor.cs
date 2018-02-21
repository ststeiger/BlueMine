
namespace BlueMine.Models.Project
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

    }



    // Definitely not like this: https://martinsdevblog.blogspot.ch/2013/05/how-to-display-recursive-tree-structure.html
    // or this https://stackoverflow.com/questions/6422895/asp-net-mvc-3-razor-recursive-function 
    // Instead, use DisplayTemplates
    public class GenericRecursor<T, TRet>
    {
        
        public delegate TRet GetIdentifierValue_t(T f);
        
        
        public T Me;
        protected System.Collections.Generic.List<T> m_list;
        protected System.Collections.Generic.List<SortTerm<T>> m_sorts;

        public int Level;
        
        
        private GetIdentifierValue_t  m_parentId_func;
        private GetIdentifierValue_t  m_id_func;
        
        
        public GenericRecursor(System.Collections.Generic.List<T> list
            ,GetIdentifierValue_t parentId
            ,GetIdentifierValue_t id  
        )
            :this(default(T), list, parentId, id, new System.Collections.Generic.List<SortTerm<T>>(), - 1)
        { } // End Constructor 
        
        
        public GenericRecursor(T me
            , System.Collections.Generic.List<T> list
            , GetIdentifierValue_t parentId
            , GetIdentifierValue_t id
            , System.Collections.Generic.List<SortTerm<T>> sorts
            , int level)
        {
            this.Me = me;
            this.m_list = list;
            this.Level = ++level;
            
            this.m_parentId_func = parentId;
            this.m_id_func = id;
            this.m_sorts = sorts;
        } // End Constructor 


        public void AddSort<TKey>(System.Func<T, TKey> sorter, SortDirection direction)
            // where TKey: System.IComparable
            // where TKey: struct, System.IComparable
        {
            this.m_sorts.Add(SortTerm<T>.Create<TKey>(sorter, direction));
        }


        public void AddSort<TKey>(System.Func<T, TKey> sorter)
            // where TKey: System.IComparable
            // where TKey: struct, System.IComparable
        {
            this.AddSort<TKey>(sorter, SortDirection.Ascending);
        }


        public void AddSort(params SortTerm<T>[] sorts)
        {
            this.m_sorts.AddRange(sorts);
        }


        private int ProjectSorter(
              GenericRecursor<T, TRet> x
            , GenericRecursor<T, TRet> y, int i)
        {
            SortDirection direction = this.m_sorts[i].Direction;

            if (x == null && y == null)
                return 0;

            if (x == null || y == null)
            {
                // return (int)direction * (x == null ? -1 : 1); // NULL-Values at top when ASC, bottom when DESC
                // return (int)direction * (x == null ? 1 : -1); // NULL-Values at bottom when ASC, top when DESC

                // return (x == null ? -1 : 1); // NULL-Values at top, indep. of search dir
                return (x == null ? 1 : -1); // NULL-Values at bottom, indep. of search dir
            } // End if (x == null || y == null) 


            if (x.Me == null && y.Me == null)
                return 0;

            if (x.Me == null || y.Me == null)
            {
                // return (int)direction * (x.Me == null ? -1 : 1); // NULL-Values at top when ASC, bottom when DESC
                // return (int)direction * (x.Me == null ? 1 : -1); // NULL-Values at bottom when ASC, top when DESC

                // return (x.Me == null ? -1 : 1); // NULL-Values at top, indep. of search dir
                return (x.Me == null ? 1 : -1); // NULL-Values at bottom, indep. of search dir
            } // End if (x.Me == null || y.Me == null) 

            System.IComparable a = this.m_sorts[i].Sort(x.Me);
            System.IComparable b = this.m_sorts[i].Sort(y.Me);

            if (a == null && b == null)
                return 0;

            if (a == null || b == null)
            {
                // return (int)direction * (a == null ? -1 : 1); // NULL-Values at top when ASC, bottom when DESC
                // return (int)direction * (a == null ? 1 : -1); // NULL-Values at bottom when ASC, top when DESC

                // return (a == null ? -1 : 1); // NULL-Values at top, indep. of search dir
                return (a == null ? 1 : -1); // NULL-Values at bottom, indep. of search dir
            } // End if (a == null || b == null) 

            return (int)direction * a.CompareTo(b);
        } // End Function ProjectSorter 
        
        
        public System.Collections.Generic.List<GenericRecursor<T, TRet>> RootNodes
        {
            get
            {
                System.Collections.Generic.List<GenericRecursor<T, TRet>> ls = 
                    new System.Collections.Generic.List<GenericRecursor<T, TRet>>();
                
                for (int i = 0; i < this.m_list.Count; ++i)
                {
                    if( object.Equals( this.m_parentId_func(this.m_list[i]), null))
                        ls.Add(new GenericRecursor<T, TRet>(this.m_list[i], this.m_list
                            ,this.m_parentId_func
                            ,this.m_id_func
                            ,this.m_sorts
                            , this.Level));
                } // Next i 

                for (int i = this.m_sorts.Count - 1; i > -1; --i)
                {

                    ls.Sort(delegate (GenericRecursor<T, TRet> x, GenericRecursor<T, TRet> y)
                    {
                        return this.ProjectSorter(x, y, i);
                    });

                } // Next i 

                return ls;
            } // End Get 
            
        } // End Property RootNodes 
        
        
        public System.Collections.Generic.List<GenericRecursor<T, TRet>> ChildNodes
        {
            get
            {
                if (this.Me == null)
                    return null;

                System.Collections.Generic.List<GenericRecursor<T, TRet>> ls = 
                    new System.Collections.Generic.List<GenericRecursor<T, TRet>>();
                for (int i = 0; i < this.m_list.Count; ++i)
                {
                    
                    if(object.Equals(
                        this.m_parentId_func(this.m_list[i])
                        ,this.m_id_func(this.Me)
                        ))
                        ls.Add(new GenericRecursor<T, TRet>(
                            this.m_list[i], this.m_list
                            ,this.m_parentId_func
                            ,this.m_id_func
                            , this.m_sorts
                            , this.Level));
                } // Next i 

                for (int i = this.m_sorts.Count - 1; i > -1; --i)
                {

                    ls.Sort(delegate (GenericRecursor<T, TRet> x, GenericRecursor<T, TRet> y)
                    {
                        return this.ProjectSorter(x, y, i);
                    });

                } // Next i 

                return ls;
            } // End Get 
            
        } // End Property ChildNodes 
        
        
    } // End Class ProjectRecursor 
    
    
} // End Namespace BlueMine.Models.Project
