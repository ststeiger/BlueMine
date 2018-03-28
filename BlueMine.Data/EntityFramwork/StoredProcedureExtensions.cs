
namespace Microsoft.EntityFrameworkCore
{


    public static class StoredProcedureExtensions
    {


        private static System.Data.DbType GetDbType(object obj)
        {
            if (obj == null)
                return System.Data.DbType.Object;
            
            System.Type type = obj.GetType();

            // http://social.msdn.microsoft.com/Forums/en/winforms/thread/c6f3ab91-2198-402a-9a18-66ce442333a6
            string strTypeName = type.Name;
            System.Data.DbType dbType = System.Data.DbType.String; // default value

            try
            {
                if (object.ReferenceEquals(type, typeof(System.DBNull)))
                {
                    return dbType;
                }

                if (object.ReferenceEquals(type, typeof(System.Byte[])))
                {
                    return System.Data.DbType.Binary;
                }

                dbType = (System.Data.DbType)System.Enum.Parse(typeof(System.Data.DbType), strTypeName, true);

                // Es ist keine Zuordnung von DbType UInt64 zu einem bekannten SqlDbType vorhanden.
                // http://msdn.microsoft.com/en-us/library/bbw6zyha(v=vs.71).aspx
                if (dbType == System.Data.DbType.UInt64)
                    dbType = System.Data.DbType.Int64;
            }
            catch (System.Exception)
            {
                // add error handling to suit your taste
            }

            return dbType;
        } // End Function GetDbType


        public static void xxx(System.Data.Common.DbDataReader dr)
        {

        }


        //public static System.Data.DataTable ExecuteStoredProcedure(this Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade databaseFacade
        // )
        //{
        //    return ExecuteStoredProcedure(databaseFacade, "foo", ("abc", "def"), ("abc", 5));
        //}


        public static System.Data.DataTable ExecuteStoredProcedure(
            this Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade databaseFacade,
            string procedureName,
            params System.ValueTuple<string, object>[] parameters
            )
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            // using (System.Data.Common.DbConnection con = databaseFacade.GetDbConnection())
            System.Data.Common.DbConnection con = databaseFacade.GetDbConnection();
            {
                using (System.Data.Common.DbCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = procedureName;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        for (int i = 0; i < parameters.Length; ++i)
                        {
                            System.Data.Common.DbParameter p = cmd.CreateParameter();
                            p.ParameterName = parameters[i].Item1;
                            p.Value = parameters[i].Item2;
                            p.DbType = GetDbType(p.Value);

                            if (p.Value == null)
                            {
                                p.Value = System.DBNull.Value;
                            } // End if (p.Value == null) 

                            cmd.Parameters.Add(p);
                        } // Next i  

                    } // End if (parameters != null) 


                    // cmd.ExecuteNonQuery();
                    // cmd.ExecuteReader();
                    using (System.Data.Common.DbDataAdapter da = System.Data.Common.ProviderExtensions.CreateDataAdapter(con, cmd))
                    {
                        da.Fill(dt);
                    } // End Using da 

                } // End Using cmd 

            } // End Using con 

            return dt;
        } // End Function ExecuteStoredProcedure 


    } // End Class StoredProcedureExtensions 


} // End Namespace Microsoft.EntityFrameworkCore
