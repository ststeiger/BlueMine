using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueMine
{

    public class SqlFactory
    {
        private static string s_cs;


        static SqlFactory()
        {
            s_cs = GetConnectionString();
        }


        public static System.Data.Common.DbConnection GetConnection()
        {
            return new System.Data.SqlClient.SqlConnection(s_cs);
        }

        
        public static string ConnectionString
        {
            get { return GetConnectionString(); }
        }



        public static string GetConnectionString()
        {
            System.Data.SqlClient.SqlConnectionStringBuilder csb = new System.Data.SqlClient.SqlConnectionStringBuilder();

            csb.DataSource = TestPlotly.SecretManager.GetSecret<string>("DataSource");
            csb.InitialCatalog = "Redmine";

            csb.UserID = TestPlotly.SecretManager.GetSecret<string>("DefaultDbUser"); ;
            csb.Password = TestPlotly.SecretManager.GetSecret<string>("DefaultDbPassword"); ;

            csb.PacketSize = 4096;
            csb.PersistSecurityInfo = false;
            csb.ApplicationName = "BlueMine";
            csb.ConnectTimeout = 15;
            csb.Pooling = true;
            csb.MinPoolSize = 1;
            csb.MaxPoolSize = 100;
            csb.MultipleActiveResultSets = false;
            csb.WorkstationID = System.Environment.MachineName;

            return csb.ConnectionString;
        }
    }

}
