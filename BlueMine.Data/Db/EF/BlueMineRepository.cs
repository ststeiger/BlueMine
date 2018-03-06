
namespace BlueMine.Db
{


    public class BlueMineRepository 
        : GenericEntityFramworkRepository<BlueMineContext> // ,IRedmineRepository
    {


        public BlueMineRepository(BlueMineContext context)
            : base(context)
        { }


    } // End Class BlueMineRepository 


} // End Namespace BlueMine.Db 
