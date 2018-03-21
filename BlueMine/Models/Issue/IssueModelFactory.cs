
// using System.Threading.Tasks;
using System.Linq;
using BlueMine.Db;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;


namespace BlueMine.Models.Issue
{

    public class IssueModelFactory
    {
        protected BlueMineRepository m_repo;


        public IssueModelFactory(BlueMineRepository repository)
        {
            this.m_repo = repository;
        }


        public void InitCustomFields(IssueModel im)
        {
            im.CustomFields = new System.Collections.Generic.List<
                System.Collections.Generic.List<
                    Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                >>();
            
            var a = this.m_repo.CustomFieldAsSelectList("Kundenname");
            var b = this.m_repo.CustomFieldAsSelectList("verrechenbar");
            var c = this.m_repo.CustomFieldAsSelectList("Kunden informieren");
            
            im.CustomFields.Add(null); // 0
            im.CustomFields.Add(null); // 1
            im.CustomFields.Add(a); // 2
            im.CustomFields.Add(b); // 3
            im.CustomFields.Add(null); // 4
            im.CustomFields.Add(c); // 5
        }
        
        
        public IssueModel Create(int? issueId)
        {
            IssueModel im = new IssueModel();

            InitCustomFields(im);
            
            im.Users = this.m_repo.GetAsSelectList<Db.T_users>(
                predicate: user => user.status == 1 && user.type == "User"
                , value: x => x.id.ToString()
                , text: y => y.lastname + " " + y.firstname
                , selected: z => false

                //,sorts: new System.Linq.Expressions.Expression<System.Func<Db.T_users, System.IComparable>>[]
                //{ a=> a.lastname, b=> b.firstname, c => c.last_login_on }

                , sorts: new Data.SortExpression<T_users>[]{
                    Data.SortExpression<T_users>.Create(a => a.lastname),
                    Data.SortExpression<T_users>.Create(a => a.firstname),
                    Data.SortExpression<T_users>.Create(a => a.last_login_on)
                    }
                );


            im.Priorities = this.m_repo.GetAsSelectList<Db.T_enumerations>(
            predicate: enu => !enu.project_id.HasValue && enu.type == "IssuePriority"
            , value: x => x.id.ToString()
            , text: y => y.name
            , selected: z => z.is_default

            , sorts: new Data.SortExpression<T_enumerations>[]{
                    Data.SortExpression<T_enumerations>.Create(a => a.position),
                    Data.SortExpression<T_enumerations>.Create(a => a.name)
                }
            );


            im.Stati = this.m_repo.GetAsSelectList<T_issue_statuses>(
                  value: x => x.id.ToString()
                , text: y => y.name
                , selected: z => false

                , sorts: new Data.SortExpression<T_issue_statuses>[]{
                                Data.SortExpression<T_issue_statuses>.Create(a => a.position),
                                Data.SortExpression<T_issue_statuses>.Create(a => a.name)
                    }
            );


            im.Trackers = this.m_repo.GetAsSelectList<T_trackers>(
                 value: x => x.id.ToString()
               , text: y => y.name
               //, selected: z => z.default_status_id 
               , selected: z => false

               , sorts: new Data.SortExpression<T_trackers>[]{
                                Data.SortExpression<T_trackers>.Create(a => a.position),
                                Data.SortExpression<T_trackers>.Create(a => a.name)
                   }
           );


            SelectListItem defaultItem = new SelectListItem()
            {
                Value = null,
                Text = "--- Bitte auswählen ---"
            };
            im.Trackers.Insert(0, defaultItem);



            if (!issueId.HasValue)
                return im;

            // im.issue.estimated_hours

            //if (issueId.HasValue)
            //{
            //    var y = (
            //        from user in m_contex.users
            //        where user.status == 1
            //              && user.type == "User"
            //        orderby user.firstname, user.last_login_on
            //        select new
            //        {
            //            Id = user.id,
            //            Text = user.firstname + " " + user.lastname
            //        }
            //    ).Take(1);
            //}

            return im;
        }
    }


}
