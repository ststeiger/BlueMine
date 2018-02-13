
namespace BlueMine
{
    
    
    public class SqlFactory
    {
        private static string s_cs;
        private static System.Data.Common.DbProviderFactory s_fac;
        
        
        static SqlFactory()
        {
            s_cs = GetConnectionString();
            s_fac = Mono.Sucks.DbProviderFactories.GetFactory(
                typeof(System.Data.SqlClient.SqlClientFactory)
            );
        }
        
        
        public static System.Data.Common.DbConnection GetConnection()
        {
            System.Data.Common.DbConnection con = s_fac.CreateConnection();
            con.ConnectionString = s_cs;
            
            return con;
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
            
            csb.UserID = TestPlotly.SecretManager.GetSecret<string>("DefaultDbUser");
            csb.Password = TestPlotly.SecretManager.GetSecret<string>("DefaultDbPassword");
            
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
