
using System.Data;
using System.Linq;

using OfficeOpenXml;


namespace libExcelImport
{


    // https://github.com/VahidN/EPPlus.Core
    // https://www.mikesdotnetting.com/article/297/the-best-way-to-import-data-from-excel-to-sql-server-via-asp-net
    // https://stackoverflow.com/questions/13669733/export-datatable-to-excel-with-epplus
    public static class ExcelPackageExtensions
    {
        public static void Test()
        {
            var dt = libExcelImport.ExcelPackageExtensions.ToDataTable(@"D:\username\Desktop\Test.xlsx");
            System.Console.WriteLine(dt.Rows.Count);
            dt.TableName = "FU .NET";
            dt.WriteXml(@"d:\testexc.xml");
        }


        public static DataTable ToDataTable(string fileName)
        {
            DataTable dt = null;

            using (System.IO.Stream fs = System.IO.File.OpenRead(fileName))
            {
                dt = ToDataTable(fs);
            }

            return dt;
        }

        public static DataTable ToDataTable(System.IO.Stream stream)
        {
            DataTable dt = null;

            using (OfficeOpenXml.ExcelPackage package = new OfficeOpenXml.ExcelPackage(stream))
            {
                dt = ToDataTable(package);
            }

            return dt;
        }

            // https://www.exceptionnotfound.net/designing-a-workflow-engine-database-part-1-introduction-and-purpose/
            public static DataTable ToDataTable(this ExcelPackage package)
        {
            ExcelWorksheet workSheet = package.Workbook.Worksheets.First();
            DataTable table = new DataTable();
            foreach (ExcelRangeBase firstRowCell 
                in workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column])
            {
                table.Columns.Add(firstRowCell.Text);
            } // Next firstRowCell 

            for (int rowNumber = 2; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
            {
                ExcelRange row = workSheet.Cells[rowNumber, 1, rowNumber
                    , workSheet.Dimension.End.Column];

                System.Data.DataRow newRow = table.NewRow();
                foreach (ExcelRangeBase cell in row)
                {
                    newRow[cell.Start.Column - 1] = cell.Text;
                } // Next cell 

                table.Rows.Add(newRow);
            } // Next rowNumber 

            return table;
        } // End Sub ToDataTable 


    } // End Class ExcelPackageExtensions 


} // End Namespace libExcelImport
