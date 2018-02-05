using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860



using Dapper;


namespace BlueMine.Controllers
{

    // template: "{controller=Home}/{action=Index}/{id?}");
    [Route("[controller]/[action]/{id?}")]
    public class ProjectController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            System.Collections.Generic.List<Db.T_projects> projects = null;

            string sql = "SELECT * FROM projects ";
            // string sql = "SELECT * FROM projects WHERE id = @projid";

            using (System.Data.Common.DbConnection connection = SqlFactory.GetConnection())
            {
                projects = connection.Query<Db.T_projects>(sql).ToList();
            }

            return View(projects);
        }

        public void demo()
        {
            using (System.Data.Common.DbConnection connection = SqlFactory.GetConnection())
            {
                string sqlInvoices = "SELECT * FROM projects ";
                string sqlInvoice = "SELECT * FROM projects WHERE id = @projid";

                System.Collections.Generic.List<Db.T_projects> projects =
                    connection.Query<Db.T_projects>(sqlInvoices).ToList();

                System.Console.WriteLine(projects);

                var invoice = connection.QueryFirstOrDefault<Db.T_projects>(sqlInvoice, new { projid = 1 });
                System.Console.WriteLine(invoice);

                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@projid", 2
                    , System.Data.DbType.Int32
                    , System.Data.ParameterDirection.Input);

                System.Collections.Generic.List<Db.T_projects> invoice2 =
                    connection.Query<Db.T_projects>(sqlInvoice, parameter).ToList();
                System.Console.WriteLine(invoice2);


                using (var transaction = connection.BeginTransaction())
                {
                    int affRows = connection.Execute("sql",
                        new { Kind = 1, Code = "Single_Insert_1" },
                        commandType: System.Data.CommandType.StoredProcedure,
                        transaction: transaction);

                    transaction.Commit();
                }

                // dapper will iterate for you
                List<Db.T_projects> projectList = new List<Db.T_projects>();
                string processQuery = "INSERT INTO PROJECT_LOGS VALUES (@A, @B)";
                connection.Execute(processQuery, projectList);


                var affectedRows = connection.Execute(
                      "sp_something"
                    , new { Param1 = "Single_Insert_1" }
                    , commandType: System.Data.CommandType.StoredProcedure
                );


                /*
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@Kind", InvoiceKind.WebInvoice
                    , System.Data.DbType.Int32
                    , System.Data.ParameterDirection.Input);

                parameter.Add("@Code", "Many_Insert_0"
                    , System.Data.DbType.String
                    , System.Data.ParameterDirection.Input);

                parameter.Add("@RowCount"
                    , dbType: System.Data.DbType.Int32
                    , direction: System.Data.ParameterDirection.ReturnValue);

                connection.Query<Db.T_projects>(sqlInvoices, parameter).ToList();
                */
            }

        }


    }
}
