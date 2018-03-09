
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata;


namespace BlueMine.Db
{

    public class GenericEntityFramworkRepository<T> // : BaseRepository, IWilderRepository
        where T : DbContext
    {

        protected T m_ctx;


        public GenericEntityFramworkRepository(T context)
        {
            this.m_ctx = context;
        }


        // IQueryable<TEntity> GetAll();
        // Task<TEntity> GetById(int id);
        // Task Create(TEntity entity);
        // Task Update(int id, TEntity entity);


        public TEntity GetFirst<TEntity>(
            Expression<System.Func<TEntity, bool>> predicate
        ) where TEntity : class
        {
            return this.m_ctx.Set<TEntity>()
                .AsNoTracking()
                .First(predicate);
        }
        
        
        public TEntity FindById<TEntity>(
            params object[] keyValues
        ) where TEntity : class
        {
            // https://msdn.microsoft.com/en-us/library/gg696418%28v=vs.103%29.aspx
            // Uses the primary key value to attempt to find an entity tracked
            // by the context. If the entity is not in the context then a query
            // will be executed and evaluated against the data in the data source,
            // and null is returned if the entity is not found 
            // in the context or in the data source.
            return this.m_ctx.Set<TEntity>().Find(keyValues);
        }
        
        
        public object FindById(System.Type type, params object[] keyValues)
        {
            System.Reflection.MethodInfo getFindByIdGeneric = 
                this.GetType().GetMethod("FindById", new System.Type[] { typeof(object[]) });
            
            if (getFindByIdGeneric == null)
                return null;
            
            System.Reflection.MethodInfo getFindById = getFindByIdGeneric
                .MakeGenericMethod(type);
            
            if (getFindById == null)
                return null;
                
            object objectById = getFindById.Invoke(this, new object[]
            {
                (object) keyValues
            });
            
            return objectById;
        } // End Function FindById 


        public async Task<TEntity> GirstFirstAsync<TEntity>(
            Expression<System.Func<TEntity, bool>> predicate
        ) where TEntity : class
        {
            return await this.m_ctx.Set<TEntity>()
                .AsNoTracking()
                .FirstAsync(predicate);
        }


        public TEntity GirstFirstOrDefault<TEntity>(
            Expression<System.Func<TEntity, bool>> predicate
        ) where TEntity : class
        {
            return this.m_ctx.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefault(predicate);
        }


        public async Task<TEntity> GirstFirstOrDefaultAsync<TEntity>(
            Expression<System.Func<TEntity, bool>> predicate
        ) where TEntity : class
        {
            return await this.m_ctx.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(predicate);
        }


        //protected int CalculatePages(int totalCount, int pageSize)
        //{
        //    return ((int)(totalCount / pageSize)) + ((totalCount % pageSize) > 0 ? 1 : 0);
        //}


        //public int GetNumPages<TEntity>(int pageSize) where TEntity : class
        //{
        //    int totalCount = this.m_ctx.Set<TEntity>().Count();
        //    return CalculatePages(totalCount, pageSize);
        //}


        protected long CalculatePages(long totalCount, long pageSize)
        {
            return ((long)(totalCount / pageSize)) + ((totalCount % pageSize) > 0 ? 1L : 0L);
        }
        

        public long GetNumPages<TEntity>(long pageSize) where TEntity : class
        {
            long totalCount = this.m_ctx.Set<TEntity>().LongCount();
            return CalculatePages(totalCount, pageSize);
        }


        public List<TEntity>
        GetAll<TEntity>() where TEntity : class
        {
            return GetFilteredPagedSorted(null, null, null, (BlueMine.Data.SortExpression<TEntity>[])null);
        }
        
        
        public object GetAll(System.Type type)
        {
            // System.Reflection.MethodInfo getAllGeneric = tRepo.GetMethod("GetAll");
            System.Reflection.MethodInfo getAllGeneric = this.GetType()
                .GetMethod("GetAll", new System.Type[0] { });
                
            if (getAllGeneric == null)
                return null;
            
            System.Reflection.MethodInfo getAll = getAllGeneric.MakeGenericMethod(type);
            if (getAll == null)
                return null;
            
            object ls = getAll.Invoke(this, null);
            return ls;
        }
        
        
        public List<TEntity> GetAll<TEntity>(
            params Expression<System.Func<TEntity, System.IComparable>>[] sorts)
            where TEntity : class
        {
            return GetFilteredPagedSorted(null, null, null, sorts);
        }
        
        
        public List<TEntity> GetAll<TEntity>(
            params BlueMine.Data.SortExpression<TEntity>[] sorts
        ) where TEntity : class
        {
            return GetFilteredPagedSorted(null, null, null, sorts);
        }
        
        
        public List<TEntity> GetFilteredSorted<TEntity>(
              Expression<System.Func<TEntity, bool>> predicate
            , params Expression<System.Func<TEntity
            , System.IComparable>>[] sorts
        ) where TEntity : class
        {
            return GetFilteredPagedSorted(predicate, null, null, sorts);
        }


        public List<TEntity> GetFilteredSorted<TEntity>(
            Expression<System.Func<TEntity, bool>> predicate
            , params BlueMine.Data.SortExpression<TEntity>[] sorts)
            where TEntity : class
        {
            return GetFilteredPagedSorted(predicate, null, null, sorts);
        }


        public List<TEntity> GetFilteredPagedSorted<TEntity>(
            Expression<System.Func<TEntity, bool>> predicate
           , System.Nullable<int> pageSize
           , System.Nullable<int> page
           , params Expression<System.Func<TEntity, System.IComparable>>[] sorts
        ) where TEntity : class
        {
            List<BlueMine.Data.SortExpression<TEntity>> ls =
                new List<BlueMine.Data.SortExpression<TEntity>>();

            for (int i = 0; i < sorts.Length; ++i)
            {
                ls.Add(
                    new BlueMine.Data.SortExpression<TEntity>(sorts[i], BlueMine.Data.SortDirection.Ascending)
                );
            } // Next i 

            return GetFilteredPagedSorted(predicate, pageSize, page, ls.ToArray());
        }


        public List<TEntity> GetFilteredPagedSorted<TEntity>(
            Expression<System.Func<TEntity, bool>> predicate
            , System.Nullable<int> pageSize
            , System.Nullable<int> page
            , params BlueMine.Data.SortExpression<TEntity>[] sorts
        ) where TEntity : class
        {
            IQueryable<TEntity> query = this.m_ctx.Set<TEntity>();

            if (predicate != null)
                query = query.Where(predicate);

            if (sorts != null)
            {
                //foreach (Expression<System.Func<TEntity, System.IComparable>> thisSort in sorts)
                //foreach (BlueMine.Data.SortExpression<TEntity> thisSortExpression in sorts)
                //{
                //    if (thisSortExpression.Direction == BlueMine.Data.SortDirection.Ascending)
                //        query = query.OrderBy(thisSortExpression.Sort);
                //    else
                //        query = query.OrderByDescending(thisSortExpression.Sort);
                //} // Next thisSort 

                for (int i = sorts.Length - 1; i > -1; --i)
                {
                    if (sorts[i].Direction == BlueMine.Data.SortDirection.Ascending)
                        query = query.OrderBy(sorts[i].Sort);
                    else
                        query = query.OrderByDescending(sorts[i].Sort);
                } // Next i 

            } // End if (sorts != null) 

            if (pageSize.HasValue && page.HasValue)
            {
                query = query
                    .Skip(pageSize.Value * (page.Value - 1))
                    .Take(pageSize.Value)
                ;
            } // End if (pageSize.HasValue && page.HasValue) 

            List<TEntity> ls = query.AsNoTracking().ToList();
            return ls;
        }


        public List<SelectListItem> GetAsSelectList<TEntity>(
              System.Func<TEntity, string> value
            , System.Func<TEntity, string> text 
        ) where TEntity : class
        {
            return GetAsSelectList(null, value, text, null, null, null
                , (BlueMine.Data.SortExpression<TEntity>[])null
                );
        }


        public List<SelectListItem> GetAsSelectList<TEntity>(
              System.Func<TEntity, string> value
            , System.Func<TEntity, string> text
            , params BlueMine.Data.SortExpression<TEntity>[] sorts
        ) where TEntity : class
        {
            return GetAsSelectList(null, value, text, null, null, null, sorts);
        }

        public List<SelectListItem> GetAsSelectList<TEntity>(
              System.Func<TEntity, string> value
            , System.Func<TEntity, string> text
            , params Expression<System.Func<TEntity, System.IComparable>>[] sorts
        ) where TEntity : class
        {
            return GetAsSelectList(null, value, text, null, null, null, sorts);
        }


        public List<SelectListItem> GetAsSelectList<TEntity>(
              Expression<System.Func<TEntity, bool>> predicate
            , System.Func<TEntity, string> value
            , System.Func<TEntity, string> text
            , params BlueMine.Data.SortExpression<TEntity>[] sorts
        ) where TEntity : class
        {
            return GetAsSelectList(predicate, value, text, null, null, null, sorts);
        }


        public List<SelectListItem> GetAsSelectList<TEntity>(
              Expression<System.Func<TEntity, bool>> predicate
            , System.Func<TEntity, string> value
            , System.Func<TEntity, string> text
            , params Expression<System.Func<TEntity, System.IComparable>>[] sorts
        ) where TEntity : class
        {
            return GetAsSelectList(predicate, value, text, null, null, null, sorts);
        }


        public List<SelectListItem> GetAsSelectList<TEntity>(
              Expression<System.Func<TEntity, bool>> predicate
            , System.Func<TEntity, string> value
            , System.Func<TEntity, string> text
        ) where TEntity : class
        {
            return GetAsSelectList(predicate, value, text, null, null, null, (Expression<System.Func<TEntity, System.IComparable>>[])null);
        }


        public List<SelectListItem> GetAsSelectList<TEntity>(
              System.Func<TEntity, string> value
            , System.Func<TEntity, string> text
            , System.Func<TEntity, bool> selected
            , params BlueMine.Data.SortExpression<TEntity>[] sorts
        ) where TEntity : class
        {
            return GetAsSelectList(null, value, text, selected, null, null, sorts);
        }

        public List<SelectListItem> GetAsSelectList<TEntity>(
              System.Func<TEntity, string> value
            , System.Func<TEntity, string> text
            , System.Func<TEntity, bool> selected
            , params Expression<System.Func<TEntity, System.IComparable>>[] sorts
        ) where TEntity : class
        {
            return GetAsSelectList(null, value, text, selected, null, null, sorts);
        }


        public List<SelectListItem> GetAsSelectList<TEntity>(
              Expression<System.Func<TEntity, bool>> predicate
            , System.Func<TEntity, string> value
            , System.Func<TEntity, string> text
            , System.Func<TEntity, bool> selected
            , params BlueMine.Data.SortExpression<TEntity>[] sorts
        ) where TEntity : class
        {
            return GetAsSelectList(predicate, value, text, selected, null, null, sorts);
        }

        public List<SelectListItem> GetAsSelectList<TEntity>(
              Expression<System.Func<TEntity, bool>> predicate
            , System.Func<TEntity, string> value
            , System.Func<TEntity, string> text
            , System.Func<TEntity, bool> selected
            , params Expression<System.Func<TEntity, System.IComparable>>[] sorts
        ) where TEntity : class
        {
            return GetAsSelectList(predicate, value, text, selected, null, null, sorts);
        }


        public List<SelectListItem> GetAsSelectList<TEntity>(
              Expression<System.Func<TEntity, bool>> predicate
            , System.Func<TEntity, string> value
            , System.Func<TEntity, string> text
            , System.Func<TEntity, bool> selected
        ) where TEntity : class
        {
            return GetAsSelectList(predicate, value, text, selected, null, null,
                (Expression<System.Func<TEntity, System.IComparable>>[])null);
        }


        public List<SelectListItem> GetAsSelectList<TEntity>(
              Expression<System.Func<TEntity, bool>> predicate
            , System.Func<TEntity, string> value
            , System.Func<TEntity, string> text
            , System.Func<TEntity, bool> selected
            , System.Func<TEntity, bool> disabled
            , System.Func<TEntity, SelectListGroup> group
            , params Expression<System.Func<TEntity, System.IComparable>>[] sorts
        ) where TEntity : class
        {
            List<BlueMine.Data.SortExpression<TEntity>> ls =
                new List<BlueMine.Data.SortExpression<TEntity>>();

            for (int i = 0; i < sorts.Length; ++i)
            {
                ls.Add(
                    new BlueMine.Data.SortExpression<TEntity>(sorts[i], BlueMine.Data.SortDirection.Ascending)
                );
            } // Next i 

            return GetAsSelectList(predicate, value, text, selected, null, null, ls.ToArray());
        }


        public List<SelectListItem> GetAsSelectList<TEntity>(
              Expression<System.Func<TEntity, bool>> predicate
            , System.Func<TEntity, string> value
            , System.Func<TEntity, string> text
            , System.Func<TEntity, bool> selected
            , System.Func<TEntity, bool> disabled
            , System.Func<TEntity, SelectListGroup> group
            , params BlueMine.Data.SortExpression<TEntity>[] sorts
        ) where TEntity : class
        {
            IQueryable<TEntity> query = this.m_ctx.Set<TEntity>();

            if (predicate != null)
                query = query.Where(predicate);

            if (sorts != null)
            {

                for (int i = sorts.Length - 1; i > -1; --i)
                {
                    if (sorts[i].Direction == BlueMine.Data.SortDirection.Ascending)
                        query = query.OrderBy(sorts[i].Sort);
                    else
                        query = query.OrderByDescending(sorts[i].Sort);
                } // Next i 

                ////foreach (Expression<System.Func<TEntity, System.IComparable>> thisSort in sorts)
                //foreach (BlueMine.Data.SortExpression<TEntity> thisSortExpression in sorts)
                //{
                //    if (thisSortExpression.Direction == BlueMine.Data.SortDirection.Ascending)
                //        query = query.OrderBy(thisSortExpression.Sort);
                //    else
                //        query = query.OrderByDescending(thisSortExpression.Sort);
                //} // Next thisSort 

            } // End if (sorts != null) 


            List<SelectListItem> ls = query.AsNoTracking()
                .Select(item => new SelectListItem()
                {
                    Value = value != null ? value(item) : text(item),
                    Text = text != null ? text(item) : value(item),
                    Selected = selected != null ? selected(item) : false,
                    Group = group != null ? group(item) : null,
                    Disabled = disabled != null ? disabled(item) : false,
                }
            ).ToList();

            return ls;
        } // End Function GetAsSelectList 
        
        
        public List<string> ListTables()
        {
            List<string> lsTables = new List<string>();
            
            // var mapping = _context.Model.FindEntityType(typeof(string)).Relational();
            // string schema = mapping.Schema;
            // string tableName = mapping.TableName;
            foreach (IEntityType et in this.m_ctx.Model.GetEntityTypes())
            {
                // System.Console.WriteLine(et.ClrType.Name);
                // System.Console.WriteLine(et.ClrType.FullName);
                // System.Console.WriteLine(et.ClrType.Assembly.FullName);
                        
                IRelationalEntityTypeAnnotations rel = et.Relational();
                        
                // string schema = rel.Schema;
                string table = rel.TableName;
                lsTables.Add(table);
                // System.Console.WriteLine(schema, table);
            } // Next et 

            return lsTables;
        } // End Function ListTables 
        
        
        public List<TEntity> GetSortedInDotNet<TEntity>(
              Expression<System.Func<TEntity, bool>> predicate
            , params BlueMine.Data.SortTerm<TEntity>[] sorts
        ) where TEntity : class
        {
            List<TEntity> ls =
                 this.m_ctx.Set<TEntity>()
                .Where(predicate)
                .AsNoTracking()
                .ToList();

            for (int i = 0; i < sorts.Length; ++i)
            {
                ls.Sort(
                    delegate (TEntity x, TEntity y)
                    {
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

                        return ((int)(sorts[i].Direction)) * a.CompareTo(b);
                    } // End Delegate Comparison<in T>(T x, T y);
                );

            } // Next i 

            return ls;
        } // End Function GetSortedInDotNet 


        //private static void SetPropertyValue<T>
        //    (T target, Expression<System.Func<T>> memberLamda, object value)
        //{
        //    var memberSelectorExpression = memberLamda.Body as MemberExpression;
        //    if (memberSelectorExpression != null)
        //    {
        //        System.Reflection.PropertyInfo property = memberSelectorExpression.Member
        //            as System.Reflection.PropertyInfo;
        //        if (property != null)
        //        {
        //            property.SetValue(target, value, null);
        //            return;
        //        }

        //        System.Reflection.FieldInfo fi = memberSelectorExpression.Member
        //            as System.Reflection.FieldInfo;
        //        if (fi != null)
        //        {
        //            fi.SetValue(target, value);
        //            return;
        //        }

        //    } // End if (memberSelectorExpression != null) 

        //    throw new System.InvalidOperationException("Cannot delete with no valid where condition.");
        //} // End Sub SetPropertyValue 


        // https://stackoverflow.com/questions/37970020/entity-framework-core7-bulk-update
        public void Delete<TEntity>(
            Expression<System.Func<TEntity, bool>> predicate
        ) where TEntity : class
        {
            // https://forums.asp.net/t/2077667.aspx?Update+Record+based+on+WHERE+clause+in+Entity+Framework+6
            this.m_ctx.RemoveRange(
                this.m_ctx.Set<TEntity>().Where(predicate)
            );
        }


        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            this.m_ctx.Update(entity).State = EntityState.Modified;
            this.m_ctx.SaveChanges();
        }


        public void Update<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            foreach (TEntity ent in entities)
            {
                this.m_ctx.Update(ent).State = EntityState.Modified;
            }

            this.m_ctx.SaveChanges();
        }


        // Bulk-Update 
        public void Update<TEntity>(
              Expression<System.Func<TEntity, bool>> predicate
            , System.Action<TEntity> updateFields
        ) where TEntity : class
        {
            List<TEntity> ls = this.m_ctx.Set<TEntity>().Where(predicate).ToList();
            ls.ForEach(updateFields);

            this.m_ctx.UpdateRange(ls);
        }


        public void Add<TEntity>(TEntity ent) where TEntity : class
        {
            this.m_ctx.Add(ent).State = EntityState.Added;
            this.m_ctx.SaveChanges();
        }


        public void Add<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            foreach (TEntity ent in entities)
            {
                this.m_ctx.Add(ent).State = EntityState.Added;
            }

            this.m_ctx.SaveChanges();
        }


    } // End  class GenericEntityFramworkRepository<T> 


} // End Namespace BlueMine.Db
