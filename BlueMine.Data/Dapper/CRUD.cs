
using System.Linq;
using Dapper;


namespace BlueMine.Data.Dapper
{


    class CRUD
    {


        public System.Collections.Generic.List<Db.T_projects> GetProjects()
        {
            System.Collections.Generic.List<Db.T_projects> projects = null;

            string sql = "SELECT * FROM projects ";
            // string sql = "SELECT * FROM projects WHERE id = @projid";

            using (System.Data.Common.DbConnection connection = SqlFactory.GetConnection())
            {
                projects = connection.Query<Db.T_projects>(sql).ToList();
            }

            return projects;
        }



        public void Demo()
        {
            /*
            using (System.Data.Common.DbConnection connection = SqlFactory.GetConnection())
            {
                string sqlInvoices = "SELECT * FROM projects ";
                string sqlInvoice = "SELECT * FROM projects WHERE id = @projid";

                System.Collections.Generic.List<Db.T_projects> projects =
                    connection.Query<Db.T_projects>(sqlInvoices).ToList();

                System.Console.WriteLine(projects);

                var invoice = connection.QueryFirstOrDefault<Db.T_projects>(sqlInvoice, new {projid = 1});
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
                        new {Kind = 1, Code = "Single_Insert_1"},
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
                    , new {Param1 = "Single_Insert_1"}
                    , commandType: System.Data.CommandType.StoredProcedure
                );



                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@Kind", 123
                    , System.Data.DbType.Int32
                    , System.Data.ParameterDirection.Input);

                parameter.Add("@Code", "Many_Insert_0"
                    , System.Data.DbType.String
                    , System.Data.ParameterDirection.Input);

                parameter.Add("@RowCount"
                    , dbType: System.Data.DbType.Int32
                    , direction: System.Data.ParameterDirection.ReturnValue);

                connection.Query<Db.T_projects>(sqlInvoices, parameter).ToList();

            }
            */
        } // End Sub Demo 


    }


}
