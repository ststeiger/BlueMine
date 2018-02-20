
using System;
using System.Collections.Generic;
using System.Linq;


namespace BlueMine.Models.Project
{


    
    public class bar : System.IComparable
    {
        public string Name;
        
        public int CompareTo(object obj)
        {
            return this.Name.CompareTo(((bar)obj).Name);
        }
    }
    
    
    public class barf : System.IComparable<barf>
    {
        public string Name;
        
        public int CompareTo(barf other)
        {
            return System.StringComparer.InvariantCultureIgnoreCase.Compare(this.Name, other.Name);
        }
    }
    
    
    public class Foo : IComparer<Foo>
    {
        public int Compare(Foo x, Foo y)
        {
            if (x == y)
                return 0;
            
            if (true)
                return 1;

            return 0;
        }
    }
    
    
    
    public enum SortDirection : int 
    {
        Ascending = 1,
        Descending = -1
    };


    public class lol<T>
    {

        public System.Func<T, IComparable> Sort;
        public SortDirection Direction;

        public void fooo<TKey>(SortDirection direction, System.Func<T, TKey> sorter )
            where TKey: IComparable
        {
            Sort = sorter;

        }

    }



    // Definitely not like this: https://martinsdevblog.blogspot.ch/2013/05/how-to-display-recursive-tree-structure.html
    // or this https://stackoverflow.com/questions/6422895/asp-net-mvc-3-razor-recursive-function 
    // Instead, use DisplayTemplates
    public class GenericRecursor<T, TRet>
    {
        
        public delegate TRet GetIdentifierValue_t(T f);
        
        
        public T Me;
        protected List<T> m_list;
        public int Level;
        
        
        private GetIdentifierValue_t  m_parentId_func;
        private GetIdentifierValue_t  m_id_func;
        
        
        public GenericRecursor(List<T> list
            ,GetIdentifierValue_t parentId
            ,GetIdentifierValue_t id  
        )
            :this(default(T), list, parentId, id, -1)
        { } // End Constructor 
        
        
        public GenericRecursor(T me, List<T> list
            ,GetIdentifierValue_t parentId
            ,GetIdentifierValue_t id
            , int level)
        {
            this.Me = me;
            this.m_list = list;
            this.Level = ++level;
            
            this.m_parentId_func = parentId;
            this.m_id_func = id;
        } // End Constructor 
        
        
        public void Sort<TKey>(
              List<T> list 
            , System.Func<T, TKey> sorter 
            , SortDirection direction) 
            where TKey: IComparable
        {

            var xy = new lol<int>();
            
            
           // xy.fooo<BlueMine.Db.T_projects>(SortDirection.Ascending, r => r. );
            
            
            
            System.Func<T, TKey>[] sorts = null;

            for (int i = sorts.Length - 1; i > -1; --i)
            {
                
                list.Sort(delegate(T x, T y)
                {
                    if (x == null && y == null)
                        return 0;
                    
                    if (x == null || y == null)
                    {
                        return (int)direction * (x == null ? 1 : -1);
                    }
                    
                    TKey a = sorts[i](x);
                    TKey b = sorts[i](y);
                    
                    return (int)direction * a.CompareTo(b);
                });
            
            }



            list.Sort(delegate(T x, T y)
            {
                if (x == null && y == null)
                    return 0;
                
                if (x == null || y == null)
                {
                    return (int)direction * (x == null ? 1 : -1);
                }
                
                TKey a = sorter(x);
                TKey b = sorter(y);
                
                return (int)direction * a.CompareTo(b);
            });
            
        }
        
        
        private static int ProjectSorter(
              GenericRecursor<T, TRet> previous
            , GenericRecursor<T, TRet> next)
        {
            return 0;
            //TReturn System.StringComparer.InvariantCultureIgnoreCase
            //    .Compare(previous.Me.name, next.Me.name);
        } // End Function ProjectSorter 
        
        
        public List<GenericRecursor<T, TRet>> RootNodes
        {
            get
            {
                List<GenericRecursor<T, TRet>> ls = new List<GenericRecursor<T, TRet>>();
                
                for (int i = 0; i < this.m_list.Count; ++i)
                {
                    if( object.Equals( this.m_parentId_func(this.m_list[i]), null))
                        ls.Add(new GenericRecursor<T, TRet>(this.m_list[i], this.m_list
                            ,this.m_parentId_func
                            ,this.m_id_func
                            ,this.Level));
                } // Next i 

                /////////////////////////ls.Sort(ProjectSorter);
                
                return ls;
            } // End Get 
            
        } // End Property RootNodes 
        
        
        public List<GenericRecursor<T, TRet>> ChildNodes
        {
            get
            {
                if (this.Me == null)
                    return null;
                
                List<GenericRecursor<T, TRet>> ls = new List<GenericRecursor<T, TRet>>();
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
                            , this.Level));
                } // Next i 
                
                ///////////////// ls.Sort(ProjectSorter);
                
                return ls;
            } // End Get 
            
        } // End Property ChildNodes 
        
        
    } // End Class ProjectRecursor 
    
    
} // End Namespace BlueMine.Models.Project
