
using System.Linq;
//using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Mvc.Rendering;


//using BlueMine.Db;


namespace BlueMine.Db
{


    public class BlogStory
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public string Categories { get; set; }
        public System.DateTime DatePublished { get; set; }
        public bool IsPublished { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string UniqueId { get; set; }
    }
    
    
    public class BlueMineRepository 
        : GenericEntityFramworkRepository<BlueMineContext> // ,IRedmineRepository
    {
        
        
        public BlueMineRepository(BlueMineContext context)
            : base(context)
        { }
        
        
        
        public System.Collections.Generic.List<BlogStory> GetStories()
        {
            System.Collections.Generic.List<BlogStory> ls =
                new System.Collections.Generic.List<BlogStory>();

            ls.Add(new BlogStory()
            {
                Id = 123,
                Body = "I am the body",
                Categories = ",a,b,c,",
                DatePublished = System.DateTime.Now,
                IsPublished = true,
                Slug = "this_is_a_slug",
                Title = "This is NOT in any way a slug",
                UniqueId = System.Guid.NewGuid().ToString()
            });
            
            
            ls.Add(new BlogStory()
            {
                Id = 234,
                Body = "I am the 234 body",
                Categories = ",a,b,c,",
                DatePublished = System.DateTime.Now,
                IsPublished = true,
                Slug = "this_is_a_234_slug",
                Title = "This is NOT in any 234 way a slug",
                UniqueId = System.Guid.NewGuid().ToString()
            });
            
            return ls;
        }




        private System.Collections.Generic.List<T_custom_values>
            FetchCustomFieldsValues(string customized_type, int id)
        {
            //SELECT* FROM custom_values
            //WHERE customized_type = 'Issue'
            //AND customized_id = 1

            System.Collections.Generic.List<T_custom_values> custom_values = (
               from custom_value in this.m_ctx.custom_values
               where custom_value.customized_type == customized_type
               && custom_value.id == id
               select custom_value
           ).ToList();

            return custom_values;
        } // End Function FetchCustomFieldsValues 


        public System.Collections.Generic.List<T_custom_values> 
            GetIssueCustomFieldsValues(int id)
        {
            return FetchCustomFieldsValues("Issue", id);
        } // End Function FetchCustomFieldsValues 


        public System.Collections.Generic.List<string> FetchCustomFieldEntries(string fieldName)
        {
            System.Collections.Generic.List<string> lsFields = null;

            // SELECT possible_values, is_required, name FROM custom_fields WHERE (1=1)
            // AND name = 'Kundenname' AND field_format = 'list'
            string notCSV = (
                from custom_field in this.m_ctx.custom_fields
                where custom_field.name == fieldName
                && custom_field.field_format == "list"
                select custom_field.possible_values
            ).Single();

            // notCSV  = "--- - INTERN - \"------\" - BKB - Campus Sursee - Helvetia - Julius Bär - Post - Raiffeisen - Rockwell - RSI - SauterFM - SNB - Sonova (Phonak) - SRGSSR - Swisscom - Swisslife - SwissRe - Wincasa - Zürich";

            if (string.IsNullOrWhiteSpace(notCSV))
                return new System.Collections.Generic.List<string>();

            int firstNewLine = notCSV.IndexOf("\n");
            if (firstNewLine != -1)
            {
                notCSV = notCSV.Substring(firstNewLine + 1);
            }

            lsFields = this.GetDashSeparatedValues(notCSV);
            return lsFields;
        } // End Function FetchCustomFieldEntries 


        private System.Collections.Generic.List<string> GetDashSeparatedValues(string csv)
        {
            System.Collections.Generic.List<string> ls = 
                new System.Collections.Generic.List<string>();

            if (csv == null)
                return ls;

            using (System.IO.TextReader reader = new System.IO.StringReader(csv))
            {
                System.Collections.Generic.IEnumerable<System.Collections.Generic.IList<string>> res =
                    //Data.CsvParser.Parse(csv, '-', '"'); // foo - use ParseSimple
                    Data.CsvParser.Parse(reader, '-', '"');

                foreach (System.Collections.Generic.IList<string> row in res)
                {
                    string val = row[1];
                    // if (!string.IsNullOrWhiteSpace(val))
                    ls.Add(val);
                } // Next row 

                //System.Tuple<System.Collections.Generic.IList<string>,
                //    System.Collections.Generic.IEnumerable<
                //        System.Collections.Generic.IList<string>
                //    >
                //> data = Data.CsvParser.ParseHeadAndTail(reader, '-', '"');

                //foreach (string header in data.Item1)
                //{
                //    if (!string.IsNullOrWhiteSpace(header))
                //        ls.Add(header);
                //} // Next header 

            } // End Using reader 

            return ls;
        } // End Function GetDashSeparatedValues  


    } // End Class BlueMineRepository 
    
    
} // End Namespace BlueMine.Db 
