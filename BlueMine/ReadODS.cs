
using System.Linq;
using System.Xml.Linq;


// https://dzone.com/articles/reading-opendocument
namespace BlueMine
{
    
    public class ImportResult
    { }



    public class OdsImporter 
    {

        public static void ImporterTest()
        {
            OdsImporter imp = new OdsImporter();
            using (var st = System.IO.File.OpenRead(@"/root/mysheet.ods"))
            {
                imp.Import(st);
            }
        }


        public OdsImporter()
        { }
        
        
        public string[] SupportedFileExtensions
        {
            get { return new[] { "ods" }; }
        }
        
        
        public ImportResult Import(System.IO.Stream fileStream)
        {
            string contentXml = GetContentXml(fileStream);

            ImportResult result = new ImportResult();
            XDocument doc = XDocument.Parse(contentXml);

            System.Collections.Generic.IEnumerable<XElement> rows = 
                doc.Descendants("{urn:oasis:names:tc:opendocument:xmlns:table:1.0}table-row").Skip(1);

            foreach (XElement row in rows)
            {
                ImportRow(row, result); //, companyId, year, result);
            }

            return result;
        }
        
        
        private static string GetContentXml(System.IO.Stream fileStream)
        {
            string contentXml = "";

            using (ICSharpCode.SharpZipLib.Zip.ZipInputStream zipInputStream = 
                new ICSharpCode.SharpZipLib.Zip.ZipInputStream(fileStream))
            {
                ICSharpCode.SharpZipLib.Zip.ZipEntry contentEntry = null;
                while ((contentEntry = zipInputStream.GetNextEntry()) != null)
                {
                    if (!contentEntry.IsFile)
                        continue;
                    
                    if (contentEntry.Name.ToLower() == "content.xml")
                        break;
                }

                if (contentEntry.Name.ToLower() != "content.xml")
                {
                    throw new System.Exception("Cannot find content.xml");
                }

                byte[] bytesResult = new byte[] { };
                byte[] bytes = new byte[2000];
                int i = 0;

                while ((i = zipInputStream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    int arrayLength = bytesResult.Length;
                    System.Array.Resize<byte>(ref bytesResult, arrayLength + i);
                    System.Array.Copy(bytes, 0, bytesResult, arrayLength, i);
                }
                
                contentXml = System.Text.Encoding.UTF8.GetString(bytesResult);
            }
            
            return contentXml;
        }
        
        
        private static void ImportRow(XElement row, ImportResult result)
        {
            System.Collections.Generic.List<XElement> cells = (
                from c in row.Descendants()
                where c.Name == "{urn:oasis:names:tc:opendocument:xmlns:table:1.0}table-cell"
                select c
            ).ToList();
            
            // var dto = new DataDto();
            
            int count = cells.Count;
            int j = -1;

            for (int i = 0; i < count; i++)
            {
                j++;
                XElement cell = cells[i];
                XAttribute attr = cell.Attribute("{urn:oasis:names:tc:opendocument:xmlns:table:1.0}number-columns-repeated");
                if (attr != null)
                {
                    int numToSkip = 0;
                    if (int.TryParse(attr.Value, out numToSkip))
                    {
                        j += numToSkip - 1;
                    }
                }

                if (i > 30) break;
                if (j == 0)
                {
                    // dto.SomeProperty = cells[i].Value;
                }
                if (j == 1)
                {
                    // dto.SomeOtherProperty = cells[i].Value;
                }
                
                // some more data reading 
                
            } // Next i 
            
            // save data 
            
        } // End Sub ImportRow 
        
        
    } // End Class OdsImporter : ImporterBase 
    
    
} // End Namespace BlueMine 
