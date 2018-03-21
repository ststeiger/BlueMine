
// using System.Threading.Tasks;
using System.Linq;
using BlueMine.Db;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BlueMine.Models.Issue
{


    public class IssueModelFactory
    {
        protected BlueMineRepository m_repo;


        public IssueModelFactory(BlueMineRepository repository)
        {
            this.m_repo = repository;
        }

        public void SetDefaults(IssueModel im)
        {
            T_issues issue = new T_issues();
            issue.start_date = System.DateTime.Now;
            issue.due_date = null;

            issue.assigned_to_id = null;
            issue.author_id = 123;
            issue.done_ratio = 0;
            issue.project_id = 123;
            issue.parent_id = null;
            issue.status_id = 133;
            issue.tracker_id = 123;
            issue.updated_on = System.DateTime.Now;

            im.Issue = issue;
        }


        public IssueModel Create(int? issueId)
        {
            IssueModel im = new IssueModel();

            if (issueId.HasValue)
            {
                im.Issue = m_repo.FindById<T_issues>(issueId.Value);
            }
            else
                SetDefaults(im);

            InitCustomFields(im);
            InitPercentage(im);
            InitUsers(im);
            InitPriorities(im);
            InitStati(im);
            InitTrackers(im);

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
        } // End Function Create 


        public void InitPercentage(IssueModel im)
        {
            System.Collections.Generic.List<
                    Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                > ls = new System.Collections.Generic.List<SelectListItem>();

            // TODO: Make configurable 
            const int STEP_SIZE = 10; // 3;

            // Always stay below 100%
            for (int i = 0; i < 100; i += STEP_SIZE)
            {
                ls.Add(new SelectListItem()
                {
                    Text = i.ToString() + "%",
                    Value = i.ToString(),
                    Selected = i == 0
                });
            } // Next i 

            // Always add 100% as last option 
            ls.Add(new SelectListItem()
            {
                Text = "100%",
                Value = "100",
                Selected = false
            });

            im.PercentComplete = ls;
        } // End Sub InitPercentage 


        public void InitCustomFields(IssueModel im)
        {
            im.CustomFields = new System.Collections.Generic.List<
                System.Collections.Generic.List<
                    Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                >>();

            System.Collections.Generic.List<SelectListItem> a = 
                this.m_repo.CustomFieldAsSelectList("Kundenname");

            System.Collections.Generic.List<SelectListItem> b = 
                this.m_repo.CustomFieldAsSelectList("verrechenbar");

            System.Collections.Generic.List<SelectListItem> c = 
                this.m_repo.CustomFieldAsSelectList("Kunden informieren");

            im.CustomFields.Add(null); // 0
            im.CustomFields.Add(null); // 1
            im.CustomFields.Add(a); // 2
            im.CustomFields.Add(b); // 3
            im.CustomFields.Add(null); // 4
            im.CustomFields.Add(c); // 5
        } // End Sub InitCustomFields 


        public void InitUsers(IssueModel im)
        {
            im.AssignedTo = this.m_repo.GetAsSelectList<Db.T_users>(
              predicate: user => user.status == 1 && user.type == "User"
              , value: x => x.id.ToString()
              , text: y => y.lastname + " " + y.firstname
              , selected: z => z.id == im.Issue.assigned_to_id

              //,sorts: new System.Linq.Expressions.Expression<System.Func<Db.T_users, System.IComparable>>[]
              //{ a=> a.lastname, b=> b.firstname, c => c.last_login_on }

              , sorts: new Data.SortExpression<T_users>[]{
                    Data.SortExpression<T_users>.Create(a => a.lastname),
                    Data.SortExpression<T_users>.Create(a => a.firstname),
                    Data.SortExpression<T_users>.Create(a => a.last_login_on)
                  }
              );
        } // End Sub InitUsers 


        public void InitPriorities(IssueModel im)
        {
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
        } // End Sub InitPriorities 


        public void InitStati(IssueModel im)
        {
            im.Stati = this.m_repo.GetAsSelectList<T_issue_statuses>(
                  value: x => x.id.ToString()
                , text: y => y.name
                , selected: z => false

                , sorts: new Data.SortExpression<T_issue_statuses>[]{
                                Data.SortExpression<T_issue_statuses>.Create(a => a.position),
                                Data.SortExpression<T_issue_statuses>.Create(a => a.name)
                    }
            );
        } // End Sub InitStati 


        public void InitTrackers(IssueModel im)
        {
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
        } // End Sub InitTrackers 


    } // End Class IssueModelFactory 


} // End Namespace BlueMine.Models.Issue 
