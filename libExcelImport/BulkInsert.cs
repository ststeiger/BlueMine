
using System.Data;
using System.Data.SqlClient;

using OfficeOpenXml;


namespace libExcelImport
{


    class BulkInsert
    {

        protected void ExcelFileToSql(System.IO.Stream stream)
        {
            // if (IsPostBack && Upload.HasFile)
            {
                // if (System.IO.Path.GetExtension(Upload.FileName).Equals(".xlsx"))
                {

                    // ExcelPackage excel = new ExcelPackage(Upload.FileContent);

                    ExcelPackage excel = new ExcelPackage(stream);
                    System.Data.DataTable dt = excel.ToDataTable();
                    string table = "Contacts";
                    using (var conn = new SqlConnection("Server=.;Database=Test;Integrated Security=SSPI"))
                    {
                        SqlBulkCopy bulkCopy = new SqlBulkCopy(conn);

                        bulkCopy.DestinationTableName = table;
                        conn.Open();
                        System.Data.DataTable schema = conn.GetSchema(
                              "Columns"
                            , new[] { null, null, table, null }
                        );

                        foreach (DataColumn sourceColumn in dt.Columns)
                        {
                            foreach (DataRow row in schema.Rows)
                            {
                                if (string.Equals(sourceColumn.ColumnName, (string)row["COLUMN_NAME"], System.StringComparison.OrdinalIgnoreCase))
                                {
                                    bulkCopy.ColumnMappings.Add(sourceColumn.ColumnName, (string)row["COLUMN_NAME"]);
                                    break;
                                } // End if (string.Equals(sourceColumn.ColumnName, (string)row["COLUMN_NAME"], StringComparison.OrdinalIgnoreCase)) 

                            } // Next DataRow 

                        } // Next sourceColumn 

                        bulkCopy.WriteToServer(dt);
                    } // End Using conn 

                } // End if (Path.GetExtension(Upload.FileName).Equals(".xlsx"))

            } // End if (IsPostBack && Upload.HasFile) 

        } // End Sub ExcelFileToSql(System.IO.Stream stream) 


    } // End Class BulkInsert 


} // End Namespace libExcelImport
