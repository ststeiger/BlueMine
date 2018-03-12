
namespace BlueMine.Db
{


    public class BlueMineRepository 
        : GenericEntityFramworkRepository<BlueMineContext> // ,IRedmineRepository
    {


        public BlueMineRepository(BlueMineContext context)
            : base(context)
        { }


        public System.Collections.Generic.List<string> GetValues(string csv)
        {
            System.Collections.Generic.List<string> ls = 
                new System.Collections.Generic.List<string>();

            using (System.IO.TextReader reader = new System.IO.StringReader(csv))
            {
                System.Tuple<System.Collections.Generic.IList<string>, 
                    System.Collections.Generic.IEnumerable<
                        System.Collections.Generic.IList<string>
                    >
                > data = Data.CsvParser.ParseHeadAndTail(reader, '-', '"');
                
                foreach (string header in data.Item1)
                {
                    if (!string.IsNullOrWhiteSpace(header))
                        ls.Add(header);
                } // Next header 

            } // End Using reader 

            return ls;
        } // End Function GetValues 



    } // End Class BlueMineRepository 


} // End Namespace BlueMine.Db 
