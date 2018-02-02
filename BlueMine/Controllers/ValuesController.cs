using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


using Dapper;


namespace BlueMine.Controllers
{

    public class projects
    {
        public int id;
        public string name;
        public string description;
        public string homepage;
        public bool is_public;
        public int? parent_id;
        public System.DateTime? created_on;
        public System.DateTime? updated_on;
        public string identifier;
        public int status;
        public int? lft;
        public int? rgt;
        public bool inherit_members;
        public int? default_version_id;
    } 



    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
             
            using (System.Data.Common.DbConnection connection = SqlFactory.GetConnection())
            {
                string sqlInvoices = "SELECT * FROM projects ";
                string sqlInvoice = "SELECT * FROM projects WHERE id = @projid";

                System.Collections.Generic.List<projects> projects = 
                    connection.Query<projects>(sqlInvoices).ToList();

                System.Console.WriteLine(projects);

                var invoice = connection.QueryFirstOrDefault<projects>(sqlInvoice, new { projid = 1 });
                System.Console.WriteLine(invoice);

                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@projid", 2
                    , System.Data.DbType.Int32
                    , System.Data.ParameterDirection.Input);

                System.Collections.Generic.List<projects> invoice2 =
                    connection.Query<projects>(sqlInvoice, parameter).ToList();
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
                List<projects> projectList = new List<projects>();
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

                connection.Query<projects>(sqlInvoices, parameter).ToList();
                */
            }


            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
