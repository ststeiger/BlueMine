
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;


using System.Linq.Expressions;


namespace BlueMine.Db
{
    
    
    namespace WilderBlog.Data
    {
        
        
        public class BlueMineRepository // : BaseRepository, IWilderRepository
        {
            private BlueMineContext m_ctx;
            
            
            public BlueMineRepository(BlueMineContext context)
            {
                this.m_ctx = context;
            }
            
            
            public TEntity GetSingle<TEntity>(
                Expression<System.Func<TEntity, bool>> predicate
            ) where TEntity : class
            {
                return this.m_ctx.Set<TEntity>()
                    .AsNoTracking()
                    .FirstOrDefault(predicate);
            }
            
            
            public async Task<TEntity> GetSingleAsync<TEntity>(
                Expression<System.Func<TEntity, bool>> predicate
            ) where TEntity : class
            {
                return await this.m_ctx.Set<TEntity>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(predicate);

                //this.m_ctx.Set<TEntity>().AsNoTracking();
            }
            
            
            /*
            public void AddSort<TEntity, TKey>(System.Func<TEntity, TKey> sorter
                    , BlueMine.Data.SortDirection direction)
                where TEntity : class
                // where TKey: System.IComparable
                // where TKey: struct, System.IComparable
            {
                System.Collections.Generic.List<BlueMine.Data.SortTerm<TEntity>> sorts = 
                    new System.Collections.Generic.List<BlueMine.Data.SortTerm<TEntity>>();
                
                
                sorts.Add(BlueMine.Data
                    .SortTerm<TEntity>.Create<TKey>(sorter, direction));
            }


            public void AddSort<TEntity, TKey>(System.Func<TEntity, TKey> sorter)
                // where TKey: System.IComparable
                // where TKey: struct, System.IComparable
            {
                this.AddSort<TEntity, TKey>(sorter, BlueMine.Data.SortDirection.Ascending);
            }


            public void AddSort<TEntity>(params BlueMine.Data.SortTerm<TEntity>[] sorts)
            {
                System.Collections.Generic.List<BlueMine.Data.SortTerm<TEntity>> sorts = 
                    new System.Collections.Generic.List<BlueMine.Data.SortTerm<TEntity>>();
                
                sorts.AddRange(sorts);
            }
            */
            
            
            public System.Collections.Generic.List<TEntity> 
                GetAll<TEntity>() where TEntity : class
            {               
                System.Collections.Generic.List<TEntity> ls = 
                     this.m_ctx.Set<TEntity>()
                    .Select(x => x)
                    .AsNoTracking()
                    .ToList();
                
                System.Collections.Generic.List<BlueMine.Data.SortTerm<TEntity>> sorts = 
                    new System.Collections.Generic.List<BlueMine.Data.SortTerm<TEntity>>();
                
                for (int i = 0; i < sorts.Count; ++i)
                {
                    ls.Sort((System.Comparison<TEntity>)
                        delegate(TEntity x, TEntity y)
                    {
                        BlueMine.Data.SortDirection direction = sorts[i].Direction;
                        
                        if (x == null && y == null)
                            return 0;
                        
                        if (x == null || y == null)
                            return (x == null ? 1 : -1);
                        
                        
                        System.IComparable a = sorts[i].Sort(x);
                        System.IComparable b = sorts[i].Sort(y);
                        
                        if (a == null && b == null)
                            return 0;
                        
                        if (a == null || b == null)
                        {
                            return (a == null ? 1 : -1);
                        }
                        
                        return (int)direction * a.CompareTo(b);
                    });
                    
                } // Next i 
                
                return ls;
            }
            
            
            private static void SetPropertyValue<T>
                (T target, Expression<System.Func<T>> memberLamda, object value)
            {
                var memberSelectorExpression = memberLamda.Body as MemberExpression;
                if (memberSelectorExpression != null)
                {
                    System.Reflection.PropertyInfo property = memberSelectorExpression.Member 
                        as System.Reflection.PropertyInfo;
                    if (property != null)
                    {
                        property.SetValue(target, value, null);
                        return;
                    }
                    
                    System.Reflection.FieldInfo fi = memberSelectorExpression.Member 
                        as System.Reflection.FieldInfo;
                    if (fi != null)
                    {
                        fi.SetValue(target, value);
                        return;
                    }
                    
                }
                
                throw new System.InvalidOperationException("Cannot delete with no valid where condition.");
            }
            
            
            public void Delete<TEntity>(
                Expression<System.Func<TEntity>> memberLamda, object value
                ) where TEntity:class, new()
            {
                TEntity employer = new TEntity(); 
                //{ Id = 1 };
                SetPropertyValue(employer, memberLamda, value);
                
                
                this.m_ctx.Entry(employer).State = EntityState.Deleted;
                this.m_ctx.SaveChanges();


                Delete<T_projects>(x =>
                {
                    x.id = 5;
                    x.parent_id = 3;
                });

            }

            public delegate void DeleteWhere_t<TEntity>(TEntity ent) where TEntity : class, new();
            
            public void Delete<TEntity>(DeleteWhere_t<TEntity> cb
            ) where TEntity : class, new()
            {
                TEntity employer = new TEntity();
                cb(employer);
                
                this.m_ctx.Entry(employer).State = EntityState.Deleted;
                this.m_ctx.SaveChanges();
                
                // var someList = new System.Collections.Generic.List<TEntity>();
                // someList.All(x => { x.SomeProp = "foo"; return true; })
            }
            
            
            public void Add<TEntity>(TEntity ent) where TEntity:class
            {
                this.m_ctx.Add(ent).State = EntityState.Added;
                this.m_ctx.SaveChanges();
            }

            


            /*
    // IQueryable<TEntity> GetAll();
    // Task<TEntity> GetById(int id);
    
    // Task Create(TEntity entity);
    
    Task Update(int id, TEntity entity);
    
             */
            
            
            
        }
    }



    public static class T_trackersExtensions
    {
        
        public static System.Collections.Generic.List<SelectListItem> 
            GetTrackers(this T_trackers t, BlueMine.Db.BlueMineContext db)
        {
            System.Collections.Generic.List<SelectListItem> trackerz = (
                    from tracker in db.trackers
                    orderby tracker.position ascending
                    select new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                    {
                        Value = tracker.id.ToString(),
                        Text = tracker.name
                        // ,Selected = tracker.default_status_id
                    }
                ).AsNoTracking()
                .ToList();
            
            return trackerz;
        }
        
        
    }



    public partial class T_trackers
    {
        
        
        private readonly BlueMine.Db.BlueMineContext m_db;  
        
        
        public T_trackers(BlueMine.Db.BlueMineContext dbContext)
        {
            this.m_db = dbContext;
        }
        
            
    }
    
}