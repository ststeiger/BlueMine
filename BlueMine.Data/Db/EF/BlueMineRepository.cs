
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
